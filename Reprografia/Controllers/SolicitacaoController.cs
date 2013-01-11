using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Reprografia.Models;
using Reprografia.Models.ViewModels;
using System.Data.Entity.Validation;
using Omu.ValueInjecter;
using Reprografia.Data;
using Reprografia.Models.Account;
using Reprografia.BusinessLogic;

namespace Reprografia.Controllers
{
    [Authorize]
    public class SolicitacaoController : Controller
    {
        private ReprografiaContext db = new ReprografiaContext();

        public ActionResult Index()
        {
            var solicitacoes = from s in db.Solicitacoes
                               join a in db.Avaliacoes on s.Id equals a.Solicitacao.Id
                               where s.User.UserName == User.Identity.Name
                               select new { Solicitacao = s, Avaliacao = a };

            //Inicializar ViewModel a partir dos modelos de dados
            List<SolicitacaoIndexModel> model = new List<SolicitacaoIndexModel>();
            foreach (var line in solicitacoes)
            {
                SolicitacaoIndexModel modelItem = new SolicitacaoIndexModel();
                modelItem.InjectFrom(line.Avaliacao, line.Solicitacao);
                model.Add(modelItem);
            }

            ViewBag.PodeCriarSolicitacao = BusinessLogic.SolicitacaoBL.PodeCriarSolicitacao(User.Identity.Name) == StatusCriacaoSolicitacao.Permitido;

            return View(model);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var model = db.Solicitacoes
                    .Include("Fornecedor")
                    .Include("Area")
                    .Include("Codificacao")
                    .First(s => s.Id == id);

                if (User.IsInRole("Administrator") || model.UserName == User.Identity.Name)
                    return View(model);
                else
                    return RedirectToAction("Index");

            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new SolicitacaoCreateModel();
            model.Areas = new SelectList(db.Areas, "Id", "Nome", 1);

            var itemsCodificacoes = db.Codificacoes
                .OrderBy(c => c.CentroDeCusto)
                .ThenBy(c => c.ContaMemo)
                .AsEnumerable()
                .Select(c => new
                {
                    Nome = string.Format("{2} - {3} : {0} - {1}",
                        c.DescricaoCentroDeCusto,
                        c.DescricaoContaMemo,
                        c.CentroDeCusto,
                        c.ContaMemo),
                    Id = c.Id
                });
            model.Codificacoes = new SelectList(itemsCodificacoes,
                "Id", "Nome", SolicitacaoBL.CODIFICACAO_DEFAULT_ID);

            model.Fornecedores = new SelectList(db.Fornecedores, "Id", "Nome", 1);
            model.Solicitacao = SolicitacaoBL.CriarSolicitacao();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SolicitacaoCreateModel data)
        {
            StatusCriacaoSolicitacao statusUsuario = SolicitacaoBL.PodeCriarSolicitacao(User.Identity.Name);
            if (!ModelState.IsValid ||
                statusUsuario != StatusCriacaoSolicitacao.Permitido) //Inválido
            {
                if (!ModelState.IsValid)
                    ViewBag.error = "Dados inválidos";
                else
                    switch (statusUsuario)
                    {
                        case StatusCriacaoSolicitacao.AvaliacaoPendente:
                            ViewBag.error = "Usuário possui avaliações pendentes!";
                            break;
                        default:
                            ViewBag.error = "Erro na autenticação do usuário";
                            break;
                    }
                //data.Areas = new SelectList(db.Areas, "Id", "Nome", 1);
                //data.Codificacoes = new SelectList(
                //    db.Codificacoes
                //    .OrderBy(c => c.CentroDeCusto)
                //    .ThenBy(c => c.ContaMemo)
                //    .AsEnumerable()
                //    .Select(c =>
                //        new
                //        {
                //            Nome = string.Format("{2} - {3} : {0} - {1}",
                //                c.DescricaoCentroDeCusto,
                //                c.DescricaoContaMemo,
                //                c.CentroDeCusto,
                //                c.ContaMemo)
                //        ,
                //            Id = c.Id
                //        })
                //        , "Id", "Nome", 1);
                //data.Fornecedores = new SelectList(db.Fornecedores, "Id", "Nome", 1);

                return Create();
            }

            try
            {
                var user = db.Users.Find(User.Identity.Name);
                Solicitacao target = SolicitacaoBL.CriarSolicitacao(data.Solicitacao);
                target.Itens = data.Itens;
                target.User = user;

                db.Solicitacoes.Add(target);

                //Criar avaliacao correspondente
                var avaliacao = AvaliacaoBL.CriarAvaliacao(target);

                db.Avaliacoes.Add(avaliacao);

                target.Avaliacao = avaliacao;

                db.SaveChanges();
            }
            catch (BusinessLogicException ex)
            {
                switch (ex.Razao)
                {
                    case StatusCriacaoSolicitacao.AvaliacaoPendente:
                        TempData.Add("ErrorMessage", "Usuário possui avaliações pendentes");
                        break;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult Admin()
        {
            var solicitacoes = db.Solicitacoes
                .Include("Avaliacao");

            //Inicializar ViewModel a partir dos modelos de dados
            List<SolicitacaoIndexModel> model = new List<SolicitacaoIndexModel>();
            foreach (var s in solicitacoes)
            {
                SolicitacaoIndexModel modelItem = new SolicitacaoIndexModel();
                modelItem.InjectFrom(s.Avaliacao, s);
                modelItem.Cancelavel = s.IsCancelavel();
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var model = db.Solicitacoes.Find(id);
            return View(model);
        }

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, string submit)
        {
            if (submit == "Sim")
            {
                Solicitacao solicitacao = db.Solicitacoes.Find(id);
                db.Avaliacoes.Remove(solicitacao.Avaliacao);
                db.Solicitacoes.Remove(solicitacao);
                db.SaveChanges();
            }
            return RedirectToAction("Admin");
        }


        public ActionResult Xl(string id)
        {
            string[] anoSeq = id.Split('-');
            int ano = int.Parse(anoSeq[0]);
            int seq = int.Parse(anoSeq[1]);
            Solicitacao solicitacao = db.Solicitacoes
                .Include("User")
                .Include("Fornecedor")
                .Include("Itens")
                .First(s => s.Ano == ano && s.Seq == seq);

            string resultPath = SolicitacaoBL.EscreverXl(solicitacao, Server.MapPath("~"));

            return new FilePathResult(resultPath, "application/vnd.ms-excel");
        }
    }
}

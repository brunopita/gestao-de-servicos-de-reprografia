using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reprografia.Data;
using Reprografia.Models.ViewModels;
using Omu.ValueInjecter;
using Reprografia.BusinessLogic;

namespace Reprografia.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ReprografiaContext db = new ReprografiaContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult SolicitacoesList()
        {
            var solicitacoes = db.Solicitacoes
                .Include("Avaliacao")
                .Include("User");

            //Inicializar ViewModel a partir dos modelos de dados
            List<SolicitacaoIndexModel> model = new List<SolicitacaoIndexModel>();
            foreach (var s in solicitacoes)
            {
                SolicitacaoIndexModel modelItem = new SolicitacaoIndexModel();
                modelItem.InjectFrom(s.Avaliacao, s);
                modelItem.InsertFrom(s);
                modelItem.Cancelavel = s.IsCancelavel();
                model.Add(modelItem);
            }
            return View(model);
        }

        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult ActionReport(int year = 0, int month = 0)
        {
            if (year == 0)
                year = DateTime.Today.Year;

            if (month == 0)
                month = DateTime.Today.Month;
            var solicitacoes = db.Solicitacoes
                .Include("User")
                .Include("Avaliacao")
                .Include("Avaliacao.ItensAvaliacao");

            solicitacoes.Where(s => s.Ano == year && s.DataSolicitacao.Month == month);

            if (solicitacoes.Count() == 0)
                throw new HttpException(404, "Nenhuma solicitação encontrada para o mês selecionado");

            List<ActionReportViewModel> model = new List<ActionReportViewModel>(solicitacoes.Count());
            foreach (var s in solicitacoes)
                model.Add(new ActionReportViewModel(s));

            //Passar parâmetros de filtro para exibição na view
            ViewBag.Year = year;
            ViewBag.Month = month;

            return View(model);
        }

    }
}

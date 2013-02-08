using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reprografia.Models.Account;
using Microsoft.Office.Interop.Excel;
using Reprografia.lib;
using System.IO;
using Reprografia.Data.XL;

namespace Reprografia.BusinessLogic
{
    public static class SolicitacaoBL
    {
        public const int CODIFICACAO_DEFAULT_ID = 23;
        public const int DIAS_ESPERADOS_PARA_ENTREGA = 3;
        private static Data.ReprografiaContext db = new Data.ReprografiaContext();

        public static Models.Solicitacao CriarSolicitacao(Models.Solicitacao solicitacao, int ano = 0)
        {
            if (ano == 0)
                ano = DateTime.Now.Year;

            solicitacao.DataEntrega = DateTime.Now.AddDays(DIAS_ESPERADOS_PARA_ENTREGA).Date;
            //Determinar numero sequencial para inserção no banco
            DeterminarSeq(solicitacao, ano);
            solicitacao.DataSolicitacao = DateTime.Now.Date;

            return solicitacao;
        }

        public static Models.Solicitacao DeterminarSeq(Models.Solicitacao solicitacao, int ano = 0)
        {
            if (ano == 0)
                ano = DateTime.Now.Year;
            var solicitacoes = from s in db.Solicitacoes where s.Ano == ano select s;

            solicitacao.Ano = ano;
            solicitacao.Seq = (solicitacoes.Count() == 0 ? 1 : solicitacoes.Max(s => s.Seq)) + 1;
            return solicitacao;
        }

        public static Models.Solicitacao CriarSolicitacao()
        {
            var solicitacao = new Models.Solicitacao();
            return CriarSolicitacao(solicitacao);
        }

        public static Models.Solicitacao CriarSolicitacao(User user, int ano = 0)
        {
            var podeCriarSolicitacao = PodeCriarSolicitacao(user);
            if (podeCriarSolicitacao != StatusCriacaoSolicitacao.Permitido)
                throw new BusinessLogicException("Usuário não está autorizado a criar solicitações", podeCriarSolicitacao);
            var solicitacao = CriarSolicitacao(new Models.Solicitacao(), ano);
            //solicitacao.User = user;

            return solicitacao;
        }

        public static Models.Solicitacao CriarSolicitacao(string userName, int ano = 0)
        {
            User user = db.Users.Find(userName);

            return CriarSolicitacao(user, ano);
        }

        public static StatusCriacaoSolicitacao PodeCriarSolicitacao(User user)
        {
            //return StatusCriacaoSolicitacao.Permitido;

            if (!AvaliacaoBL.PossuiAvaliacaoPendente(user))
                return StatusCriacaoSolicitacao.Permitido;
            else
                return StatusCriacaoSolicitacao.AvaliacaoPendente;
        }

        public static StatusCriacaoSolicitacao PodeCriarSolicitacao(string userName)
        {
            User user = db.Users.Find(userName);
            return PodeCriarSolicitacao(user);
        }

        public static void EscreverXl(Models.Solicitacao solicitacao, string siteRoot, Stream destination)
        {
            var strategy = new SolicitacaoWriterStrategy(solicitacao);
            var writer = new XLWriter(Path.Combine(siteRoot,"Excel/Planilhas/"), strategy);
            writer.WriteAll(destination);
            
        }

        internal static IQueryable<Reprografia.Models.Solicitacao> GetByCancelavel(bool cancelavel)
        {
            if (cancelavel)
                return db.Solicitacoes.Include("Avaliacao")
                    .Where(s => s.DataEntrega >= DateTime.Now.Date && !s.Avaliacao.Avaliado);
            return db.Solicitacoes.Include("Avaliacao")
                .Where(s => s.DataEntrega < DateTime.Now.Date || s.Avaliacao.Avaliado);
        }

        internal static bool IsCancelavel(this Reprografia.Models.Solicitacao solicitacao)
        {
            return !solicitacao.Avaliacao.Avaliado;
        }
    }
}
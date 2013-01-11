using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reprografia.Models.Account;
using Microsoft.Office.Interop.Excel;
using Reprografia.lib;
using System.IO;

namespace Reprografia.BusinessLogic
{
    public static class SolicitacaoBL
    {
        public const int CODIFICACAO_DEFAULT_ID = 25;
        public const int DIAS_ESPERADOS_PARA_ENTREGA = 5;
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

        public static string EscreverXl(Models.Solicitacao solicitacao, string siteRoot)
        {
            using (ExcelWrapper wrapper = new ExcelWrapper())
            {
                Application xl = wrapper.App;
                Workbook wkb = xl.Workbooks.Add(Path.Combine(siteRoot, @"Excel\Planilhas\Solicitacao.xlt"));
                xl.DisplayAlerts = false;
                Worksheet sheet = wkb.ActiveSheet;
                WriteToXl(sheet, solicitacao);

                string destPath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Process),
                    string.Format(@"Solicitacao{0}-{1}.xls", solicitacao.Ano, solicitacao.Seq));
                //string destPath = Path.Combine(siteRoot, string.Format(@"Excel\Solicitacoes\Solicitacao{0}-{1}.xls",
                //    solicitacao.Ano, solicitacao.Seq));

                if (File.Exists(destPath))
                    File.Delete(destPath);

                wkb.SaveAs(Filename: destPath
                    , FileFormat: XlFileFormat.xlExcel8);
                wkb.Close();
                wrapper.Dispose();

                return destPath;
            }
        }

        private static void WriteToXl(Worksheet sheet, Models.Solicitacao solicitacao)
        {
            sheet.Range["FullName"].Value = solicitacao.User.FullName;
            sheet.Range["DataSolicitacao"].Value = solicitacao.DataSolicitacao.ToShortDateString();
            sheet.Range["DataEntrega"].Value = solicitacao.DataEntrega.ToShortDateString();
            sheet.Range["CC"].Value = solicitacao.Codificacao.CentroDeCusto.ToString();
            sheet.Range["CM"].Value = solicitacao.Codificacao.ContaMemo.ToString();
            sheet.Range["Numero"].Value = string.Format(@"{0}\{1}", solicitacao.Ano, solicitacao.Seq);
            sheet.Range["Fornecedor"].Value = solicitacao.Fornecedor.Nome;
            //Fone

            int i = 1;
            foreach (Reprografia.Models.Item item in solicitacao.Itens)
            {
                sheet.Range["Titulo" + i].Value = item.Descricao;
                sheet.Range["Paginas" + i].Value = item.Paginas.ToString();
                sheet.Range["Copias" + i].Value = item.Paginas.ToString();
                sheet.Range["GramposACavalo" + i].Value = item.GramposACavalo ? "X" : "";
                sheet.Range["GramposLaterais" + i].Value = item.GramposLaterais ? "X" : "";
                sheet.Range["Espiral" + i].Value = item.Espiral ? "X" : "";
                sheet.Range["CapaEmPVC" + i].Value = item.CapaEmPVC ? "X" : "";
                sheet.Range["CapaEmPapel" + i].Value = item.CapaEmPapel ? "X" : "";
                sheet.Range["Transparencia" + i].Value = item.Transparencia ? "X" : "";
                sheet.Range["Reduzido" + i].Value = item.Reduzido ? "X" : "";
                sheet.Range["Ampliado" + i].Value = item.Ampliado ? "X" : "";
                sheet.Range["Digitacao" + i].Value = item.Digitacao ? "X" : "";
                sheet.Range["SemAcabamento" + i].Value = item.SemAcabamento ? "X" : "";
                sheet.Range["Grampear" + i].Value = item.Grampear ? "X" : "";
                sheet.Range["PretoBranco" + i].Value = item.PretoBranco ? "X" : "";
                sheet.Range["FrenteVerso" + i].Value = item.FrenteVerso ? "X" : "";
                sheet.Range["SoFrente" + i].Value = item.SoFrente ? "X" : "";
                sheet.Range["CortarAoMeio" + i].Value = item.CortarAoMeio ? "X" : "";
                i++;
            }
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
            return solicitacao.DataEntrega >= DateTime.Now.Date && !solicitacao.Avaliacao.Avaliado;
        }
    }
}
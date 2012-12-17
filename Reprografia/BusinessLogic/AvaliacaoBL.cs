using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reprografia.Models.Account;
using Reprografia.lib;
using Microsoft.Office.Interop.Excel;
using System.IO;
using Reprografia.Models;

namespace Reprografia.BusinessLogic
{
    public static class AvaliacaoBL
    {
        private static Data.ReprografiaContext db = new Data.ReprografiaContext();
        public const double DIAS_CORRIDOS_PARA_AVALIAR = 10.0;

        /// <summary>
        /// Determina se <paramref name="user"/> possui alguma avaliação pendente
        /// </summary>
        /// <param name="user">usuário a ser analisado</param>
        /// <returns>true se alguma das Avaliações de <paramref name="user"/> não tiver sido avaliada</returns>
        public static bool PossuiAvaliacaoPendente(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", "user is null.");
            var solicitacoes = from s in db.Solicitacoes
                               where s.User.UserName == user.UserName
                               select s;

            //Verificar se avaliações estão pendentes
            return solicitacoes.Any(s => !s.Avaliacao.Avaliado);
        }

        public static Models.Avaliacao CriarAvaliacao()
        {
            return new Models.Avaliacao();
        }
        public static Models.Avaliacao CriarAvaliacao(Models.Solicitacao solicitacao)
        {
            var avaliacao = CriarAvaliacao();
            avaliacao.Avaliado = false;
            avaliacao.DataLimite = solicitacao.DataSolicitacao.AddDays(DIAS_CORRIDOS_PARA_AVALIAR);
            avaliacao.Solicitacao = solicitacao;

            foreach (var item in solicitacao.Itens)
            {
                avaliacao.ItemsAvaliacao.Add(new Models.ItemAvaliacao(item)
                    {
                        Avaliacao = avaliacao
                    });
            }

            return avaliacao;
        }

        public static string EscreverXl(Models.Avaliacao avaliacao, string siteRoot)
        {
            using (var wrapper = new ExcelWrapper())
            {
                Application xl = wrapper.App;
                Workbook wkb = xl.Workbooks.Add(Path.Combine(siteRoot, @"Excel\Planilhas\Avaliacao.xlt"));
                xl.DisplayAlerts = false;
                Worksheet sheet = wkb.ActiveSheet;
                WriteToXl(sheet, avaliacao);

                string destPath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Process),
                    string.Format(@"Solicitacao{0}-{1}.xls", avaliacao.Solicitacao.Ano, avaliacao.Solicitacao.Seq));
                //string destPath = Path.Combine(siteRoot, string.Format(@"Excel\Solicitacoes\Avaliacao{0}-{1}.xls",
                //    avaliacao.Solicitacao.Ano, avaliacao.Solicitacao.Seq));

                if (File.Exists(destPath))
                    File.Delete(destPath);

                wkb.SaveAs(Filename: destPath
                    , FileFormat: XlFileFormat.xlExcel8);
                wkb.Close();

                wrapper.Dispose();
                return destPath;
            }
        }

        private static void WriteToXl(Worksheet sheet, Models.Avaliacao avaliacao)
        {
            sheet.Range["Fornecedor"].Value = avaliacao.Solicitacao.Fornecedor.Nome;
            sheet.Range["Id"].Value = string.Format("{0}-{1}", avaliacao.Solicitacao.Ano, avaliacao.Solicitacao.Seq);
            sheet.Range["FullName"].Value = avaliacao.Solicitacao.User.FullName;
            sheet.Range["Date"].Value = avaliacao.DataAvaliado;

            int i = 1;
            foreach (ItemAvaliacao item in avaliacao.ItemsAvaliacao)
            {
                sheet.Range["Titulo" + i].Value = item.Item.Descricao;

                sheet.Range["Prazo" + i].Value = item.Prazo.ToXlString();
                sheet.Range["Nitidez" + i].Value = item.Nitidez.ToXlString();
                sheet.Range["Paginacao" + i].Value = item.Paginacao.ToXlString();
                sheet.Range["Quantidade" + i].Value = item.Quantidade.ToXlString();
                sheet.Range["Matriz" + i].Value = item.Matriz.ToXlString();
                sheet.Range["Acabamento" + i].Value = item.Acabamento.ToXlString();
                i++;
            }

        }
        private static string ToXlString(this AvaliacaoNotaEnum value)
        {
            switch (value)
            {
                case AvaliacaoNotaEnum.A:
                    return "A";
                case AvaliacaoNotaEnum.X:
                    return "X";
                case AvaliacaoNotaEnum.NA:
                    return "NA";
                default:
                    //throw new ArgumentException("Valor fora da enumeração");
                    return "NA";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reprografia.Models;
using Reprografia.BusinessLogic;

namespace Reprografia.Data.XL
{
    public class AvaliacaoWriterStrategy : XlWriterStrategy
    {
        public override string ModelFilename
        {
            get { return "Avaliacao.xls"; }
        }

        public AvaliacaoWriterStrategy(Avaliacao Avaliacao)
        {
            this.Avaliacao = Avaliacao;
        }

        protected Dictionary<string, string> values = new Dictionary<string, string>();

        private Avaliacao _avaliacao;

        public Avaliacao Avaliacao
        {
            get { return _avaliacao; }
            set
            {
                _avaliacao = value;
                FillValues(value);
            }
        }

        private void FillValues(Avaliacao avaliacao)
        {
            values.Add("Fornecedor", avaliacao.Solicitacao.Fornecedor.Nome);
            values.Add("Id", string.Format("{0}-{1}", avaliacao.Solicitacao.Ano, avaliacao.Solicitacao.Seq));
            values.Add("FullName", avaliacao.Solicitacao.User.FullName);
            values.Add("Date", avaliacao.DataAvaliado.ToString());
            values.Add("Total", avaliacao.Satisfacao.ToString("0%"));

            {
                int i = 1;
                foreach (ItemAvaliacao item in avaliacao.ItensAvaliacao)
                {
                    values.Add("Titulo" + i, item.Item.Descricao);

                    values.Add("Prazo" + i, item.Prazo.ToXlString());
                    values.Add("Nitidez" + i, item.Nitidez.ToXlString());
                    values.Add("Paginacao" + i, item.Paginacao.ToXlString());
                    values.Add("Quantidade" + i, item.Quantidade.ToXlString());
                    values.Add("Matriz" + i, item.Matriz.ToXlString());
                    values.Add("Acabamento" + i, item.Acabamento.ToXlString());
                    values.Add("Total" + i, item.GetSatisfacao().ToString("0%"));
                    i++;
                }
            }
        }

        public override Dictionary<string, string> Values
        {
            get { return values; }
        }
    }
}
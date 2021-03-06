﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Reprografia.Models;

namespace Reprografia.Data.XL
{
    public class SolicitacaoWriterStrategy : XlWriterStrategy
    {
        public override string ModelFilename
        {
            get { return "Solicitacao.xls"; }
        }

        public SolicitacaoWriterStrategy(Solicitacao solicitacao)
        {
            this.Solicitacao = solicitacao;
        }

        protected Dictionary<string, string> values = new Dictionary<string, string>();

        private Solicitacao _solicitacao;

        public Solicitacao Solicitacao
        {
            get { return _solicitacao; }
            set
            {
                _solicitacao = value;
                // Preencher values com valores necessários da solicitacao
                FillValues(value);
            }
        }

        private void FillValues(Solicitacao solicitacao)
        {
            values.Add("FullName", solicitacao.User.FullName);
            values.Add("DataSolicitacao", solicitacao.DataSolicitacao.ToShortDateString());
            values.Add("DataEntrega", solicitacao.DataEntrega.ToShortDateString());
            values.Add("CC", solicitacao.Codificacao.CentroDeCusto.ToString());
            values.Add("CM", solicitacao.Codificacao.ContaMemo.ToString());
            values.Add("Numero", string.Format(@"{0}-{1}", solicitacao.Ano, solicitacao.Seq));
            values.Add("Fornecedor", solicitacao.Fornecedor.Nome);
            values.Add("Comentario", solicitacao.Comment);

            // Escrever Curso e Suporte em seus respectivos Nomes
            values.Add(solicitacao.Suporte, "X");

            // Por enquanto limitar somente a áreas suportadas pela planilha
            if (new String[] { "CAI", "CT", "FC", "CST", "Pos" }.Contains(solicitacao.Area.Nome))
            {
                values.Add(solicitacao.Area.Nome, "X");
            }

            // Prefixo "Formato" para compatibilidade com nomes do Excel
            // Formatos: [A3, A4, A5, Outro]
            values.Add("Formato" + solicitacao.Formato, "X");

            int i = 1;
            foreach (Reprografia.Models.Item item in solicitacao.Itens)
            {
                values.Add("Titulo" + i, item.Descricao);
                values.Add("Paginas" + i, item.Paginas.ToString());
                values.Add("Copias" + i, item.Copias.ToString());
                values.Add("Total" + i, (item.Paginas * item.Copias).ToString());
                values.Add("GramposACavalo" + i, item.GramposACavalo ? "X" : "");
                values.Add("GramposLaterais" + i, item.GramposLaterais ? "X" : "");
                values.Add("Espiral" + i, item.Espiral ? "X" : "");
                values.Add("CapaEmPVC" + i, item.CapaEmPVC ? "X" : "");
                values.Add("CapaEmPapel" + i, item.CapaEmPapel ? "X" : "");
                values.Add("Transparencia" + i, item.Transparencia ? "X" : "");
                values.Add("Reduzido" + i, item.Reduzido ? "X" : "");
                values.Add("Ampliado" + i, item.Ampliado ? "X" : "");
                values.Add("Digitacao" + i, item.Digitacao ? "X" : "");
                values.Add("SemAcabamento" + i, item.SemAcabamento ? "X" : "");
                values.Add("Grampear" + i, item.Grampear ? "X" : "");
                values.Add("PretoBranco" + i, item.PretoBranco ? "X" : "");
                values.Add("FrenteVerso" + i, item.FrenteVerso ? "X" : "");
                values.Add("SoFrente" + i, item.SoFrente ? "X" : "");
                values.Add("CortarAoMeio" + i, item.CortarAoMeio ? "X" : "");
                i++;
            }

        }

        public override Dictionary<string, string> Values
        {
            get { return values; }
        }
    }
}
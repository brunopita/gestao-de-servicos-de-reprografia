using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reprografia.Models.ViewModels
{
    public class SolicitacaoCreateModel
    {

        public SolicitacaoCreateModel()
        {
            this.Formatos = new SelectList(new[] { "A4", "A3", "A5" });
            this.Suportes = new SelectList(new[] { "E-Mail", "Zipdrive", "Papel", "CD" });
        }

        public Solicitacao Solicitacao { get; set; }

        public SelectList Areas { get; set; }

        public SelectList Codificacoes { get; set; }

        public SelectList Fornecedores { get; set; }

        public SelectList Suportes { get; set; }

        public SelectList Formatos { get; set; }

        public ICollection<Item> Itens { get; set; }
    }
}
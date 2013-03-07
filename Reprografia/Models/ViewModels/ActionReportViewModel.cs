using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reprografia.Models.ViewModels
{
    public class ActionReportViewModel
    {
        public ActionReportViewModel(Solicitacao s)
        {
            this.Fill(s);
        }

        public void Fill(Solicitacao s)
        {
            this.Id = s.Id;
            this.Ano = s.Ano;
            this.Seq = s.Seq;
            this.AnoSeq = s.AnoSeq;
            this.SN = s.User.UserName;
            this.FullName = s.User.FullName;
            this.DataSolicitacao = s.DataSolicitacao;
            this.Satisfacao = s.Avaliacao.Satisfacao;
        }

        public int Id { get; set; }
        public int Ano { get; set; }
        public int Seq { get; set; }
        public string AnoSeq { get; set; }

        public string SN { get; set; }
        public string FullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0%}")]
        public double Satisfacao { get; set; }

        [Display(Name = "Data de Solicitação"),
        DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name= "Ação")]
        public string Acao { get; set; }
    }
}
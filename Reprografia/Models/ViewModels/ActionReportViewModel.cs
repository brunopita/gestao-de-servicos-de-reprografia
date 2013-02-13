using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reprografia.Models.ViewModels
{
    public class ActionReportViewModel
    {
        public ActionReportViewModel(Solicitacao s)
        {
            this.Id = s.Id;
            this.Ano = s.Ano;
            this.Seq = s.Seq;
            this.AnoSeq = s.AnoSeq;
            this.sn = s.User.UserName;
            this.FullName = s.User.FullName;
            this.DataSolicitacao = s.DataSolicitacao;
        }
        public int Id { get; set; }
        public int Ano { get; set; }
        public int Seq { get; set; }
        public string AnoSeq { get; set; }

        public string sn { get; set; }
        public string FullName { get; set; }

        public int Nota { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public string Acao { get; set; }
    }
}
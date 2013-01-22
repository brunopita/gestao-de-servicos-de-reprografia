using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models.ViewModels
{
    public class SolicitacaoIndexModel
    {

        public SolicitacaoIndexModel()
        {
        }

        public SolicitacaoIndexModel(Solicitacao solicitacao)
            : this()
        {
            this.InsertFrom(solicitacao);
        }

        public void InsertFrom(Solicitacao solicitacao)
        {
            this.Id = solicitacao.Id;
            this.Ano = solicitacao.Ano;
            this.AnoSeq = solicitacao.AnoSeq;
            this.Formato = solicitacao.Formato;
            this.UserName = solicitacao.UserName;
            this.Suporte = solicitacao.Suporte;
            this.DataLimite = solicitacao.Avaliacao.DataLimite;
            this.Avaliado = solicitacao.Avaliacao.Avaliado;
        }

        public int Id { get; set; }
        public string AnoSeq { get; set; }
        public int Ano { get; set; }
        public string Formato { get; set; }


        [Display(Name = "Data de Solicitação"),
        DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSolicitacao { get; set; }

        public string UserName { get; set; }
        public string Suporte { get; set; }



        [Display(Name = "Data Limite para Avaliação"),
        DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataLimite { get; set; }

        public bool Avaliado { get; set; }

        public bool Cancelavel { get; set; }
    }
}
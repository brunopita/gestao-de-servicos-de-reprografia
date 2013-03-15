using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class Avaliacao
    {

        public Avaliacao()
        {
            this.ItensAvaliacao = new HashSet<ItemAvaliacao>();
            this.DataLimite = new DateTime(1900, 1, 1);
            this.DataAvaliado = new DateTime(1900, 1, 1);
        }

        public int Id { get; set; }

        //public int SolicitacaoId { get; set; }
        [ForeignKey("Id")]
        public virtual Solicitacao Solicitacao { get; set; }

        public bool Avaliado { get; set; }

        private DateTime _DataLimite;
        [Display(Name = "Data limite para avaliação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataLimite
        {
            get
            {
                return _DataLimite;
            }
            set
            {
                _DataLimite = value.Date;
            }
        }

        private DateTime _DataAvaliado;
        [Display(Name = "Data Avaliada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAvaliado
        {
            get
            {
                return _DataAvaliado;
            }
            set
            {
                _DataAvaliado = value.Date;
            }
        }

        public virtual ICollection<ItemAvaliacao> ItensAvaliacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:0%}")]
        public double Satisfacao
        {
            get
            {
                return this.ItensAvaliacao.AsEnumerable().Average(i => i.Satisfacao);
            }
        }

        public string Acao { get; set; }
    }
}
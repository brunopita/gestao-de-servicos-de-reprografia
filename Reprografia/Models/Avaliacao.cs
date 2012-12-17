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
            this.ItemsAvaliacao = new HashSet<ItemAvaliacao>();
            this.DataLimite = new DateTime(1900, 1, 1);
            this.DataAvaliado = new DateTime(1900, 1, 1);
        }

        public int Id { get; set; }

        public int SolicitacaoId { get; set; }
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

        public virtual ICollection<ItemAvaliacao> ItemsAvaliacao { get; set; }
    }
}
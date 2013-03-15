using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Reprografia.BusinessLogic;

namespace Reprografia.Models
{
    public class ItemAvaliacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public virtual Item Item { get; set; }

        public int AvaliacaoId { get; set; }
        public virtual Avaliacao Avaliacao { get; set; }

        public string Prazo { get; set; }
        public string Nitidez { get; set; }

        [Display(Name = "Paginação")]
        public string Paginacao { get; set; }
        public string Quantidade { get; set; }
        public string Matriz { get; set; }
        public string Acabamento { get; set; }

        [DisplayFormat(DataFormatString = "{0:0%}")]
        [Display(Name = "Satisfação")]
        public double Satisfacao
        {
            get
            {
                return this.GetSatisfacao();
            }
        }

        public ItemAvaliacao(Models.Item item)
            : this()
        {
            this.Item = item;
        }

        public ItemAvaliacao() { }
    }
}
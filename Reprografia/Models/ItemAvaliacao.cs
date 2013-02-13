using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class ItemAvaliacao
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Item")]
        //public int Item_Id { get; set; }

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

        public double GetSatisfacao()
        {
            //n[AvaliacaoNotaEnum.Aceitavel] / (n[AvaliacaoNotaEnum.Aceitavel] + n[AvaliacaoNotaEnum.NaoAceitavel])
            double aceitavel = 0.0;
            double nAceitavel = 0.0;

            switch ((AvaliacaoNotaEnum)this.Prazo[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch ((AvaliacaoNotaEnum)this.Nitidez[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch ((AvaliacaoNotaEnum)this.Paginacao[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch ((AvaliacaoNotaEnum)this.Quantidade[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch ((AvaliacaoNotaEnum)this.Matriz[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch ((AvaliacaoNotaEnum)this.Acabamento[0])
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.NA: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            this.Satisfacao = aceitavel / (aceitavel + nAceitavel);
            return this.Satisfacao;
        }

        [DisplayFormat(DataFormatString = "p")]
        [Display(Name = "Satisfação")]
        public double Satisfacao { get; set; }

        public ItemAvaliacao(Models.Item item)
            : this()
        {
            this.Item = item;
        }

        public ItemAvaliacao() { }
    }
}
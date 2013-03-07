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
            this.Satisfacao = GetSatisfacao(new string[] {
                this.Prazo, 
                this.Nitidez,
                this.Paginacao,
                this.Quantidade,
                this.Matriz,
                this.Acabamento
            });
            return this.Satisfacao;
        }

        private static double GetSatisfacao(string[] notas)
        {
            double aceitavel = 0.0, nAceitavel = 0.0;

            foreach (var nota in notas)
                if (nota != null)
                    switch ((AvaliacaoNotaEnum)nota[0])
                    {
                        case AvaliacaoNotaEnum.A: aceitavel++; break;
                        case AvaliacaoNotaEnum.NA: aceitavel++; break;
                        case AvaliacaoNotaEnum.X: nAceitavel++; break;
                        default: aceitavel++; break;
                    }
            return aceitavel / (aceitavel + nAceitavel);
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
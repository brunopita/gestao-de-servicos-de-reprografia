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

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int AvaliacaoId { get; set; }
        public virtual Avaliacao Avaliacao { get; set; }

        #region Propriedades de formulário

        public AvaliacaoNotaEnum Prazo { get; set; }
        public AvaliacaoNotaEnum Nitidez { get; set; }

        [Display(Name="Paginação")]
        public AvaliacaoNotaEnum Paginacao { get; set; }
        public AvaliacaoNotaEnum Quantidade { get; set; }
        public AvaliacaoNotaEnum Matriz { get; set; }
        public AvaliacaoNotaEnum Acabamento { get; set; }

        public double GetSatisfacao()
        {
            //n[AvaliacaoNotaEnum.Aceitavel]/ n[AvaliacaoNotaEnum.Aceitavel] + n[AvaliacaoNotaEnum.NaoAceitavel]
            int aceitavel = 0;
            int nAceitavel = 0;

            switch (this.Prazo)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch (this.Nitidez)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch (this.Paginacao)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch (this.Quantidade)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch (this.Matriz)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }

            switch (this.Acabamento)
            {
                case AvaliacaoNotaEnum.A: aceitavel++; break;
                case AvaliacaoNotaEnum.X: nAceitavel++; break;
            }
            return aceitavel / aceitavel + nAceitavel;
        }

        [DisplayFormat(DataFormatString="p")]
        [Display(Name = "Satisfação")]
        public double Satisfacao { get; set; }

        #endregion

        public ItemAvaliacao(Models.Item item)
        {
            this.Item = item;
        }
        public ItemAvaliacao()
        {
        }
    }
}
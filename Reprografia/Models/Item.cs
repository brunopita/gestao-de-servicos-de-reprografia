using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Range(1, 10000, ErrorMessage = "A quantidade de páginas deve estar entre 1 e 10000")]
        [Display(Name = "Páginas")]
        public int Paginas { get; set; }

        [Display(Name = "Cópias")]
        [Range(1, 10000, ErrorMessage = "A quantidade de cópias deve estar entre 1 e 10000")]
        public int Copias { get; set; }

        #region Propriedades de formulário

        [Display(Name = "2 grampos a cavalo")]
        public bool GramposACavalo { get; set; }

        [Display(Name = "2 grampos laterais")]
        public bool GramposLaterais { get; set; }

        [Display(Name = "Encadernação em espiral")]
        public bool Espiral { get; set; }

        [Display(Name = "Capa em PVC")]
        public bool CapaEmPVC { get; set; }

        [Display(Name = "Capa em papel 150g/cm²")]
        public bool CapaEmPapel { get; set; }

        [Display(Name = "Transparência P&B")]
        public bool Transparencia { get; set; }

        [Display(Name = "Reduzido")]
        public bool Reduzido { get; set; }

        [Display(Name = "Ampliado")]
        public bool Ampliado { get; set; }

        [Display(Name = "Digitação")]
        public bool Digitacao { get; set; }

        [Display(Name = "Sem acabamento")]
        public bool SemAcabamento { get; set; }

        [Display(Name = "Grampear")]
        public bool Grampear { get; set; }

        [Display(Name = "Preto & Branco")]
        public bool PretoBranco { get; set; }

        [Display(Name = "Frente e Verso")]
        public bool FrenteVerso { get; set; }

        [Display(Name = "Só Frente")]
        public bool SoFrente { get; set; }

        [Display(Name = "Cortar ao Meio")]
        public bool CortarAoMeio { get; set; }

        #endregion

        public int SolicitacaoId { get; set; }
        public virtual Solicitacao Solicitacao { get; set; }

        public virtual ItemAvaliacao ItemAvaliacao { get; set; }
    }
}

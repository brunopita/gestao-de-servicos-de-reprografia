using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Reprografia.Models.Account;

namespace Reprografia.Models
{
    public class Solicitacao
    {
        public Solicitacao()
        {
            this.Itens = new HashSet<Item>();
            this.DataEntrega = new DateTime(1900, 1, 1);
            this.DataSolicitacao = new DateTime(1900, 1, 1);
        }

        public int Id { get; set; }
        public int Ano { get; set; }

        [DisplayFormat(DataFormatString = "{0:000}")]
        public int Seq { get; set; }

        public int FornecedorId { get; set; }
        [Display(Name = "Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        [Display(Description = "Formato do papel", ShortName = "Formato")]
        public string Formato { get; set; }

        [DataType(DataType.Date)]
        private DateTime _DataSolicitacao;
        [Display(Name = "Data de Solicitação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSolicitacao
        {
            get
            {
                return _DataSolicitacao;
            }
            set
            {
                _DataSolicitacao = value.Date;
            }
        }

        [DataType(DataType.Date)]
        private DateTime _DataEntrega;
        [Required]
        [Display(Name = "Data esperada para a entrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataEntrega
        {
            get
            {
                return _DataEntrega;
            }
            set
            {
                _DataEntrega = value.Date;
            }
        }

        [Display(Name = "Suporte")]
        public string Suporte { get; set; }

        [ForeignKey("User")]
        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual User User { get; set; }

        public virtual Avaliacao Avaliacao { get; set; }

        public virtual ICollection<Item> Itens { get; set; }

        [Required(ErrorMessage = "Área deve ser preenchida")]
        public int AreaId { get; set; }
        [Display(Name = "Área (Curso)")]
        public virtual Area Area { get; set; }

        [Required(ErrorMessage = "CC e CM são necessários")]
        public int CodificacaoId { get; set; }
        [Display(Name = "Centro de Custo + Conta Memo")]
        public virtual Codificacao Codificacao { get; set; }

        public string AnoSeq
        {
            get
            {
                return String.Format("{0}-{1}", this.Ano.ToString(), this.Seq.ToString());
            }
        }

        [Display(Name="Comentário")]
        public string Comment { get; set; }
    }
}
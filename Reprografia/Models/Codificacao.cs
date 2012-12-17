using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class Codificacao
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Centro de Custo")]
        public int CentroDeCusto { get; set; }
        [Display(Name = "Centro de Custo - Descrição")]
        public string DescricaoCentroDeCusto { get; set; }
        [Display(Name = "Conta Memo")]
        public int ContaMemo { get; set; }
        [Display(Name = "Conta Memo - Descrição")]
        public string DescricaoContaMemo { get; set; }
    }
}

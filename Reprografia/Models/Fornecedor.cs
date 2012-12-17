using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class Fornecedor
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }


    }
}

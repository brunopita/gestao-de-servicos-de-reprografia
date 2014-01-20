using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class Area
    {
        public Area(string nome, string nomeExcel = "")
            : this()
        {
            if (nomeExcel == "")
            {
                nomeExcel = nome;
            }
            this.Nome = nome;
            this.NomeExcel = NomeExcel;
        }
        public Area() { }

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Nome { get; set; }
        [ScaffoldColumn(false)]
        public string NomeExcel { get; set; }
    }
}
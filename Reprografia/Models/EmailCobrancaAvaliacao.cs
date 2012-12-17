using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Reprografia.Models
{
    public class EmailCobrancaAvaliacao
    {
        public EmailCobrancaAvaliacao(Avaliacao avaliacao)
            : this()
        {
            this.Avaliacao = avaliacao;
        }

        public EmailCobrancaAvaliacao()
        {
            this.EnviadoEm = new DateTime(1900, 1, 1);
            this.GeradoEm = DateTime.Now;
        }


        public int Id { get; set; }

        public DateTime GeradoEm { get; set; }
        public DateTime EnviadoEm { get; set; }

        public bool Enviado { get; set; }

        public virtual Avaliacao Avaliacao { get; set; }
    }
}
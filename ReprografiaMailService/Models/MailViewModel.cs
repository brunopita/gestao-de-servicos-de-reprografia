using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ReprografiaMailService.Models
{
    public class MailViewModel
    {
        public string AvaliacaoSolicitacaoUserFullName { get; set; }

        public int AvaliacaoSolicitacaoAno { get; set; }
        [DisplayFormat(DataFormatString = "{0:000}")]
        public int AvaliacaoSolicitacaoSeq { get; set; }

        public int AvaliacaoId { get; set; }

        public int AvaliacaoSolicitacaoId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AvaliacaoDataLimite { get; set; }

        public string AvaliacaoSolicitacaoUserEmail { get; set; }
    }
}

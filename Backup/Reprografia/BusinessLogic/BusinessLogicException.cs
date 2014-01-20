using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reprografia.BusinessLogic
{
    class BusinessLogicException : Exception
    {
        public new string Message { get; set; }
        public StatusCriacaoSolicitacao Razao { get; set; }

        public BusinessLogicException(string message, StatusCriacaoSolicitacao razao = BusinessLogic.StatusCriacaoSolicitacao.NaoEspecificado)
        {
            this.Message = message;
            this.Razao = razao;
        }

    }
}

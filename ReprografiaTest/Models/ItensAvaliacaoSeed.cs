using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reprografia.Models;

namespace ReprografiaTest.Models
{
    static class ItensAvaliacaoSeed
    {
        public static ItemAvaliacao Boa
        {
            get
            {
                return new ItemAvaliacao
                {
                    Acabamento = "A",
                    Matriz = "A",
                    Nitidez = "A",
                    Paginacao = "A",
                    Prazo = "A",
                    Quantidade = "A"
                };
            }
        }

        public static ItemAvaliacao Media
        {
            get
            {
                return new ItemAvaliacao
                {
                    Acabamento = "A",
                    Matriz = "A",
                    Nitidez = "A",
                    Paginacao = "X",
                    Prazo = "N",
                    Quantidade = "A"
                };
            }
        }

        public static ItemAvaliacao Ruim
        {
            get
            {
                return new ItemAvaliacao
                {
                    Acabamento = "X",
                    Matriz = "X",
                    Nitidez = "X",
                    Paginacao = "X",
                    Prazo = "X",
                    Quantidade = "X"
                };
            }
        }
    }
}

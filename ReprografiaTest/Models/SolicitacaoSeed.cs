using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reprografia.Models;
using Reprografia.Models.Account;

namespace ReprografiaTest.Models
{
    static class SolicitacaoSeed
    {
        public static Solicitacao ApostilasNR10
        {
            get
            {
                var solicitacao = new Solicitacao();
                solicitacao.User = UserSeed.Paolo;
                solicitacao.UserName = "sn1018302";
                solicitacao.Fornecedor = new Fornecedor() { Id = 1, Nome = "Fornecedor" };
                solicitacao.Codificacao = new Codificacao()
                {
                    Id = 1,
                    CentroDeCusto = 0,
                    ContaMemo = 0,
                    DescricaoCentroDeCusto = "CC",
                    DescricaoContaMemo = "CM"
                };
                solicitacao.Area = new Area("Curso técnico", "CT");
                solicitacao.Formato = "A4";
                solicitacao.Suporte = "E-mail";
                solicitacao.Seq = 1;
                solicitacao.Ano = DateTime.Now.Year;
                solicitacao.Itens = new[] 
                { 
                    new Item { Descricao = "Apostila de NR 10 GMT", Espiral = true, CapaEmPVC = true, FrenteVerso = true , Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Avaliação NR 10 GMT", FrenteVerso = true, Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Exercícios GMT", FrenteVerso = true, Copias= 5, Paginas = 5} 
                };

                solicitacao.Avaliacao = new Avaliacao()
                {
                    ItensAvaliacao = new[] {
                        ItensAvaliacaoSeed.Boa,
                        ItensAvaliacaoSeed.Media,
                        ItensAvaliacaoSeed.Ruim
                    },
                    Avaliado = true,
                    DataAvaliado = new DateTime(2013, 04, 02),
                    DataLimite = new DateTime(2013, 04, 05),
                    Acao = "Ação corretiva"
                };

                return solicitacao;
            }
        }



    }
}

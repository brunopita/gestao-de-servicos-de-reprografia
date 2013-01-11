using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Reprografia.Models.Account;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Reprografia.Models;
using Reprografia.BusinessLogic;

namespace Reprografia.Data
{
    public class ReprografiaInitializer
        : DropCreateDatabaseIfModelChanges<ReprografiaContext>
    //: DropCreateDatabaseAlways<ReprografiaContext>
    {
        protected override void Seed(ReprografiaContext context)
        {
            #region Roles
            var admin = new Role("Administrator");
            var normalUser = new Role("user");
            #endregion

            #region Users
            var paolo = new User("sn1018302")
            {
                FullName = "Paolo Haji de Carvalho Bueno",
                Email = "paolohaji@gmail.com",
                Password = "Senai115"
            };
            var fulano = new User("fulano")
            {
                FullName = "Fulano da Silva",
                Email = "paolohaji@gmail.com",
                Password = "Senai115"
            };
            admin.Users.Add(paolo);
            normalUser.Users.Add(fulano);

            context.Roles.Add(admin);
            context.Users.Add(fulano);

            context.Users.Add(paolo);
            context.Users.Add(fulano);

            context.SaveChanges();
            #endregion

            #region Fornecedores
            var forn1 = new Fornecedor
                {
                    Nome = "Copiadora Módulo LTDA."
                };

            context.Fornecedores.Add(forn1);
            #endregion

            #region Areas
            Area[] areas = new[] { 
                            new Area ("CT")
                            , new Area("CAI")
                            , new Area("FC")
                            , new Area("CST")
                            , new Area("Pós Graduação", "Pos")
                            , new Area("Olimpíada do Conhecimento", "Especificar")
                            , new Area("Inova SENAI", "Especificar" )
                            , new Area("Copa SENAI", "Especificar")
                        };
            foreach (Area item in areas)
                context.Areas.Add(item);
            #endregion

            #region Codificacoes
            var codificacoes = new[]{
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Marketing Promocional", ContaMemo = 170201, DescricaoContaMemo = "Programa SENAI Casa Aberta"},
                new Codificacao(){CentroDeCusto=11521, DescricaoCentroDeCusto = "Suporte Administrativo/Financeiro", ContaMemo = 170300, DescricaoContaMemo = "Administracao das Unidades Operacionais - SENAI"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 600100, DescricaoContaMemo = "Formacao de Tecnologo"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 171101, DescricaoContaMemo = "Suporte Tecnico/Operacional - Aprendiz. Industrial"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 171103, DescricaoContaMemo = "Suporte Tecnico/Operacional - Curso Tecnico"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 400101, DescricaoContaMemo = "Iniciacao Profissional"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 400102, DescricaoContaMemo = "Iniciacao Profissional - PCFP"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 400105, DescricaoContaMemo = "Iniciacao Prof. - CIPA - Fomento SESI/DN"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401501, DescricaoContaMemo = "Qualificacao Prof. Nivel Basico - Escola"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401502, DescricaoContaMemo = "Qualificacao Prof. Nivel Basico - Empresa"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401506, DescricaoContaMemo = "Qualificacao Prof. - Contrato SENAI/SDECT"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401505, DescricaoContaMemo = "Qualificacao Prof. - Contrato SENAI/SERT"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401503, DescricaoContaMemo = "Qualificacao Prof. Nivel Basico - Entidade"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 401504, DescricaoContaMemo = "Qualificacao Prof. Nivel Basico - PCFP"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 403001, DescricaoContaMemo = "Aperfeicoamento Prof. Nivel Basico - Escola"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 403002, DescricaoContaMemo = "Aperfeicoamento Prof. Nivel Basico - Empresa"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 403003, DescricaoContaMemo = "Aperfeicoamento Prof. Nivel Basico - Entidade"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 403004, DescricaoContaMemo = "Aperfeicoamento Prof. Nivel Basico - PCFP"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 404501, DescricaoContaMemo = "Especializacao Prof. Nivel Basico - Escola"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 404502, DescricaoContaMemo = "Especializacao Prof. Nivel Basico - Empresa"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 404503, DescricaoContaMemo = "Especializacao Prof. Nivel Basico - Entidade"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 404504, DescricaoContaMemo = "Especializacao Prof. Nivel Basico - PCFP"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 500101, DescricaoContaMemo = "Tecnico de Nivel Medio - Escola"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 500102, DescricaoContaMemo = "Tecnico de Nivel Medio - Empresa"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 500103, DescricaoContaMemo = "Tecnico de Nivel Medio - Entidade"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 500104, DescricaoContaMemo = "Tecnico de Nivel Medio - Projeto Especial"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 682000, DescricaoContaMemo = "Telecurso 2000 - Prestacao Servicos Educacionais"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 772100, DescricaoContaMemo = "Assessoria e Consultoria em Educacao"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 710000, DescricaoContaMemo = "Asses Tec/Tecnol - Gestao Empresarial"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 735000, DescricaoContaMemo = "Elaboracao/Disseminacao Informacoes (Serv. Docum.)"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 760000, DescricaoContaMemo = "Serv. Tec. Labor. - Calibracao Dimensional"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 773000, DescricaoContaMemo = "Desenvolv. Tecnologico - Pesquisa Aplicada"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 774000, DescricaoContaMemo = "Desenvolv. Tecnologico - Desenvolv. Experimental"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 775000, DescricaoContaMemo = "Desenvolv. Tecnologico - Design"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 755003, DescricaoContaMemo = "Gestao Estrategica do Design"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 163901, DescricaoContaMemo = "Supervisao de Projetos e de Infraestrutura"},
                new Codificacao(){CentroDeCusto=11541, DescricaoCentroDeCusto = "Educacao e Tecnologia", ContaMemo = 700101, DescricaoContaMemo = "Acao Global Regional"}
            };

            foreach (var codificacao in codificacoes)
            {
                context.Codificacoes.Add(codificacao);
            }
            #endregion

            #region Solicitacoes

            Solicitacao sol1 = SolicitacaoBL.CriarSolicitacao(paolo);
            sol1.User = paolo;
            sol1.Formato = "A4";
            sol1.Suporte = "E-mail";
            sol1.Fornecedor = forn1;
            sol1.Codificacao = codificacoes[0];
            sol1.Area = areas[0];
            sol1.Seq = 1;
            sol1.Ano = DateTime.Now.Year;
            sol1.Itens = new[] 
                { 
                    new Item { Descricao = "Apostila de NR 10 GMT", Espiral = true, CapaEmPVC = true, FrenteVerso = true , Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Avaliação NR 10 GMT", FrenteVerso = true, Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Exercícios GMT", FrenteVerso = true, Copias= 5, Paginas = 5} 
                };

            Solicitacao sol2 = SolicitacaoBL.CriarSolicitacao(fulano);
            sol2.User = fulano;
            sol2.Formato = "A4";
            sol2.Suporte = "E-mail";
            sol2.Fornecedor = forn1;
            sol2.Codificacao = codificacoes[0];
            sol2.Area = areas[0];
            sol2.Seq = 1;
            sol2.Ano = DateTime.Now.Year;
            sol2.Itens = new[] 
                { 
                    new Item { Descricao = "Apostila de NR 10 GMT", Espiral = true, CapaEmPVC = true, FrenteVerso = true, Copias= 5, Paginas = 5 }, 
                    new Item { Descricao = "Avaliação NR 10 GMT", FrenteVerso = true, Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Exercícios GMT", FrenteVerso = true , Copias= 5, Paginas = 5} 
                };

            context.Solicitacoes.Add(sol1);
            context.Solicitacoes.Add(sol2);
            context.SaveChanges();
            #endregion

            #region Avaliacoes
            var ava1 = AvaliacaoBL.CriarAvaliacao(sol1);
            var ava2 = AvaliacaoBL.CriarAvaliacao(sol2);
            context.Avaliacoes.Add(ava1);
            context.Avaliacoes.Add(ava2);

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
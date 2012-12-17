using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReprografiaMailService;
using Reprografia.Models;
using System.Net.Mail;
using ReprografiaMailService.Models;
using Reprografia.Models.Account;

namespace ReprografiaMailServiceTest
{
    /// <summary>
    /// Summary description for ProgramTest
    /// </summary>
    [TestClass]
    public class ProgramTest
    {
        private static EmailCobrancaAvaliacao GetEmailCompleto()
        {
            EmailCobrancaAvaliacao emailCompleto = new EmailCobrancaAvaliacao()
            {
                Avaliacao = new Avaliacao()
                {
                    Id = 1,
                    Solicitacao = new Solicitacao()
                    {
                        Id = 1,
                        Seq = 1,
                        Ano = 2012,
                        User = new User() { Email = "paolohaji@gmail.com", FullName = "Paolo Haji de Carvalho Bueno" }
                    },
                    DataLimite = DateTime.Now,
                    SolicitacaoId = 1
                },
            };
            return emailCompleto;
        }
        public ProgramTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion



        /// <summary>
        ///A test for EnviarEmails
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ReprografiaMailService.exe")]
        public void EnviarEmailsTest()
        {
            EmailCobrancaAvaliacao emailCompleto = GetEmailCompleto();
            IEnumerable<EmailCobrancaAvaliacao> emails = new EmailCobrancaAvaliacao[]{
                emailCompleto
            };
            Program_Accessor.EnviarEmails(emails);
            foreach (var email in emails)
            {
                Assert.IsTrue(email.Enviado, "Email deve ser marcado como enviado");
                Assert.IsTrue(DateTime.Now.Subtract(email.EnviadoEm) < new TimeSpan(0, 2, 0),
                    "Tempo desde o envio deve ser menor que 2 min");
            }
        }

        /// <summary>
        ///A test for EnviarEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ReprografiaMailService.exe")]
        public void EnviarEmailTest()
        {
            string templateText = "Enviando email para @Model.AvaliacaoSolicitacaoUserEmail";
            MailViewModel email = new MailViewModel()
            {
                AvaliacaoSolicitacaoUserEmail = "paolohaji@gmail.com",
                AvaliacaoSolicitacaoAno = 2012,
                AvaliacaoSolicitacaoSeq = 1,
                AvaliacaoSolicitacaoId = 1,
                AvaliacaoId = 1,
                AvaliacaoDataLimite = DateTime.Now.AddDays(2),
                AvaliacaoSolicitacaoUserFullName = "Paolo Haji de Carvalho Bueno"
            };
            bool expected = true;
            bool actual = false;
            actual = Program_Accessor.EnviarEmail(templateText, email);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for MapViewModel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ReprografiaMailService.exe")]
        public void MapViewModelTest()
        {

            EmailCobrancaAvaliacao email = GetEmailCompleto();
            MailViewModel actual;
            actual = Program_Accessor.MapViewModel(email);
            Assert.AreEqual(actual.AvaliacaoSolicitacaoAno, email.Avaliacao.Solicitacao.Ano);
            Assert.AreEqual(actual.AvaliacaoSolicitacaoSeq, email.Avaliacao.Solicitacao.Seq);
            Assert.AreEqual(actual.AvaliacaoDataLimite, email.Avaliacao.DataLimite);
            Assert.AreEqual(actual.AvaliacaoSolicitacaoUserFullName, email.Avaliacao.Solicitacao.User.FullName);
            Assert.AreEqual(actual.AvaliacaoId, email.Avaliacao.Id);
            Assert.AreEqual(actual.AvaliacaoSolicitacaoUserEmail, email.Avaliacao.Solicitacao.User.Email);
            Assert.AreEqual(actual.AvaliacaoSolicitacaoId, email.Avaliacao.Solicitacao.Id);


        }
    }
}

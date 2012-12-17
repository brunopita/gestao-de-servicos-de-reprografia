using Reprografia.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Reprografia.Models;

namespace ReprografiaTest
{


    /// <summary>
    ///This is a test class for SolicitacaoIndexModelTest and is intended
    ///to contain all SolicitacaoIndexModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SolicitacaoIndexModelTest
    {


        private static Solicitacao sol1;
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            sol1 = new Solicitacao();
            //sol1.User = paolo;
            sol1.Formato = "A4";
            sol1.Suporte = "E-mail";
            //sol1.Fornecedor = forn1;
            //sol1.Codificacao = codificacoes[0];
            //sol1.Area = areas[0];
            sol1.Seq = 1;
            sol1.Ano = DateTime.Now.Year;
            sol1.Itens = new[] 
                { 
                    new Item { Descricao = "Apostila de NR 10 GMT", Espiral = true, CapaEmPVC = true, FrenteVerso = true , Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Avaliação NR 10 GMT", FrenteVerso = true, Copias= 5, Paginas = 5}, 
                    new Item { Descricao = "Exercícios GMT", FrenteVerso = true, Copias= 5, Paginas = 5} 
                };
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SolicitacaoIndexModel Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Paolo\\documents\\visual studio 2010\\Projects\\Reprografia\\Reprografia", "/")]
        [UrlToTest("http://localhost:1576/")]
        public void SolicitacaoIndexModelConstructorTest()
        {
            Solicitacao solicitacao = null; // TODO: Initialize to an appropriate value
            SolicitacaoIndexModel target = new SolicitacaoIndexModel(solicitacao);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for InsertFrom
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\Paolo\\documents\\visual studio 2010\\Projects\\Reprografia\\Reprografia", "/")]
        [UrlToTest("http://localhost:1576/")]
        public void InsertFromTest()
        {
            SolicitacaoIndexModel target = new SolicitacaoIndexModel(); // TODO: Initialize to an appropriate value
            Solicitacao solicitacao = null;
            target.InsertFrom(solicitacao);
            Assert.AreEqual(target.Ano, solicitacao.Ano);


            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}

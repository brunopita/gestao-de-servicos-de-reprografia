using Reprografia.Models.ViewModels;
using Reprografia.Models.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Reprografia.Models;

namespace ReprografiaTest.Models.ViewModels
{


    /// <summary>
    ///This is a test class for SolicitacaoIndexModelTest and is intended
    ///to contain all SolicitacaoIndexModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SolicitacaoIndexModelTest
    {
        private static Solicitacao solicitacao;
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

        [TestInitialize()]
        public static void Initialize(TestContext testContext)
        {
            solicitacao = Models.SolicitacaoSeed.solicitacao;
        }


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
            SolicitacaoIndexModel target = new SolicitacaoIndexModel(solicitacao);
            Assert.AreEqual(solicitacao.Ano, target.Ano);
            Assert.AreEqual(solicitacao.AnoSeq, target.AnoSeq);
            Assert.AreEqual(solicitacao.Area, target.UserName);
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
            target.InsertFrom(solicitacao);
            Assert.AreEqual(solicitacao.User.FullName, target.UserFullName);
            Assert.AreEqual(target.Ano, solicitacao.Ano);
        }
    }
}

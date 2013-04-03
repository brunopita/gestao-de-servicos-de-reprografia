using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reprografia.Models;
using Reprografia.Data.XL;

namespace ReprografiaTest.Data.XL
{
    [TestClass]
    public class SolicitacaoWriterStrategyTest
    {

        [TestMethod]
        public void AreasFillTest()
        {
            var s = Models.SolicitacaoSeed.ApostilasNR10;
            var areas = new String[] { "CAI", "CT", "FC", "CST", "Pos" };

            foreach (string area in areas)
            {
                s.Area = new Area(area, area);
                SolicitacaoWriterStrategy sw = new SolicitacaoWriterStrategy(s);
                Assert.AreEqual(sw.Values[area], "X", "Area da solicitação deve ser preenchida!");
            }

        }
    }
}

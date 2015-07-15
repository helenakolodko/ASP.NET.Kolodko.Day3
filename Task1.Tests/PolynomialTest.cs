using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1.Library;

namespace Task1.Tests
{
    [TestClass]
    public class PolynomialTest
    {
        [TestMethod]
        public void Add_TwoPolynomials_ReturnsNewPolynomial()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 8, 9.11, 5 });
            p = p.Add(pp);
            Assert.AreNotSame(p, pp);
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalsel()
        {
            Polynomial p = new Polynomial(new double[]{0, 5.6, 5, 0.5, 0, 0, 0});
	        Assert.IsFalse(p.Equals(null));
        }
    }
}

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
            Polynomial result = Polynomial.Add(p, pp);
            Assert.AreNotSame(p, result);
            Assert.AreNotSame(pp, result);
        }

        [TestMethod]
        public void Add_TwoPolynomials_ReturnsPolynomialWithCorrectValue()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 8, 9.11, 5 });
            p = Polynomial.Add(p, pp);
            Assert.AreEqual(44.31, p.GetValue(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullToPolynomial_ThrowsException()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial result = Polynomial.Add(p, null);
            Assert.AreSame(p, result);
        }

        [TestMethod]
        public void Subtract_TwoPolynomials_ReturnsNewPolynomial()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 8, 9.11, 5 });
            Polynomial result = Polynomial.Subtract(p, pp);
            Assert.AreNotSame(p, result);
            Assert.AreNotSame(pp, result);
        }

        [TestMethod]
        public void Subtract_TwoPolynomials_ReturnsPolynomialWithCorrectValue()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 8, 9.11, 5 });
            p = Polynomial.Subtract(p, pp);
            Assert.AreEqual(-22.11, p.GetValue(1));
        }

        [TestMethod]
        public void Multiply_ByOne_ReturnsNewPolynomial()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial result = Polynomial.Multiply(p, 1);
            Assert.AreNotSame(p, result);
        }

        [TestMethod]
        public void Multiply_ByOne_ReturnsEqualPolynomial()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial result = Polynomial.Multiply(p, 1);
            Assert.IsTrue(p == result);
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            Polynomial p = new Polynomial(new double[]{0, 5.6, 5, 0.5, 0, 0, 0});
	        Assert.IsFalse(p.Equals(null));
        }

        [TestMethod]
        public void GetHashCode_OfTheSamePolynomialObjects_ReturnsTheSameResult()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Assert.AreEqual(p.GetHashCode(), p.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_OfEqualPolynomials_ReturnsTheSameResult()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Assert.AreEqual(p.GetHashCode(), pp.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_OfPolynomialsObject_ReturnsCorrectValue()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Assert.AreEqual(1717986534, p.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_OfTwoNonEqualPolynomialsObject_ReturnDiffernValues()
        {
            Polynomial p = new Polynomial(new double[] { 0, 5.6, 5, 0.5, 0, 0, 0 });
            Polynomial pp = new Polynomial(new double[] { 0, 5.6, 5.0000001, 0.5, 0, 0, 0 });
            Assert.AreNotEqual(pp.GetHashCode(), p.GetHashCode());
        }
    }
}

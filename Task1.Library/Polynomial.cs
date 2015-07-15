using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Task1.Library
{
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private readonly double[] coefficients;
        private int degree;
        public int Degree { get { return degree; } }
        public double this[int i]
        {
            get
            {
                if (i > degree)
                    return 0;
                else
                    return coefficients[i];
            }
        }

        public Polynomial(IEnumerable<double> coefficients)
        {
            double[] temp = coefficients.ToArray<double>();
            degree = temp.Length - 1;
            while (temp[degree] == 0)
            {
                degree--;
            }
            this.coefficients = new double[degree + 1];
            Array.Copy(temp, this.coefficients, degree + 1);
        }

        public Polynomial(Polynomial otherPolynomial)
        {
            degree = otherPolynomial.degree;
            coefficients = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                coefficients[i] = otherPolynomial[i];
            }
        }

        public double GetValue(double variableValue)
        {
            double result = 0;
            for (int i = 0; i <= degree; i++)
            {
                result += coefficients[i] * Math.Pow(variableValue, i);
            }
            return result;
        }

        public Polynomial Add(Polynomial otherPolynomial)
        {
            int newDegree = Math.Max(degree, otherPolynomial.degree);
            double[] newCoefficients = new double[newDegree + 1];
            for (int i = 0; i <= newDegree; i++)
            {
                newCoefficients[i] = this[i] + otherPolynomial[i];
            }
            return new Polynomial(newCoefficients);
        }

        public Polynomial Add(double value)
        {

        }

        public Polynomial Subtract(Polynomial otherPolynomial)
        {

        }

        public Polynomial Subtract(double value)
        {

        }

        public Polynomial Multiply(Polynomial otherPolynomial)
        {

        }
        public Polynomial Multiply(double value)
        {

        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Polynomial other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            return false;
        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {
            return true;
        }
    }
}

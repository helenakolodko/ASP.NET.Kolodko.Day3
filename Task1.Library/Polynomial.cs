using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Task1.Library
{
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private readonly double[] coefficients;
        private readonly int degree;
        private readonly int minDegree;
        public int Degree { get { return degree; } }
        public double this[int i]
        {
            get
            {
                if (i > degree || i < minDegree)
                    return 0;
                else 
                    return coefficients[i - minDegree];
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

        public Polynomial(IEnumerable<double> coefficients, int minDegree)
        {

            double[] temp = coefficients.ToArray<double>();
            degree = temp.Length - 1 + minDegree;
            while (temp[degree - minDegree] == 0)
            {
                degree--;
            }
            this.minDegree = minDegree;
            while (temp[this.minDegree - minDegree] == 0)
            {
                this.minDegree++;
            }
            this.coefficients = new double[degree + 1 - minDegree];
            Array.Copy(temp, this.minDegree - minDegree, this.coefficients, 0, degree + 1 - this.minDegree);
        }

        public Polynomial(Polynomial otherPolynomial)
        {
            degree = otherPolynomial.degree;
            minDegree = otherPolynomial.minDegree;
            coefficients = new double[degree + 1 - minDegree];
            for (int i = 0; i <= degree - minDegree; i++)
            {
                coefficients[i] = otherPolynomial[i + minDegree];
            }
        }

        public double GetValue(double variableValue)
        {
            double result = 0;
            for (int i = minDegree; i <= degree; i++)
            {
                result += this[i] * Math.Pow(variableValue, i);
            }
            return result;
        }

        public Polynomial Add(Polynomial otherPolynomial)
        {
            return NewWithPolynomial(otherPolynomial, (x, y) => x + y);
        }     

        public Polynomial AddAll(double value)
        {
            return NewWithConstant((x) => x + value);
        }

        public Polynomial Subtract(Polynomial otherPolynomial)
        {
            return NewWithPolynomial(otherPolynomial, (x, y) => x - y);
        }

        public Polynomial SubtractAll(double value)
        {
            return NewWithConstant((x) => x - value);
        }

        public Polynomial Multiply(double value)
        {
            return NewWithConstant((x) => x * value);
        }

        public object Clone()
        {
            return new Polynomial(this);
        }

        public bool Equals(Polynomial other)
        {
            if ((object)other == null)
            {
                return false;
            }
            int maxDegree = Math.Max(degree, other.degree);
            bool equals = true;
            for (int i =  Math.Min(minDegree, other.minDegree); i <= maxDegree; i++)
            {
                if (this[i] != other[i])
                {
                    equals = false;
                }
            }
            return equals;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                Polynomial p = obj as Polynomial;
                if (p != null)
                {
                    return Equals(p);
                }
            }
            return false;

        }

        public override int GetHashCode()
        {
            int result = (int)BitConverter.DoubleToInt64Bits(this[minDegree]);
            for (int i = minDegree + 1; i <= degree; i++)
            {
                result *= 31;
                result += (int)BitConverter.DoubleToInt64Bits(this[i]);
            }
            return result;
        }

        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if ((object)p1 == null)
            {
                return false;
            }
            return p1.Equals(p2);
        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {
            if ((object)p1 == null)
            {
                return false;
            }
            return !p1.Equals(p2);
        }

        private Polynomial NewWithPolynomial(Polynomial otherPolynomial, Func<double, double, double> operation)
        {
            int newDegree = Math.Max(degree, otherPolynomial.degree);
            int newMinDegree = Math.Min(minDegree, otherPolynomial.minDegree);
            double[] newCoefficients = new double[newDegree + 1 - newMinDegree];
            for (int i = newMinDegree; i <= newDegree; i++)
            {
                newCoefficients[i - newMinDegree] = operation(this[i], otherPolynomial[i]);
            }
            return new Polynomial(newCoefficients, newMinDegree);
        }

        private Polynomial NewWithConstant(Func<double, double> operation)
        {
            double[] newCoefficients = new double[degree + 1 - minDegree];
            for (int i = minDegree; i <= degree; i++)
            {
                newCoefficients[i] = operation(coefficients[i]);
            }
            return new Polynomial(newCoefficients, minDegree);
        }
    }
}

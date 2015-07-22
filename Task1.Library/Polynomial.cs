using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Task1.Library
{
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private double[] coefficients = {};
        private static double epsilon = .00001;
        public static double Epsilon { get { return epsilon; } }
        public int Degree { get { return coefficients.Length - 1; } }

        public double this[int i]
        {
            get
            {
                if (i > coefficients.Length - 1 || i < 0)
                    throw new ArgumentOutOfRangeException();
                else 
                    return coefficients[i];
            }
        }

        public Polynomial(IEnumerable<double> coefficients)
            : this(coefficients.ToArray<double>())
        {
        }

        public Polynomial(params double[] coefficients)
        {
            if (ReferenceEquals(coefficients, null))
                throw new ArgumentNullException();
            int degree = coefficients.Length - 1;
            while (CompareTwoDoublesWithEpsilon(coefficients[degree], 0, Epsilon) == 0)
            {
                degree--;
            }
            this.coefficients = new double[degree + 1];
            Array.Copy(coefficients, this.coefficients, degree + 1);
        }

        public Polynomial(Polynomial other)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();
            int degree = other.Degree;
            coefficients = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                coefficients[i] = other[i];
            }
        }

        public double GetValue(double variableValue)
        {
            double result = 0;
            for (int i = 0; i <= Degree; i++)
            {
                result += this[i] * Math.Pow(variableValue, i);
            }
            return result;
        }

        public static Polynomial Add(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null))
                throw new ArgumentNullException();
            return p1.NewWithPolynomial(p2, (x, y) => x + y);
        }


        public static Polynomial Subtract(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, null))
                throw new ArgumentNullException();
            return p1.NewWithPolynomial(p2, (x, y) => x - y);
        }

        public static Polynomial Multiply(Polynomial p1, double value)
        {
            if (ReferenceEquals(p1, null))
                throw new ArgumentNullException();
            return p1.NewWithConstant((x) => x * value);
        }

        object ICloneable.Clone()
        {
            return new Polynomial(this);
        }

        public Polynomial Clone()
        {
            return new Polynomial(this);
        }

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;
            if (Degree != other.Degree)
                return false;
            for (int i = 0; i <= Degree; i++)
                if (CompareTwoDoublesWithEpsilon(coefficients[i], other.coefficients[i], Epsilon) != 0)
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            Polynomial p = obj as Polynomial;
            if (p != null)
                return Equals(p);
            return false;
        }

        public override int GetHashCode()
        {
            int result = (int)BitConverter.DoubleToInt64Bits(this[0]);
            for (int i = 0; i <= Degree; i++)
            {
                result *= 31;
                result += (int)BitConverter.DoubleToInt64Bits(this[i]);
            }
            return result;
        }

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                throw new ArgumentNullException();
            return Add(lhs, rhs);
        }

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                throw new ArgumentNullException();
            return Subtract(lhs, rhs);
        }

        public static Polynomial operator *(Polynomial lhs, int rhs)
        {
            if (ReferenceEquals(lhs, null))
                throw new ArgumentNullException();
            return Multiply(lhs, rhs);
        }

        public static Polynomial operator +(int lhs, Polynomial rhs)
        {
            return rhs * lhs;
        }

        public static bool operator ==(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Polynomial lhs, Polynomial rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return false;
            if (ReferenceEquals(lhs, null))
                return true;
            return !lhs.Equals(rhs);
        }

        private int CompareTwoDoublesWithEpsilon(double d1, double d2, double epsilon)
        {
            if (Math.Abs(d1 - d2) <= epsilon)
                return 0;
            else if (d1 > d2)
                return 1;
            else
                return -1;

        }

        private Polynomial NewWithPolynomial(Polynomial other, Func<double, double, double> operation)
        {
            if (ReferenceEquals(other, null))
                throw new ArgumentNullException();
            int max = Math.Max(Degree, other.Degree);
            double[] newCoefficients = new double[max + 1];
            for (int i = 0; i <= max; i++)
            {
                newCoefficients[i] = operation(i > Degree ? 0 : coefficients[i], i > other.Degree ? 0 : other.coefficients[i]);
            }
            return new Polynomial(newCoefficients);
        }

        private Polynomial NewWithConstant(Func<double, double> operation)
        {
            double[] newCoefficients = new double[Degree + 1];
            for (int i = 0; i <= Degree; i++)
            {
                newCoefficients[i] = operation(coefficients[i]);
            }
            return new Polynomial(newCoefficients);
        }
    }
}

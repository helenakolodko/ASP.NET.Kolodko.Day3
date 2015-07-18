using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Task1.Library
{
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private double[] coefficients;
        private int degree;
        public int Degree { get { return degree; } private set { degree = value; } }

        public double this[int i]
        {
            get
            {
                if (i > degree || i < 0)
                    return 0;
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
            Degree = coefficients.Length - 1;
            while (coefficients[Degree] == 0)
            {
                Degree--;
            }
            this.coefficients = new double[Degree + 1];
            Array.Copy(coefficients, this.coefficients, Degree + 1);
        }

        public Polynomial(Polynomial other)
        {
            degree = other.Degree;
            coefficients = new double[Degree + 1];
            for (int i = 0; i <= Degree; i++)
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

        public Polynomial Add(Polynomial other)
        {
            return NewWithPolynomial(other, (x, y) => x + y);
        }     

        public Polynomial AddAll(double value)
        {
            return NewWithConstant((x) => x + value);
        }

        public Polynomial Subtract(Polynomial other)
        {
            return NewWithPolynomial(other, (x, y) => x - y);
        }

        public Polynomial SubtractAll(double value)
        {
            return NewWithConstant((x) => x - value);
        }

        public Polynomial Multiply(double value)
        {
            return NewWithConstant((x) => x * value);
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
            if ((object)other == null)
            {
                return false;
            }
            int max = Math.Max(Degree, other.Degree);
            bool equals = true;
            for (int i = 0; i <= max; i++)
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
            int result = (int)BitConverter.DoubleToInt64Bits(this[0]);
            for (int i = 0; i <= Degree; i++)
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

        private Polynomial NewWithPolynomial(Polynomial other, Func<double, double, double> operation)
        {
            int max = Math.Max(Degree, other.Degree);
            double[] newCoefficients = new double[max + 1];
            for (int i = 0; i <= max; i++)
            {
                newCoefficients[i] = operation(this[i], other[i]);
            }
            return new Polynomial(newCoefficients);
        }

        private Polynomial NewWithConstant(Func<double, double> operation)
        {
            double[] newCoefficients = new double[Degree + 1];
            for (int i = 0; i <= Degree; i++)
            {
                newCoefficients[i] = operation(this[i]);
            }
            return new Polynomial(newCoefficients);
        }
    }
}

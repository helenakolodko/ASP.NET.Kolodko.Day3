using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Task1.Library
{
    public sealed class Polynomial : IEquatable<Polynomial>, ICloneable
    {
        private double[] coefficients;
        private int maxDegree;
        private int minDegree;
        public int MaxDegree { get { return maxDegree; } set { maxDegree = value; } }
        public int MinDegree { get { return minDegree; } set { minDegree = value; } }

        public double this[int i]
        {
            get
            {
                if (i > maxDegree || i < minDegree)
                    return 0;
                else 
                    return coefficients[i - minDegree];
            }
        }

        public Polynomial(IEnumerable<double> coefficients)
            : this(coefficients, 0)
        {
        }

        public Polynomial(IEnumerable<double> coefficients, int minDegree)
        {

            double[] temp = coefficients.ToArray<double>();
            MaxDegree = temp.Length - 1 + minDegree;
            while (temp[MaxDegree - minDegree] == 0)
            {
                MaxDegree--;
            }
            this.MinDegree = minDegree;
            while (temp[MinDegree - minDegree] == 0)
            {
                MinDegree++;
            }
            this.coefficients = new double[MaxDegree + 1 - MinDegree];
            Array.Copy(temp, MinDegree - minDegree, this.coefficients, 0, MaxDegree + 1 - MinDegree);
        }

        public Polynomial(Polynomial other)
        {
            maxDegree = other.MaxDegree;
            minDegree = other.MinDegree;
            coefficients = new double[MaxDegree + 1 - MinDegree];
            for (int i = 0; i <= MaxDegree - MinDegree; i++)
            {
                coefficients[i] = other[i + MinDegree];
            }
        }

        public double GetValue(double variableValue)
        {
            double result = 0;
            for (int i = MinDegree; i <= MaxDegree; i++)
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

        public object Clone()
        {
            return new Polynomial(this);
        }

        //public Polynomial Clone()
        //{
        //    return new Polynomial(this);
        //}

        public bool Equals(Polynomial other)
        {
            if ((object)other == null)
            {
                return false;
            }
            int max = Math.Max(MaxDegree, other.MaxDegree);
            bool equals = true;
            for (int i = Math.Min(MinDegree, other.MinDegree); i <= max; i++)
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
            int result = (int)BitConverter.DoubleToInt64Bits(this[MinDegree]);
            for (int i = MinDegree + 1; i <= MaxDegree; i++)
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
            int max = Math.Max(MaxDegree, other.MaxDegree);
            int min = Math.Min(MinDegree, other.MinDegree);
            double[] newCoefficients = new double[max + 1 - min];
            for (int i = min; i <= max; i++)
            {
                newCoefficients[i - min] = operation(this[i], other[i]);
            }
            return new Polynomial(newCoefficients, min);
        }

        private Polynomial NewWithConstant(Func<double, double> operation)
        {
            double[] newCoefficients = new double[maxDegree + 1 - minDegree];
            for (int i = MinDegree; i <= MaxDegree; i++)
            {
                newCoefficients[i - MinDegree] = operation(coefficients[i - MinDegree]);
            }
            return new Polynomial(newCoefficients, MinDegree);
        }
    }
}

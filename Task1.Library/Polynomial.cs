﻿using System;
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
            return NewWithPolynomial(otherPolynomial, (x, y) => x + y);
        }     

        public Polynomial Add(double value)
        {
            return NewWithConstant((x) => x + value);
        }

        public Polynomial Subtract(Polynomial otherPolynomial)
        {
            return NewWithPolynomial(otherPolynomial, (x, y) => x - y);
        }

        public Polynomial Subtract(double value)
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
            for (int i = 0; i <= maxDegree; i++)
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
            for (int i = 1; i <= degree; i++)
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
            double[] newCoefficients = new double[newDegree + 1];
            for (int i = 0; i <= newDegree; i++)
            {
                newCoefficients[i] = operation(this[i], otherPolynomial[i]);
            }
            return new Polynomial(newCoefficients);
        }

        private Polynomial NewWithConstant(Func<double, double> operation)
        {
            double[] newCoefficients = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                newCoefficients[i] = operation(coefficients[i]);
            }
            return new Polynomial(newCoefficients);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Library
{
    public sealed class Polynomial: IEquatable<Polynomial>, ICloneable
    {
        //private readonly int[] coefficients;
        private int degree;
        private readonly IReadOnlyCollection<int> coefficients;
        public IReadOnlyCollection<int> Coeffitients { get { return coefficients; } }

        public Polynomial(params int[] coefficients)
        {
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (temp[i] != 0)
                {
                    degree = i;
                }
            }
            int[] innner;
            Array.Copy(coefficients, innner, degree + 1);
            this.coefficients = new IReadOnlyCollection<int>(inner);
        }

        public Polynomial(Polynomial otherPolynomial)
        {
            
        }

        public double GetValue(double variableValue)
        {

        }

        public Polynomial Add(Polynomial otherPolynomial)
        {

        }

        public Polynomial Add(int value)
        {

        }

        public Polynomial Subtract(Polynomial otherPolynomial)
        {

        }

        public Polynomial Subtract(int value)
        {

        }

        public Polynomial Subtract(Polynomial otherPolynomial)
        {

        }

        public Polynomial Multiply(Polynomial otherPolynomial)
        {

        }
        public Polynomial Multiply(int value)
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Polynomial p1, Polynomial p2)
        {

        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMath
{
    public class Wielomian : IEquatable<Wielomian>
    {
        public int[] Polynomial { get; set; }

        public int Stopien => Polynomial.Length - 1;

        public Wielomian()
        {
            Polynomial = new int[1] { 0 };
        }

        public Wielomian(int polynomial)
        {
            Polynomial = new int[1] { polynomial };
        }

        public Wielomian(params int[] polynomial)
        {
            if (polynomial == null) throw new NullReferenceException();
            if (polynomial.Length == 0) throw new ArgumentException("wielomian nie moze być pusty");
            var tempPolynomial = polynomial;
            var i = 0;
            foreach (var item in tempPolynomial)
            {
                if (item == 0) i++;
                else break;
            }
            tempPolynomial = tempPolynomial.Skip(i).ToArray();
            if (tempPolynomial.Length == 0) Polynomial = new int[1] { 0 };
            else Polynomial = tempPolynomial;
            Array.Reverse(Polynomial);
        }

        public override string ToString()
        {
            if (Polynomial.Length == 1)
                return Polynomial[0].ToString();
            var result = string.Empty;

            result += $"{Polynomial[Polynomial.Length - 1]}x^{Polynomial.Length - 1}";
            for (int i = Polynomial.Length - 2; i >= 0 ; i--)
            {
                if (Polynomial[i] >= 0)
                {
                    result += " + ";
                    if (i == 0)
                    {
                        result += Polynomial[i].ToString();
                        break;
                    }
                    result += $"{Polynomial[i]}x^{i}";
                }
                else
                {
                    result += " - ";
                    if (i == 0)
                    {
                        result += Math.Abs(Polynomial[i]).ToString();
                        break;
                    }
                    result += $"{Math.Abs(Polynomial[i])}x^{i}";
                }
            }
            return result;
        }

        bool IEquatable<Wielomian>.Equals(Wielomian other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this.Stopien != other.Stopien) return false;

            for (int i = 0; i < this.Polynomial.Length - 1; i++)
            {
                if (this.Polynomial[i] != other.Polynomial[i]) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Wielomian) return Equals((Wielomian)obj);
            else return false;
        }

        public static bool Equals(Wielomian obj1, Wielomian obj2)
        {
            if (obj1 is null && obj2 is null) return true;
            if (obj1 is null || obj2 is null) return false;

            return obj1.Equals(obj2);
        }

        public override int GetHashCode() => (Stopien, Polynomial).GetHashCode();

        public static bool operator ==(Wielomian w1, Wielomian w2) => Equals(w1, w2);
        public static bool operator !=(Wielomian w1, Wielomian w2) => !(w1 == w2);

        public static bool operator +(Wielomian w1, Wielomian w2)
        {
            var maxLength = w1.Stopien > w2.Stopien ? w1.Stopien : w2.Stopien;
            var tempPolynomial = new int[maxLength];
            for (int i = 0; i < maxLength ; i++)
            {
                if ()
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyMath
{
    public class Wielomian : IEquatable<Wielomian>, IEnumerable
    {
        public int[] Polynomial { get; }

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

        public Wielomian(bool reversed = false, params int[] polynomial)
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

            if (!reversed)
                Array.Reverse(Polynomial);
        }

        public override string ToString()
        {
            if (Polynomial.Length == 1)
                return Polynomial[0].ToString();
            var result = string.Empty;

            result += $"{Polynomial[Polynomial.Length - 1]}x^{Polynomial.Length - 1}";
            for (int i = Polynomial.Length - 2; i >= 0; i--)
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

        public string ToString(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return ToString();

            if (Polynomial.Length == 1)
                return Polynomial[0].ToString();

            var result = string.Empty;
            var index = Polynomial.Length - 1;
            if (Polynomial[index] == 1) result += $"x^{index}";
            else if (Polynomial[index] == -1) result += $"-x^{index}";
            result += $"{Polynomial[index]}x^{index}";
            for (int i = index - 1; i >= 0; i--)
            {
                if (i == 1)
                {
                    if (Polynomial[i] == 1) result += $" + x";
                    else if (Polynomial[i] >= 0) result += $" + {Polynomial[i]}x";
                    else if (Polynomial[i] < 0) result += $" - {Polynomial[i]}x";
                    continue;
                }

                if (Polynomial[i] == 0) continue;
                else if (Polynomial[i] == 1) result += $" + x^{i}";
                else if (Polynomial[i] == -1) result += $" - x^{i}";
                else if (Polynomial[i] > 1)
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

        public bool Equals(Wielomian other)
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

        public static Wielomian operator +(Wielomian w1, Wielomian w2)
        {
            var maxLength = w1.Stopien > w2.Stopien ? w1.Stopien : w2.Stopien;
            var tempPolynomial = new int[maxLength + 1];
            for (int i = 0; i <= maxLength; i++)
            {
                if (w1.Stopien >= i)
                {
                    tempPolynomial[i] = w1.Polynomial[i];
                    if (w2.Stopien >= i) tempPolynomial[i] += w2.Polynomial[i];
                }
                else tempPolynomial[i] = w2.Polynomial[i];
            }
            Array.Reverse(tempPolynomial);
            return new Wielomian(tempPolynomial);
        }

        public static Wielomian operator -(Wielomian w1, Wielomian w2)
        {

            var maxLength = w1.Stopien > w2.Stopien ? w1.Stopien : w2.Stopien;
            var tempPolynomial = new int[maxLength + 1];
            for (int i = 0; i <= maxLength; i++)
            {
                if (w1.Stopien >= i)
                {
                    tempPolynomial[i] = w1.Polynomial[i];
                    if (w2.Stopien >= i) tempPolynomial[i] -= w2.Polynomial[i];
                }
                else tempPolynomial[i] = 0 - w2.Polynomial[i];
            }
            Array.Reverse(tempPolynomial);
            return new Wielomian(tempPolynomial);
        }

        public static Wielomian operator +(Wielomian w1, int number)
        {
            var tempPolynomial = new int[w1.Stopien + 1];
            for (int i = 0; i <= w1.Stopien; i++)
            {
                tempPolynomial[i] = w1.Polynomial[i];
            }
            tempPolynomial[0] += number;
            Array.Reverse(tempPolynomial);
            return new Wielomian(tempPolynomial);
        }

        public static Wielomian operator -(Wielomian w1, int number)
        {
            var tempPolynomial = new int[w1.Stopien + 1];
            for (int i = 0; i <= w1.Stopien; i++)
            {
                tempPolynomial[i] = w1.Polynomial[i];
            }
            tempPolynomial[0] -= number;
            Array.Reverse(tempPolynomial);
            return new Wielomian(tempPolynomial);
        }

        public static Wielomian operator +(int number, Wielomian w1) => w1 + number;
        public static Wielomian operator -(int number, Wielomian w1)
        {
            var tempPolynomial = new int[w1.Stopien + 1];
            for (int i = 0; i <= w1.Stopien; i++)
            {
                tempPolynomial[i] = -(w1.Polynomial[i]);
            }
            tempPolynomial[0] = number - (-tempPolynomial[0]);
            Array.Reverse(tempPolynomial);
            return new Wielomian(tempPolynomial);
        }

        public static implicit operator Wielomian(int number)
        {
            return new Wielomian(number);
        }

        public static explicit operator int[](Wielomian w)
        {
            var tempPolynomial = new int[w.Stopien + 1];
            for (int i = 0; i <= w.Stopien; i++)
            {
                tempPolynomial[i] = w.Polynomial[i];
            }
            return tempPolynomial;
        }

        public static explicit operator int(Wielomian w)
        {
            if (w.Stopien != 0) throw new InvalidCastException("wielomian nie jest stopnia zerowego");
            return w.Polynomial[0];
        }

        public int this[int i]
        {
            get
            {
                if (i > Polynomial.Length - 1) throw new ArgumentOutOfRangeException();
                return Polynomial[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var item in Polynomial)
            {
                yield return item;
            }
        }

        public static Wielomian Parse(string expression)
        {
            var stringArray = expression.Split("x");
            var tempPolynomial = new int[stringArray.Length];
            for (int i = stringArray.Length - 1; i >= 0; i--)
            {
                stringArray[i] = stringArray[i].Remove(0, stringArray[i].IndexOf(" ") > 0 ? stringArray[i].IndexOf(" ") : 0).Replace(" ", "");
                if (stringArray[i].Contains("+")) stringArray[i] = stringArray[i].Replace("+", " ");
                tempPolynomial[i] = int.Parse(stringArray[i]);
            }
            return new Wielomian(tempPolynomial);
        }

        public static Wielomian Parse(string expression, string type)
        {
            if (string.IsNullOrWhiteSpace(type)) return Parse(expression);

            var stringList = expression.Split(" ").ToList();
            if (stringList.Count % 2 == 0) throw new ArgumentException();

            var maxElement = stringList[0].Split("x^");
            var tempPolynomial = new int[int.Parse(maxElement[1]) + 1];
            tempPolynomial[tempPolynomial.Length - 1] = int.Parse(maxElement[0]);

            for (int i = 2; i < tempPolynomial.Length; i += 2)
            {
                var tempElement = stringList[i].Split("x^");
                var tempString = stringList[i - 1] + tempElement[0];
                var tempNumber = int.Parse(tempString);
                var tempPower = int.Parse(tempElement[1]);

                tempPolynomial[tempPower] = tempNumber;
            }

            return new Wielomian(true, tempPolynomial);
        }
    }
}
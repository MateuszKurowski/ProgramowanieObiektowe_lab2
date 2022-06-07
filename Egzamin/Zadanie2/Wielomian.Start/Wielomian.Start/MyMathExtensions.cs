using MyMath;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace z2_1_wielomian
{
    public class MyMathExtensions
    {
        public static int WielomianPoprzedzaComparison(Wielomian w1, Wielomian w2)
        {
            if (w1 == null)
            {
                if (w2 == null) return 0;
                else return -1;
            }
            else
            {
                if (w2 == null) return 1;
                else
                {
                    if (w1.Stopien != w2.Stopien) return w1.Stopien.CompareTo(w2.Stopien);
                    else
                    {
                        for (int i = w1.Stopien; i >= 0; i--)
                        {
                            if (w1.Polynomial[i] != w2.Polynomial[i]) return w1.Polynomial[i].CompareTo(w2.Polynomial[i]);
                        }
                        return w1.Polynomial[0].CompareTo(w2.Polynomial[0]);
                    }
                }
            }
        }
    }
}
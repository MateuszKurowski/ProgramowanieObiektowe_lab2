using System;

using MyMath;

using W = MyMath.Wielomian; //alias dla nazwy klasy Wielomian

namespace MyExtensions
{
    public static class MyExtensions
    {
        public static double Eval(this W w, double numberDouble)
        {
            var result = w.Polynomial[w.Stopien];
            var number = int.Parse(numberDouble.ToString());

            for (int i = w.Stopien - 1; i >= 0; i--)
            {
                result = result * number + w.Polynomial[i];
            }
            return double.Parse(result.ToString());
        }
    }
}
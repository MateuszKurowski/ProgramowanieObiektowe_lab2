
using System;

namespace BitMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Laby m = new Laby(4);
            //Console.WriteLine(m);
            m[2, 3] = true;
            //Console.WriteLine(m[2,3]);
            //Console.WriteLine(m);

            foreach (var x in m)
            {
                Console.WriteLine(x);
            }
        }
    }
}
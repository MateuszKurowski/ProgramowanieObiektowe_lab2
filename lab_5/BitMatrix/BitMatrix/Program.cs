using System;
using System.Collections;

namespace Program
{
    public static class Program
    {
        static void Main(string[] args)
        {

            // funkcja `And`
            // dane poprawne
            var m1 = new BitMatrix(2, 3, 1, 0, 1, 1, 1, 0);
            var m2 = new BitMatrix(2, 3, 1, 1, 0, 1, 1, 0);
            var expected = new BitMatrix(2, 3, 1, 0, 0, 1, 1, 0);
            var m3 = m1.And(m2);
            // czy poprawnie And i czy zwraca `this`
            if (expected.Equals(m1) && ReferenceEquals(m1, m3))
                Console.WriteLine("Correct data: Pass");

            // argument `null`
            try
            {
                m2.And(null);
                Console.WriteLine("Argument null: Fail");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Argument null: Pass");
            }

            // niepoprawne wymiary
            try
            {
                m2.And(new BitMatrix(2, 1));
                Console.WriteLine("Incorrect dimensions: Fail");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Incorrect dimensions: Pass");
            }
        }
    }
}
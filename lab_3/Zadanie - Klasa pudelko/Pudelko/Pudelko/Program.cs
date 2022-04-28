using BoxLib;

using System;
using System.Collections.Generic;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var boxList = new List<Box>()
            {
                new Box(),
                new Box(5),
                new Box(2, 9, 0.4),
                new Box(20, 2000, 2120, UnitOfMeasure.milimeter),
                new Box(880, 100, 200, UnitOfMeasure.centimeter)
            };

            foreach (var box in boxList)
            {
                Console.WriteLine(box.Volume);
            }
            Comparison<Box> comparison = (b1, b2)
                =>
            {
                if (b2 is null) return 1;
                if (b1.Equals(b2)) return 0;

                if (b1.Volume != b2.Volume) return b1.Volume.CompareTo(b2.Volume);
                if (!b1.Field.Equals(b2.Field)) return b1.Field.CompareTo(b2.Field);

                return (b1.GetA() + b1.GetB() + b1.GetC()).CompareTo(b2.GetA() + b2.GetB() + b2.GetC());
            };
            Box.Comparison(boxList, comparison);

            Console.WriteLine();
            foreach (var box in boxList)
            {
                Console.WriteLine(box.Volume);
            }
        }
    }
}

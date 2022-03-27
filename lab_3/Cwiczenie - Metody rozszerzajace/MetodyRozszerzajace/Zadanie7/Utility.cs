using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class Utility
    {
        public static bool Between<T>(this T element, T lower, T upper) where T : IComparable<T> => element.CompareTo(lower) == 1 && element.CompareTo(upper) == -1;

        public static bool Between<T>(this T element, T lower, T upper, Comparison<T> comparison) => comparison(element, lower) == 1 && comparison(element, upper) == -1;
    }
}
using System;

namespace Extensions
{
    public static class Utility
    {
        public static bool Between<T>(this T element, T lower, T upper) where T : IComparable<T> => element.CompareTo(lower) == 1 && element.CompareTo(upper) == -1;
    }
}
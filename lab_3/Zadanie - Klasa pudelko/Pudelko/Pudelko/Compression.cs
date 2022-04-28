using BoxLib;

using System;

namespace BoxApp
{
    public static class Compression
    {
        public static Box Compress(this Box box)
        {
            var a = Math.Pow(box.Volume, 1/3);
            return new Box(a, a, a);
        }
    }
}

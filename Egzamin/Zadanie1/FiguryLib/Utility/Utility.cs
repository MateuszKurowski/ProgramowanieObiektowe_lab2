//file: Utility.cs

namespace FiguryLib
{
    public enum Kolor { Bialy, Niebieski, Zielony, Czerwony, Czarny, Zolty }

    public enum Format { Prosty, Pelny };

    public struct Punkt2D
    {
        public static readonly Punkt2D ZERO = new Punkt2D(0, 0);
        public readonly double X;
        public readonly double Y;
        public Punkt2D(double x = 0, double y = 0) { X = x; Y = y; }
        public override string ToString() => $"({X}; {Y})";
    }

    public struct Punkt3D
    {
        public static readonly Punkt3D ZERO = new Punkt3D(0, 0, 0);
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public Punkt3D(double x = 0, double y = 0, double z = 0) { X = x; Y = y; Z = z; }
        public override string ToString() => $"({X}; {Y}; {Z})";
    }

    public struct Wektor2D
    {
        public readonly double X;
        public readonly double Y;
        public Wektor2D(double x = 0, double y = 0) { X = x; Y = y; }
        public override string ToString() => $"[{X}; {Y}]";
        public double[] ToArray() => new double[2] { X, Y };
    }

    public struct Wektor3D
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public Wektor3D(double x = 0, double y = 0, double z = 0) { X = x; Y = y; Z = z; }
        public override string ToString() => $"[{X}; {Y}]";
        public double[] ToArray() => new double[3] { X, Y, Z };
    }
}
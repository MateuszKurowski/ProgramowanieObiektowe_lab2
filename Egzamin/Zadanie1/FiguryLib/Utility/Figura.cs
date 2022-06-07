//file: Figura.cs

using System;

namespace FiguryLib
{
    public abstract class Figura
    {
        private static long id = 0;
        public string Nazwa { get; set; }
        public Kolor Kolor { get; set; }
        public Figura(Kolor kolor = Kolor.Czarny)
        {
            id++;
            Nazwa = $"f{id}";
            Kolor = kolor;
        }

        public Figura(string nazwa = "", Kolor kolor = Kolor.Czarny) : this(kolor)
        {
            Nazwa = (nazwa != "") ? nazwa : "f" + id;
        }

        public virtual void Rysuj() => Console.WriteLine(this);
        public override string ToString() => $"Nazwa: {Nazwa}, kolor: {Kolor}, Figura";
    }

    public abstract class Figura2D : Figura
    {
        public Figura2D() : base(Kolor.Czerwony) { }
        public Figura2D(string nazwa = "", Kolor kolor = Kolor.Czerwony) : base(nazwa, kolor) { }
        public abstract void Przesun(double dx, double dy);
        public abstract void Przesun(Wektor2D kierunek);
        public override string ToString() => $"{base.ToString()}-2D";
    }

    public abstract class Figura3D : Figura
    {
        public Figura3D() : base(Kolor.Niebieski) { }
        public Figura3D(string nazwa = "", Kolor kolor = Kolor.Niebieski) : base(nazwa, kolor) { }
        public abstract void Przesun(double dx, double dy, double dz);
        public abstract void Przesun(Wektor3D kierunek);
        public override string ToString() => $"{base.ToString()}-3D";
    }
}
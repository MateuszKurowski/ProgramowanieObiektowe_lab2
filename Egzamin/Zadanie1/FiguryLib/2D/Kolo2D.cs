using FiguryLib;

using System;

namespace FiguryLib
{
    /// <summary>
    /// Koło2D o środku O i promieniu R. Obiekty są mutable.
    /// </summary>
    public class Kolo2D : Okrag2D, IMierzalna1D, IMierzalna2D
    {
        public Kolo2D() : base() { }
        public Kolo2D(Punkt2D srodek, double promien = 0, string nazwa = "") : base(srodek, promien, nazwa)
        {
            R = promien;
            O = srodek;
        }
        public double Pole => Math.PI * R * R;
        public double Obwod => Dlugosc;
        public override string ToString() => $"Kolo2D({O}, {R})";

        public override string ToString(Format format)
            => (format is Format.Prosty) ?
                  this.ToString()
                : base.ToString(format).Replace("Długość", "Obwód") + $", Pole = {Pole:0.##}";
    }
}
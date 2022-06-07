namespace FiguryLib
{
    public static class Extensions
    {
        public static Okrag2D ToOkrag2D(this Kolo2D kolo)
            => new Okrag2D(kolo.O, kolo.R);

        public static Kolo2D ToKolo2D(this Okrag2D okrag)
            => new Kolo2D(okrag.O, okrag.R);

        public static Kula ToKula(this Sfera sfera)
            => new Kula(sfera.O, sfera.R);

        public static Sfera ToSfera(this Kula kula)
            => new Sfera(kula.O, kula.R);
    }
}
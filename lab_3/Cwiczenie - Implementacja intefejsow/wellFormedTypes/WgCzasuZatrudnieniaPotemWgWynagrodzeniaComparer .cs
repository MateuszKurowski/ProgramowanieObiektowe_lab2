namespace wellFormedTypes
{
    class WgCzasuZatrudnieniaPotemWgWynagrodzeniaComparer : IComparer<Pracownik>
    {
        public int Compare(Pracownik p1, Pracownik p2)
        {
            if ((p1 is null) && (p2 is null)) return 0;
            if  ((p1 is null) && !(p2 is null)) return -1;
            if  (!(p1 is null) && (p2 is null)) return 1;

            if (p1.CzasZatrudnienia != p2.CzasZatrudnienia) return (p1.CzasZatrudnienia).CompareTo(p2.CzasZatrudnienia);

            return p1.Wynagrodzenie.CompareTo(p2.Wynagrodzenie);
        }
    }
}
namespace wellFormedTypes
{
    class Program
{
    static void Main(string[] args)
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;
        //Krok1();
        //Krok2();
        //Krok3();
        //Krok4();
        //Krok5();
    }

    static void Krok5()
    {
        var lista = new List<Pracownik>()
        {
            new Pracownik("CCC", new DateTime(2010, 10, 02), 1050),
            new Pracownik("AAA", new DateTime(2010, 10, 01), 100),
            new Pracownik("DDD", new DateTime(2010, 10, 03), 2000),
            new Pracownik("AAA", new DateTime(2011, 10, 01), 1000),
            new Pracownik("BBB", new DateTime(2010, 10, 01), 1050)
        };
        Console.WriteLine($"Lista pracowników:\n{string.Join('\n', lista)}");

        var listaInt = new List<int> { 2, 5, 1, 2, 1, 7, 4, 5 };
        Console.WriteLine($"Lista liczb: {string.Join(',', listaInt)}");

        // wewnętrzny porządek w zbiorze
        Console.WriteLine("--- Porządkowanie za pomocą własnej metody sortującej" + Environment.NewLine
            + "zgodnie z naturalnym porządkiem zdefiniowanym w klasie Pracownik ---");
        Sortowanie.Sortuj(lista); // wywołanie metody "tradycyjnie"
        Console.WriteLine(string.Join('\n', lista));
        listaInt.Sortuj(); // wywołanie jako metody rozszerzajacej
        Console.WriteLine(string.Join(',', listaInt));

        // zewnętrzny porządek - obiekt IComparer
        Console.WriteLine("--- Porządkowanie za pomocą własnej metody sortującej" + Environment.NewLine
            + "zgodnie z porządkiem zdefiniowanym w klasie typu IComparer ---");
        Sortowanie.Sortuj(lista, new WgCzasuZatrudnieniaPotemWgWynagrodzeniaComparer()); // wywołanie metody "tradycyjnie"
        Console.WriteLine(string.Join('\n', lista));

        listaInt.Sortuj(new MyIntComparer()); // wywołanie jako metody rozszerzajacej
        Console.WriteLine(string.Join(',', listaInt));


        // zewnętrzny porządek - delegat Comparison
        Console.WriteLine("--- Porządkowanie za pomocą własnej metody sortującej" + Environment.NewLine
            + "zgodnie z porządkiem zdefiniowanym przez delegat Comparison ---");
        Comparison<Pracownik> porownywacz
            = (p1, p2) => (p1.Wynagrodzenie != p2.Wynagrodzenie) ?
                (-1) * (p1.Wynagrodzenie.CompareTo(p2.Wynagrodzenie)) :
                p1.CzasZatrudnienia.CompareTo(p2.CzasZatrudnienia);
        Sortowanie.Sortuj(lista, porownywacz); // wywołanie metody "tradycyjnie"
        Console.WriteLine(string.Join('\n', lista));

        listaInt.Sortuj( (x,y) => y - x ); // wywołanie jako metody rozszerzajacej
        Console.WriteLine(string.Join(',', listaInt));
    }

    static void Krok4()
    {
        var lista = new List<Pracownik>()
    {
        new Pracownik("CCC", new DateTime(2010, 10, 02), 1050),
        new Pracownik("AAA", new DateTime(2010, 10, 01), 100),
        new Pracownik("DDD", new DateTime(2010, 10, 03), 2000),
        new Pracownik("AAA", new DateTime(2011, 10, 01), 1000),
        new Pracownik("BBB", new DateTime(2010, 10, 01), 1050)
    };

    Console.WriteLine(lista); //wypisze typ, a nie zawartość listy
    foreach (var pracownik in lista)
        System.Console.WriteLine(pracownik);

    Console.WriteLine("--- Porządkowanie za pomocą metod rozszerzających Linq" + Environment.NewLine
                        + "kolejno: rosnąco według wynagrodzenia, " + Environment.NewLine
                        + "później malejąco według nazwiska");

    var query = lista.OrderBy(x => x.Wynagrodzenie).ThenBy(x => x.Nazwisko);
    foreach (var pracownik in query)
        Console.WriteLine(pracownik);
    }

    static void Krok3()
    {
        var lista = new List<Pracownik>();
        lista.Add(new Pracownik("CCC", new DateTime(2010, 10, 02), 1050));
        lista.Add(new Pracownik("AAA", new DateTime(2010, 10, 01), 100));
        lista.Add(new Pracownik("DDD", new DateTime(2010, 10, 03), 2000));
        lista.Add(new Pracownik("AAA", new DateTime(2011, 10, 01), 1000));
        lista.Add(new Pracownik("BBB", new DateTime(2010, 10, 01), 1050));

        Console.WriteLine(lista); //wypisze typ, a nie zawartość listy
        foreach (var pracownik in lista)
            System.Console.WriteLine(pracownik);

        Console.WriteLine("--- Zewnętrzny porządek - obiekt typu IComparer" + Environment.NewLine
                            + "najpierw według czasu zatrudnienia (w miesiącach), " + Environment.NewLine
                            + "a później według wynagrodzenia - wszystko rosnąco");

        lista.Sort(new WgCzasuZatrudnieniaPotemWgWynagrodzeniaComparer());
        foreach (var pracownik in lista)
            System.Console.WriteLine(pracownik);

        Console.WriteLine("--- Zewnętrzny porządek - delegat typu Comparison" + Environment.NewLine
                        + "najpierw według czasu zatrudnienia (w miesiącach), " + Environment.NewLine
                            + "a później kolejno według nazwiska i wynagrodzenia - wszystko rosnąco");
        // sklejamy odpowiednio napisy i je porównujemy
        lista.Sort((p1, p2) => (p1.CzasZatrudnienia.ToString("D3")
                                    + p1.Nazwisko + p1.Wynagrodzenie.ToString("00000.00")
                                )
                                .CompareTo
                                (p2.CzasZatrudnienia.ToString("D3")
                                    + p2.Nazwisko + p2.Wynagrodzenie.ToString("00000.00")
                                )
                    );
        foreach (var pracownik in lista)
            System.Console.WriteLine(pracownik);

        Console.WriteLine("--- Zewnętrzny porządek - delegat typu Comparison" + Environment.NewLine
                        + "kolejno: malejąco według wynagrodzenia, " + Environment.NewLine
                        + "później rosnąca według czasu zatrudnienia");
        //budujemy warunek wyrażeniem warunkowym ()?:
        lista.Sort((p1, p2) => (p1.Wynagrodzenie != p2.Wynagrodzenie) ? (-1) * (p1.Wynagrodzenie.CompareTo(p2.Wynagrodzenie)) : p1.CzasZatrudnienia.CompareTo(p2.CzasZatrudnienia)
                    );
        foreach (var pracownik in lista)
            System.Console.WriteLine(pracownik);
    }

    static void Krok2()
    {
         var lista = new List<Pracownik>();
        lista.Add( new Pracownik("CCC", new DateTime(2010, 10, 02), 1050 ) );
        lista.Add( new Pracownik("AAA", new DateTime(2010, 10, 01), 100 ) );
        lista.Add( new Pracownik("DDD", new DateTime(2010, 10, 03), 2000 ) );
        lista.Add( new Pracownik("AAA", new DateTime(2011, 10, 01), 1000 ) );
        lista.Add( new Pracownik("BBB", new DateTime(2010, 10, 01), 1050 ) );

        Console.WriteLine( lista ); //wypisze typ, a nie zawartość listy

        Console.WriteLine("-- Wariant 1 --");
        foreach( var pracownik in lista )
            Console.WriteLine( pracownik );

        Console.WriteLine("-- Wariant 2 --");
        lista.ForEach( (p) => {Console.Write(p + ",");} );
        Console.WriteLine(   );

        Console.WriteLine("-- Wariant 3 --");
        Console.WriteLine( string.Join('\n', lista) );

        lista.Sort(); //zadziała, jeśli klasa Pracownik implementuje IComparable<Pracownik>

        Console.WriteLine("Po posortowaniu:");
        foreach( var pracownik in lista )
            Console.WriteLine( pracownik );
    }

    static void Krok1()
    {
        Console.WriteLine("--- Sprawdzenie poprawności tworzenia obiektu ---");
        Pracownik p = new Pracownik("Kowalski", new DateTime(2010, 10, 1), 1_000);
        Console.WriteLine(p);

        Console.WriteLine("--- Sprawdzenie równości obiektów ---");
        Pracownik p1 = new Pracownik("Nowak", new DateTime(2010, 10, 1), 1_000);
        Pracownik p2 = new Pracownik("Nowak", new DateTime(2010, 10, 1), 1_000);
        Pracownik p3 = new Pracownik("Kowalski", new DateTime(2010, 10, 1), 1_000);
        Pracownik p4 = p1;
        Console.WriteLine($"p1: {p1} hashCode: {p1.GetHashCode()}");
        Console.WriteLine($"p2: {p2} hashCode: {p2.GetHashCode()}");
        Console.WriteLine($"p3: {p3} hashCode: {p3.GetHashCode()}");
        Console.WriteLine($"p4: {p4} hashCode: {p4.GetHashCode()}");
        Console.WriteLine();

        Console.WriteLine($"--- Równość dla p1 oraz p2 -");
        Console.WriteLine($"Object.ReferenceEquals(p1, p2): {Object.ReferenceEquals(p1, p2)}");
        Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
        Console.WriteLine($"p1 == p2: {p1 == p2}");
        Console.WriteLine();

        Console.WriteLine($"--- Równość dla p1 oraz p3 -");
        Console.WriteLine($"Object.ReferenceEquals(p1, p3): {Object.ReferenceEquals(p1, p3)}");
        Console.WriteLine($"p1.Equals(p3): {p1.Equals(p3)}");
        Console.WriteLine($"p1 == p3: {p1 == p3}");
        Console.WriteLine();

        Console.WriteLine($"--- Równość dla p1 oraz p4 -");
        Console.WriteLine($"Object.ReferenceEquals(p1, p4): {Object.ReferenceEquals(p1, p4)}");
        Console.WriteLine($"p1.Equals(p3): {p1.Equals(p4)}");
        Console.WriteLine($"p1 == p4: {p1 == p4}");
    }

    private class MyIntComparer : IComparer<int>
{
    public int Compare(int x, int y) => (y - x); // malejąco
}
}
}
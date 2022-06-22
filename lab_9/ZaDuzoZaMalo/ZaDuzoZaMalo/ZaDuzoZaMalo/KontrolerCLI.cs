using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using GraZaDuzoZaMalo.Model;

using static GraZaDuzoZaMalo.Model.Gra.Odpowiedz;

namespace AppGraZaDuzoZaMaloCLI
{
    public class KontrolerCLI
    {
        public const char ZNAK_ZAKONCZENIA_GRY = 'X';

        private Gra gra;
        private WidokCLI widok;
        private string _fileName;

        public int MinZakres { get; private set; } = 1;
        public int MaxZakres { get; private set; } = 100;
        private bool _isNewGame = true;

        public IReadOnlyList<Gra.Ruch> ListaRuchow
        {
            get
            { return gra.ListaRuchow; }
        }

        public KontrolerCLI()
        {
            gra = new Gra();
            widok = new WidokCLI(this);
        }

        public void Uruchom()
        {
            widok.CzyscEkran();
            widok.OpisGry();

            widok.PokazMenu();
            var success = false;
            var odp = 0;
            while (!success)
            {
                Console.SetCursorPosition(11, 10);
                char odpChar = Console.ReadKey().KeyChar;
                if (int.TryParse(odpChar.ToString(), out odp) && odp > 0 && odp < 5)
                    success = true;
                else
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Prosze wybrać opcje z menu by kontynuować!");
                }
            }

            switch(odp)
            {
                case 1:
                    {
                        _isNewGame = true;
                        UruchomRozgrywke();
                    }break;
                case 2:
                    {
                        _isNewGame = false;
                        WczytajRozgrywke();
                    }break;
                case 3:
                    {
                        UstawZakresDoLosowania();
                    }
                    break;
                case 4:
                    {
                        Environment.Exit(0);
                    }
                    break;
                default:
                    throw new ApplicationException("Wystąpił nieznany błąd. Skontaktuj się z administratorem.");
            }
        }

        public void UruchomRozgrywke()
        {
            widok.CzyscEkran();

            if (MinZakres != 1 && MaxZakres != 100 && _isNewGame)
                gra = new Gra(MinZakres, MaxZakres);

            int propozycja = 0;
            do
            {
                //wczytaj propozycję
                try
                {
                    propozycja = widok.WczytajPropozycje();
                }
                catch (KoniecGryException)
                {
                    gra.Przerwij();
                }

                if (gra.StatusGry == Gra.Status.Poddana) break;

                switch (gra.Ocena(propozycja))
                {
                    case ZaDuzo:
                        widok.KomunikatZaDuzo();
                        break;
                    case ZaMalo:
                        widok.KomunikatZaMalo();
                        break;
                    case Trafiony:
                        widok.KomunikatTrafiono();
                        break;
                    default:
                        break;
                }
                widok.HistoriaGry();
                Console.WriteLine();
            }
            while (gra.StatusGry == Gra.Status.WTrakcie);

            if (gra.StatusGry == Gra.Status.Poddana)
            {
                if(ZapiszRozgrywke())
                    Uruchom();
                else
                {
                    _isNewGame = false;
                    UruchomRozgrywke();
                }
            }
            if (gra.StatusGry == Gra.Status.Zakonczona)
            {
                widok.Wygrana(propozycja, gra.ListaRuchow.Count, gra.CalkowityCzasGry);
                Uruchom();
            }
        }

        private void WczytajRozgrywke()
        {
            widok.CzyscEkran();
            try
            {
                Console.WriteLine("Znalezione zapisy gier:");
                var files = Directory.EnumerateFiles(@"c:\temp\ZaDuzoZaMalo\", "*.xml").ToList();
                for (int i = 1; i <= files.Count; i++)
                {
                    Console.WriteLine(i + ". " + files[i]);
                }
                var odp = 0;
                var succes = false;
                while (!succes)
                {
                    char odpChar = Console.ReadKey().KeyChar;
                    if (int.TryParse(odpChar.ToString(), out odp) && odp > 0 && odp <= files.Count)
                        succes = true;
                    else
                        Console.WriteLine("Prosze wybrać opcje z menu by kontynuować!");
                }
                ZaladujZapis(files[odp + 1]);
            }
            catch (DirectoryNotFoundException)
            {
                widok.CzyscEkran();
                Console.WriteLine("Nie odnaleziono żadnego zapisu.");
                Console.WriteLine("Naciśnij dowolny klawisz by wrócić do menu.");
                Console.ReadKey();
                widok.CzyscEkran();
                Uruchom();
            }
        }

        private void ZaladujZapis(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Gra));
            try
            {
                using (var sr = new StreamReader(@"c:\temp\ZaDuzoZaMalo\" + fileName))
                {
                    gra = (Gra)xs.Deserialize(sr);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Podany plik jest uszkodzony, nie można wczytać zapisu.");
                Console.WriteLine("Naciśnij dowolny klawisz by wrócić do menu.");
                Console.ReadKey();
                widok.CzyscEkran();
                Uruchom();
            }
            _fileName = fileName;
            MaxZakres = gra.MaxLiczbaDoOdgadniecia;
            MinZakres = gra.MinLiczbaDoOdgadniecia;
            _isNewGame = false;
            UruchomRozgrywke();
        }

        private bool ZapiszRozgrywke()
        {
            Console.WriteLine();
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Gra));
                TextWriter tw = new StreamWriter(@"c:\temp\ZaDuzoZaMalo\" + _fileName + ".xml");
                xs.Serialize(tw, gra);
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udało się zapisać rozgrywki"); 
                Console.WriteLine("Naciśnij dowolny klawisz by wrócić do gry.");
                Console.ReadKey();
                return false;
            }
            Console.WriteLine("Rozgrywka została zapisana");
            Console.WriteLine("Naciśnij dowolny klawisz by wrócić do menu.");
            Console.ReadKey();
            return true;
        }

        ///////////////////////

        public void UstawZakresDoLosowania()
        {
            widok.CzyscEkran();
            Console.WriteLine($"Najniższa możliwa liczba: {MinZakres}");
            Console.WriteLine($"Najwyższa możliwa liczba: {MaxZakres}");
            Console.WriteLine();
            Console.WriteLine("Edytuj:");
            Console.WriteLine("1. Najniższą liczbę");
            Console.WriteLine("2. Najwyższą liczbę");
            Console.WriteLine("3. Cały zakres");
            Console.WriteLine("4. Wróc do menu");
            Console.WriteLine();
            Console.Write("Odpowiedź: ");

            var success = false;
            var odp = 0;
            while (!success)
            {
                Console.SetCursorPosition(11, 9);
                char odpChar = Console.ReadKey().KeyChar;
                if (int.TryParse(odpChar.ToString(), out odp) && odp > 0 && odp < 5)
                    success = true;
                else
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Prosze wybrać opcje z menu by kontynuować!");
                }
            }

            switch (odp)
            {
                case 1:
                    ZmienZakres(true, false);
                    UstawZakresDoLosowania();
                    break;
                case 2:
                    ZmienZakres(false, true);
                    UstawZakresDoLosowania();
                    break;
                case 3:
                    ZmienZakres(true, true);
                    UstawZakresDoLosowania();
                    break;
                case 4:
                    Uruchom();
                    break;
            }
        }

        private void ZmienZakres(bool min, bool max)
        {
            if (min)
            {
                widok.CzyscEkran();
                Console.Write("Podaj nowy najniższy zakres: ");
                var success = false;
                var odp = 0;
                while (!success)
                {
                    var nowyZakresString = Console.ReadLine();
                    if (int.TryParse(nowyZakresString.ToString(), out odp))
                        success = true;
                    else
                    {
                        Console.WriteLine("Prosze podać liczbę!");
                    }
                }
                MinZakres = odp;
            }
            if (max)
            {
                widok.CzyscEkran();
                Console.Write("Podaj nowy najwyższy zakres: ");
                var success = false;
                var odp = 0;
                while (!success)
                {
                    var nowyZakresString = Console.ReadLine();
                    if (int.TryParse(nowyZakresString.ToString(), out odp))
                        success = true;
                    else
                    {
                        Console.WriteLine("Prosze podać liczbę!");
                    }
                }
                MaxZakres = odp;
            }
        }

        public int LiczbaProb() => gra.ListaRuchow.Count();

        public void ZakonczGre()
        {
            //np. zapisuje stan gry na dysku w celu późniejszego załadowania
            //albo dopisuje wynik do Top Score
            //sprząta pamięć
            gra = null;
            widok.CzyscEkran(); //komunikat o końcu gry
            widok = null;
            System.Environment.Exit(0);
        }

        public void ZakonczRozgrywke()
        {
            gra.Przerwij();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <exception cref="KoniecGryException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public int WczytajLiczbeLubKoniec(string value, int defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            value = value.TrimStart().ToUpper();
            if (value.Length > 0 && value[0].Equals(ZNAK_ZAKONCZENIA_GRY))
                throw new KoniecGryException();

            //UWAGA: ponizej może zostać zgłoszony wyjątek 
            return Int32.Parse(value);
        }

        private void CreateNewGameName()
            => _fileName = (DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString()).Replace(".", "_").Replace(":", "_");
    }

    [Serializable]
    internal class KoniecGryException : Exception
    {
        public KoniecGryException()
        {
        }

        public KoniecGryException(string message) : base(message)
        {
        }

        public KoniecGryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KoniecGryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;

namespace ver5
{
    /// <summary>
    /// Interfejs urządzenia
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Obsługiwane stany urządzeń
        /// </summary>
        enum State { off, on, standby };

        /// <summary>
        /// Uruchamia urządzenie
        /// </summary>
        void PowerOn()
        {
            _Counter++;
            SetState(State.on);
        }
        /// <summary>
        /// Włącza tryb oszędzania energii na urządzeniu
        /// </summary>
        void StandbyOn()
        {
            SetState(State.standby);
        }
        /// <summary>
        /// Wyłącza tryb oszczędzanai energii na urządzeniu
        /// </summary>
        void StandbyOff()
        {
            _Counter++;
            SetState(State.on);
        }
        /// <summary>
        /// Wyłącza urządzenie
        /// </summary>
        void PowerOff()
        {
            SetState(State.off);
        }
        /// <summary>
        /// Usatwia obecny stan urządzenia
        /// </summary>
        /// <param name="state">Stan urządzenia</param>
        private void SetState(State state)
        {
            _State = state;
        }
        /// <summary>
        /// Obecny stan urządzenia
        /// </summary>
        protected State _State { get; set; }
        /// <summary>
        /// Zwraca obecny stan urządzenia
        /// </summary>
        /// <returns></returns>
        public State GetState()
        {
            return _State;
        }
        /// <summary>
        /// Sprawdza czy urządzenie jest wyłączone, gdy ma włączone oszczędzanie energii wyłącza je
        /// </summary>
        /// <returns>Zwraca inforacje czy urządzenie jest włączone (true)</returns>
        protected bool CheckState()
        {
            if (GetState() == State.off)
                return false;
            else if (GetState() == State.standby)
                StandbyOff();
            return true;
        }
        /// <summary>
        /// Liczba uruchomień urządzeń
        /// </summary>
        int Counter { get { return _Counter; } }
        /// <summary>
        /// Zwraca liczbe uruchomień urządzeń
        /// </summary>
        private static int _Counter = 0;
    }

    /// <summary>
    /// Interfejs drukarki
    /// </summary>
    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Jeśli urządzenie jest włączone drukuje dokument
        /// </summary>
        /// <param name="document">Dokument do drukowania</param>
        void Print(in IDocument document)
        {
            if (!CheckState())
                return;
            _PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }
        /// <summary>
        /// Zwraca liczbę zeskanowanych dokumentów
        /// </summary>
        int PrintCounter { get { return _PrintCounter; } }
        /// <summary>
        /// Liczba wydrukowanych dokumentów
        /// </summary>
        private static int _PrintCounter = 0;
    }

    /// <summary>
    /// Interfejs skanera
    /// </summary>
    public interface IScanner : IDevice
    {
        /// <summary>
        /// Jeśli urządzenie jest włączone skanuje dokument do podanego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
        void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (!CheckState())
                return;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{_ScannerCounter}.pdf");
                    _ScannerCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;

                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{_ScannerCounter}.jpg");
                    _ScannerCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;

                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TextScan{_ScannerCounter}.txt");
                    _ScannerCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;
            }
        }
        /// <summary>
        /// Jeśli urządzenie jest włączone skanuje dokument
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        void Scan(out IDocument document)
        {
            document = null;
            if (!CheckState())
                return;
            _ScannerCounter++;
            Console.WriteLine($"{DateTime.Now} Scan: {_ScannerCounter} dzisiejszy skan");
        }
        /// <summary>
        /// Zwraca liczbę zeskanowanych dokumentów
        /// </summary>
        int ScanCounter { get { return _ScannerCounter; } }
        /// <summary>
        /// Liczba zeskanowanych dokumentów
        /// </summary>
        private static int _ScannerCounter = 0;
    }

    /// <summary>
    /// Interfejs faksu
    /// </summary>
    public interface IFax : IDevice
    {
        /// <summary>
        /// Jeśli urządzenie jest włączone wysyła faks
        /// </summary>
        /// <param name="document">Dokument od wysłania</param>
        public void SendFax(in IDocument document)
        {
            if (!CheckState())
                return;
            _FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Wysłano fax: {document.GetFileName()}");
        }
        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera faks
        /// </summary>
        public void DownloadFax()
        {
            if (!CheckState())
                return;
            Random random = new Random();
            IDocument document;
            switch (random.Next(3))
            {
                case 0:
                    document = new TextDocument("aaa.txt");
                    _FaxCounter++;
                    _DownloadFaxCounter++;
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 1:
                    document = new PDFDocument("aaa.pdf");
                    _FaxCounter++;
                    _DownloadFaxCounter++;
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 2:
                    document = new ImageDocument("aaa.jpg");
                    _FaxCounter++;
                    _DownloadFaxCounter++;
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;
            }
        }
        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera i wysyła faks
        /// </summary>
        public void FullFax()
        {
            if (!CheckState())
                return;
            SendFax(new ImageDocument("Image.jpg"));
            DownloadFax();
        }
        /// <summary>
        /// Zwraca liczbę użycia faksu
        /// </summary>
        int FaxCounter { get { return _FaxCounter; } }
        /// <summary>
        /// Liczba użycia faksu
        /// </summary>
        private static int _FaxCounter = 0;
        /// <summary>
        /// Zwraca liczbę pobranych faksu
        /// </summary>
        int DownloadFaxCounter { get { return _DownloadFaxCounter; } }
        /// <summary>
        /// Liczba pobranych faksów
        /// </summary>
        private static int _DownloadFaxCounter = 0;
    }
}
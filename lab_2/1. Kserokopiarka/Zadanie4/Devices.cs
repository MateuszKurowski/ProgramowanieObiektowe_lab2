using System;

namespace ver4
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
        abstract protected void SetState(State state);

        /// <summary>
        /// Obecny stan urządzenia
        /// </summary>
        private static State _State = State.off;

        /// <summary>
        /// Zwraca obecny stan urządzenia
        /// </summary>
        State GetState()
        {
            return _State;
        }

        /// <summary>
        /// Zwraca liczbe uruchomień urządzeń
        /// </summary>
        int Counter { get { return _Counter; } }

        /// <summary>
        /// Liczba uruchomień urządzeń
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

        /// <summary>
        ///  Wysyła żądanie włączenia drukarki
        /// </summary>
        new public void PowerOn()
        {
            SetState(State.on);
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia tryb oszczędzania energii w drukarce
        /// </summary>
        new void StandbyOn()
        {
            SetState(State.standby);
            ((IDevice)this).StandbyOn();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia tryb oszczędzania energii w drukarce
        /// </summary>
        new void StandbyOff()
        {
            SetState(State.on);
            ((IDevice)this).StandbyOff();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia drukarki
        /// </summary>
        new void PowerOff()
        {
            SetState(State.off);
            ((IDevice)this).PowerOff();
        }

        /// <summary>
        /// Obecny stan drukarki
        /// </summary>
        private static State _PrinterState = State.off;

        /// <summary>
        /// Ustawia stan drukarki na podany stan
        /// </summary>
        /// <param name="state">Stan</param>
        new public void SetState(State state)
        {
            _PrinterState = state;
        }

        /// <summary>
        /// Zwraca obecny stan drukarki
        /// </summary>
        new State GetState()
        {
            return _PrinterState;
        }
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
            if (_ScannerState == State.off)
                return;
            _ScannerCounter++;
            Console.WriteLine($"{DateTime.Now} Scan: {_ScannerCounter}");
        }

        /// <summary>
        /// Zwraca liczbę zeskanowanych dokumentów
        /// </summary>
        int ScanCounter { get { return _ScannerCounter; } }

        /// <summary>
        /// Liczba zeskanowanych dokumentów
        /// </summary>
        private static int _ScannerCounter = 0;

        /// <summary>
        ///  Wysyła żądanie włączenia skanera
        /// </summary>
        new public void PowerOn()
        {
            SetState(State.on);
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia tryb oszczędzania energii w skanerze
        /// </summary>
        new void StandbyOn()
        {
            _ScannerState = State.standby;
            ((IDevice)this).StandbyOn();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia tryb oszczędzania energii w skanerze
        /// </summary>
        new void StandbyOff()
        {
            _ScannerState = State.on;
            ((IDevice)this).StandbyOff();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia skanera
        /// </summary>
        new void PowerOff()
        {
            _ScannerState = State.off;
            ((IDevice)this).PowerOff();
        }

        /// <summary>
        /// Obecny stan skanera
        /// </summary>
        private static State _ScannerState = State.off;

        /// <summary>
        /// Ustawia stan skanera na podany
        /// </summary>
        /// <param name="state">Stan</param>
        new public void SetState(State state)
        {
            _ScannerState = state;
        }

        /// <summary>
        /// Zwraca obecny stan skanera
        /// </summary>
        new State GetState()
        {
            return _ScannerState;
        }
    }
}
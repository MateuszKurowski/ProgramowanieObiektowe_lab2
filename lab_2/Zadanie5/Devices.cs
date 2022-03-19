using System;

namespace ver5
{
    public interface IDevice
    {
        enum State { on, off, standby };

        void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            _Counter++;
            SetState(State.on);
        }
        void PowerOff() // wyłącza urządzenie, zmienia stan na `off
        {
            SetState(State.off);
        }
        void StandbyOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            SetState(State.standby);
        }
        void StandbyOff() // wyłącza urządzenie, zmienia stan na `off
        {
            _Counter++;
            SetState(State.on);
        }
        protected void SetState(State state)
        {
            _State = state;
        }
        private static State _State = State.off;
        State GetState()// zwraca aktualny stan urządzenia
        {
            return _State;
        }
        int Counter { get { return _Counter; } }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                                                  // np. liczbę uruchomień
        private static int _Counter = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document)
        {
            _PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }
        int PrintCounter { get { return _PrintCounter; } }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                                                            // np. liczbę uruchomień
        private static int _PrintCounter = 0;
        new public void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            SetState(State.on);
            ((IDevice)this).PowerOn();
        }
        new void PowerOff() // wyłącza urządzenie, zmienia stan na `off
        {
            SetState(State.off);
            ((IDevice)this).PowerOff();
        }
        new void StandbyOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            SetState(State.standby);
            ((IDevice)this).StandbyOn();
        }
        new void StandbyOff() // wyłącza urządzenie, zmienia stan na `off
        {
            SetState(State.on);
            ((IDevice)this).StandbyOff();
        }
        private static State _PrinterState = State.off;
        new public void SetState(State state)
        {
            _PrinterState = state;
        }
        new State GetState()// zwraca aktualny stan urządzenia
        {
            return _PrinterState;
        }
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
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
        void Scan(out IDocument document)
        {
            document = null;
            if (_ScannerState == State.off)
                return;
            _ScannerCounter++;
            Console.WriteLine($"{DateTime.Now} Scan: {_ScannerCounter}");
        }
        int ScanCounter { get { return _ScannerCounter; } }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                                                             // np. liczbę uruchomień
        private static int _ScannerCounter = 0;
        new public void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            SetState(State.on);
            ((IDevice)this).PowerOn();
        }
        new void PowerOff() // wyłącza urządzenie, zmienia stan na `off
        {
            _ScannerState = State.off;
            ((IDevice)this).PowerOff();
        }
        new void StandbyOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            _ScannerState = State.standby;
            ((IDevice)this).StandbyOn();
        }
        new void StandbyOff() // wyłącza urządzenie, zmienia stan na `off
        {
            _ScannerState = State.on;
            ((IDevice)this).StandbyOff();
        }
        private static State _ScannerState = State.off;
        new public void SetState(State state)
        {
            _ScannerState = state;
        }
        new State GetState()// zwraca aktualny stan urządzenia
        {
            return _ScannerState;
        }
    }

    public interface IFax
    {
        int FaxCounter { get { return _FaxCounter; } }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                                                             // np. liczbę uruchomień
        private static int _FaxCounter = 0;
        int DownloadFaxCounter { get { return _DownloadFaxCounter; } }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                                                        // np. liczbę uruchomień
        private static int _DownloadFaxCounter = 0;
        public void SendFax(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Wysłano fax: {document.GetFileName()}");
        }
        public void DownloadFax()
        {
            if (state == IDevice.State.off)
                return;
            FaxCounter++;
            DownloadFaxCounter++;
            Random random = new Random();
            IDocument document;
            switch (random.Next(3))
            {
                case 0:
                    document = new TextDocument("aaa.txt");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 1:
                    document = new PDFDocument("aaa.pdf");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 2:
                    document = new ImageDocument("aaa.jpg");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;
            }
        }
        public void FullFax()
        {
            SendFax(new ImageDocument("Image.jpg"));
            DownloadFax();
        }
    }

}
using System.Threading;

using static ver4.IDevice;

namespace ver4
{
    /// <summary>
    /// Kserokopiarka do obsługi drukarki i skanera
    /// </summary>
    public class Copier : IPrinter, IScanner
    {
        /// <summary>
        /// Limit skanera przed przejściem na tryb oszczędzania energii
        /// </summary>
        private int _scannerLimit = 0;

        /// <summary>
        /// Limit drukarki przed przejściem na tryb oszczędzania energii
        /// </summary>
        private int _printerLimit = 0;

        /// <summary>
        /// Liczba uruchomień urządzeń
        /// </summary>
        public int CopierCounter { get { return ((IDevice)this).Counter; } }

        /// <summary>
        /// Stan urządzenia
        /// </summary>
        private static State _State = State.off;

        /// <summary>
        /// Zwraca stan urządzenia
        /// </summary>
        public State GetCopierState()
        {
            return _State;
        }

        /// <summary>
        /// Ustawia oszczędzanie energii na podurządzeniach
        /// </summary>
        public void CopierStandbyOn()
        {
            ((IScanner)this).StandbyOn();
            ((IPrinter)this).StandbyOn();
        }

        /// <summary>
        /// Wyłącza oszczędzanie energii na podurządzeniach
        /// </summary>
        public void CopierStandbyOff()
        {
            ((IScanner)this).StandbyOff();
            ((IPrinter)this).StandbyOff();
        }
        /// <summary>
        /// Ustawia stan na podstawie stawnów podurządzeń
        /// </summary>
        /// <param name="state">Stan</param>
        void IDevice.SetState(State state)
        {
            var printerState = ((IPrinter)this).GetState();
            var scannerState = ((IScanner)this).GetState();
            if (printerState == scannerState)
                _State = printerState;
            else if (printerState != State.off && scannerState == State.off)
                _State = printerState;
            else if (printerState == State.off && scannerState != State.off)
                _State = scannerState;
            else
                _State = State.on;
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do zeskanowania</param>
        public void ScanByCopier(IDocument document)
        {
            if (((IScanner)this).GetState() == State.off)
                return;
            if (((IScanner)this).GetState() == State.standby)
                ((IScanner)this).StandbyOff();
            if (_scannerLimit % 2 == 0)
            {
                ((IScanner)this).StandbyOn();
                Thread.Sleep(1000);
                ((IScanner)this).StandbyOff();
                _scannerLimit = 0;
            }
            ((IPrinter)this).StandbyOn();
            ((IScanner)this).Scan(out document);
            _scannerLimit++;
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu do określonego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
        public void ScanByCopier(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (((IScanner)this).GetState() == State.off)
                return;
            if (((IScanner)this).GetState() == State.standby)
                ((IScanner)this).StandbyOff();
            if (_scannerLimit > 0 && _scannerLimit % 2 == 0)
            {
                ((IScanner)this).StandbyOn();
                Thread.Sleep(1000);
                ((IScanner)this).StandbyOff();
                _scannerLimit = 0;
            }
            ((IPrinter)this).StandbyOn();
            ((IScanner)this).Scan(out document, formatType);
            _scannerLimit++;
        }

        /// <summary>
        /// Wysyła żądanie wydrukowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do wydrukowania</param>
        public void PrintByCopier(in IDocument document)
        {
            if (((IPrinter)this).GetState() == State.off)
                return;
            if (((IPrinter)this).GetState() == State.standby)
                ((IPrinter)this).StandbyOff();
            if (_printerLimit > 0 &&_printerLimit % 3 == 0)
            {
                ((IPrinter)this).StandbyOn();
                Thread.Sleep(1000);
                ((IPrinter)this).StandbyOff();
                _printerLimit = 0;
            }
            ((IScanner)this).StandbyOn();
            ((IPrinter)this).Print(in document);
            _printerLimit++;
        }

        /// <summary>
        ///  Wysyła żądanie skanowania i drukowania dokumentów testowych
        /// </summary>
        public void ScanAndPrint()
        {
            if (GetCopierState() == State.off)
                return;
            else if (GetCopierState() == State.standby)
                CopierStandbyOn();
            IDocument doc = null;
            ScanByCopier(out doc, IDocument.FormatType.JPG);
            if (doc == null)
                return;
            PrintByCopier(in doc);
        }

        

    }
}
namespace ver3
{
    /// <summary>
    /// Kserokopiarka do obsługi drukarki i skanera
    /// </summary>
    public class Copier : BaseDevice
    {
        /// <summary>
        /// Obiekt skanera
        /// </summary>
        protected Scanner _Scanner;

        /// <summary>
        /// Obiekt drukarki
        /// </summary>
        protected Printer _Printer;

        /// <summary>
        /// Domyślny kontruktor - ustawia urządzenia
        /// </summary>
        /// <param name="printer">Instancja drukarki</param>
        /// <param name="scanner">Insatncja skanera</param>
        public Copier(Printer printer, Scanner scanner)
        {
            _Scanner = scanner;
            _Printer = printer;
        }

        /// <summary>
        /// Włącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        public new void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            _Printer.PowerOn();
            _Scanner.PowerOn();
            base.PowerOn();
        }

        /// <summary>
        /// Wyłącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        public new void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            _Printer.PowerOff();
            _Scanner.PowerOff();
            base.PowerOff();
        }

        #region Scanner
        /// <summary>
        ///  Wysyła żądanie włączenia skanera
        /// </summary>
        public void PowerOnScanner()
        {
            if (state == IDevice.State.on)
                return;
            _Scanner.PowerOn();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia skanera
        /// </summary>
        public void PowerOffScanner()
        {
            if (state == IDevice.State.off)
                return;
            _Scanner.PowerOff();
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do zeskanowania</param>
        public void Scan(IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _Scanner.Scan(out document);
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu do określonego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
        public void Scan(IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.off)
                return;
            _Scanner.Scan(out document, formatType);
        }
        #endregion

        #region Printer
        /// <summary>
        ///  Wysyła żądanie włączenia drukarki
        /// </summary>
        public void PowerOnPrinter()
        {
            if (state == IDevice.State.on)
                return;
            _Printer.PowerOn();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia drukarki
        /// </summary>
        public void PowerOffPrinter()
        {
            if (state == IDevice.State.off)
                return;
            _Printer.PowerOff();
        }
        /// <summary>
        /// Wysyła żądanie wydrukowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do wydrukowania</param>
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _Printer.Print(in document);
        }
        #endregion

        /// <summary>
        ///  Wysyła żądanie skanowania i drukowania dokumentów testowych
        /// </summary>
        public void ScanAndPrint()
        {
            if (state == IDevice.State.off)
                return;
            _Scanner.Scan(out IDocument doc, IDocument.FormatType.JPG);
            _Printer.Print(in doc);
        }
    }
}
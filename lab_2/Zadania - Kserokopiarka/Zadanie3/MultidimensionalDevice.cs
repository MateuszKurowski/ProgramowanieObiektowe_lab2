namespace ver3
{
    /// <summary>
    /// Urządzenie wielofunkcyjne do obsługi faksu i kserokopiarki (drukarki i skanera)
    /// </summary>
    public class MultidimensionalDevice : Copier
    {
        /// <summary>
        /// Urządzenie wielofunkcyjne do obsługi faksu i kserokopiarki (drukarki i skanera)
        /// </summary>
        private Fax _fax;

        /// <summary>
        /// Domyślny konstruktor - ustawia urządzenia
        /// </summary>
        /// <param name="printer">Insatncja drukarki</param>
        /// <param name="scanner">Instancja skanera</param>
        /// <param name="fax">Instancja faksu</param>
        public MultidimensionalDevice(Printer printer, Scanner scanner, Fax fax) : base(printer, scanner)
        {
            _fax = fax;
        }

        /// <summary>
        /// Włącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        new public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            _fax.PowerOn();
            base.PowerOn();
        }

        /// <summary>
        /// Wyłącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        public new void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOff();
            base.PowerOff();
        }

        #region Fax
        /// <summary>
        /// Wysyła żądanie pobrania faksu
        /// </summary>
        public void DownloadFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.DownloadFax();
        }

        /// <summary>
        ///  Wysyła żądanie wysyłania faksu
        /// </summary>
        /// <param name="document">Dokument od wysłania</param>
        public void SendFax(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _fax.SendFax(in document);
        }

        /// <summary>
        ///  Wysyła żądanie pobrania i wysyła faksu
        /// </summary>
        public void FullFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.FullFax();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia faks
        /// </summary>
        public void PowerOnFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOn();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia faksu
        /// </summary>
        public void PowerOffFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOff();
        }
        #endregion

        /// <summary>
        ///  Wysyła żądanie pobrania faksu, wysyłania faksu, wydrukowania i skanowania dokumentów testowych
        /// </summary>
        public void FaxPrintAndScan()
        {
            if (state == IDevice.State.off)
                return;
            _fax.FullFax();
            _Scanner.Scan(out IDocument document);
            _Printer.Print(document);
        }
    }
}
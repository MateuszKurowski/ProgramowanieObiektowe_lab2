using static ver5.IDevice;

namespace ver5
{
    /// <summary>
    /// Urządzenie wielofunkcyjne do obsługi faksu i kserokopiarki (drukarki i skanera)
    /// </summary>
    public class MultidimensionalDevice : Copier, IDevice
    {
        /// <summary>
        /// Obiekt faksu
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
        public override void PowerDeviceOn()
        {
            if (((IDevice)this).GetState() == State.on)
                return;
            ((IDevice)_Printer).PowerOn();
            ((IDevice)_Scanner).PowerOn();
            ((IDevice)_fax).PowerOn();
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        /// Włącza oszczędzanie energii w urządzeniu i wszystkich podurządzeniach
        /// </summary>
        public override void StandbyDeviceOn()
        {
            if (((IDevice)this).GetState() == State.standby)
                return;
            ((IDevice)_Printer).StandbyOn();
            ((IDevice)_Scanner).StandbyOn();
            ((IDevice)_fax).StandbyOn();
            ((IDevice)this).StandbyOn();
        }

        /// <summary>
        /// Wyłącza oszczędzanie energii w urządzeniu i wszystkich podurządzeniach
        /// </summary>
        public override void StandbyDeviceOff()
        {
            if (((IDevice)this).GetState() == State.on)
                return;
            ((IDevice)_Printer).PowerOn();
            ((IDevice)_Scanner).PowerOn();
            ((IDevice)_fax).PowerOn();
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        /// Wyłącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        public override void PowerDeviceOff()
        {
            if (((IDevice)this).GetState() == State.off)
                return;
            ((IDevice)_Printer).PowerOff();
            ((IDevice)_Scanner).PowerOff();
            ((IDevice)_fax).PowerOff();
            ((IDevice)this).PowerOff();
        }

        /// <summary>
        /// Ustawia stan urządzenia na podstawie jego podurządzeń
        /// </summary>
        protected override void SetStateByDevice()
        {
            var faxState = ((IFax)_fax).GetState();
            var printerState = ((IPrinter)_Printer).GetState();
            var scannerState = ((IScanner)_Scanner).GetState();
            // Gdy wszystkie urządzenai mają taki sam stan
            if (printerState == scannerState && printerState == faxState)
                SetStateByDevice(printerState);
            // Gdy tylko jedno urządzeie jest włączone
            else if (printerState != State.off && scannerState == State.off && faxState == State.off)
                SetStateByDevice(printerState);
            else if (printerState == State.off && scannerState != State.off && faxState == State.off)
                SetStateByDevice(scannerState);
            else if (printerState == State.off && scannerState == State.off && faxState != State.off)
                SetStateByDevice(faxState);
            // Gdy dwa urządzenia są włączone i mają taki sam stan
            else if (printerState == State.off && scannerState == faxState)
                SetStateByDevice(scannerState);
            else if (scannerState == State.off && printerState == faxState)
                SetStateByDevice(printerState);
            else if (faxState == State.off && printerState == scannerState)
                SetStateByDevice(printerState);
            // Gdy są włączone urządzenia i mają różne stany ustaw urządzenie na włączone
            else
                ((IDevice)this).PowerOn();
        }

        #region Fax
        /// <summary>
        ///  Wysyła żądanie włączenia faks
        /// </summary>
        public void PowerOnFax()
        {
            if (!CheckState())
                return;
            ((IDevice)_fax).PowerOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia tryb oszczędzania energii w faksie
        /// </summary>
        public void StandbyOnFax()
        {
            if (!CheckState())
                return;
            ((IDevice)_fax).StandbyOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia tryb oszczędzania energii w faksie
        /// </summary>
        public void StandbyOffFax()
        {
            if (!CheckState())
                return;
            ((IDevice)_fax).StandbyOff();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia faksu
        /// </summary>
        public void PowerOffFax()
        {
            if (!CheckState())
                return;
            ((IDevice)_fax).PowerOff();
            SetStateByDevice();
        }

        /// <summary>
        /// Wysyła żądanie pobrania faksu
        /// </summary>
        public void DownloadFax()
        {
            if (!CheckState())
                return;
            ((IFax)_fax).DownloadFax();
        }

        /// <summary>
        ///  Wysyła żądanie wysyłania faksu
        /// </summary>
        /// <param name="document">Dokument od wysłania</param>
        public void SendFax(in IDocument document)
        {
            if (!CheckState())
                return;
            ((IFax)_fax).SendFax(in document);
        }

        /// <summary>
        ///  Wysyła żądanie pobrania i wysyła faksu
        /// </summary>
        public void FullFax()
        {
            if (!CheckState())
                return;
            ((IFax)_fax).FullFax();
        }
        #endregion

        /// <summary>
        ///  Wysyła żądanie pobrania faksu, wysyłania faksu, wydrukowania i skanowania dokumentów testowych
        /// </summary>
        public void FaxPrintAndScan()
        {
            if (!CheckState())
                return;
            ((IFax)_fax).FullFax();
            ((IScanner)_Scanner).Scan(out IDocument document);
            ((IPrinter)_Printer).Print(document);
        }
    }
}
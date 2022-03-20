using static ver5.IDevice;

namespace ver5
{
    /// <summary>
    /// Kserokopiarka do obsługi drukarki i skanera
    /// </summary>
    public class Copier : IDevice
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
        /// Stan urządzenia
        /// </summary>
        State IDevice._State { get; set; }

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
        public virtual void PowerDeviceOn()
        {
            if (((IDevice)this).GetState() == State.on)
                return;
            ((IDevice)_Printer).PowerOn();
            ((IDevice)_Scanner).PowerOn();
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        /// Włącza oszczędzanie energii w urządzeniu i wszystkich podurządzeniach
        /// </summary>
        public virtual void StandbyDeviceOn()
        {
            if (((IDevice)this).GetState() == State.standby)
                return;
            ((IDevice)_Printer).StandbyOn();
            ((IDevice)_Scanner).StandbyOn();
            ((IDevice)this).StandbyOn();
        }

        /// <summary>
        /// Wyłącza oszczędzanie energii w urządzeniu i wszystkich podurządzeniach
        /// </summary>
        public virtual void StandbyDeviceOff()
        {
            if (((IDevice)this).GetState() == State.on)
                return;
            ((IDevice)_Printer).PowerOn();
            ((IDevice)_Scanner).PowerOn();
            ((IDevice)this).PowerOn();
        }

        /// <summary>
        /// Wyłącza urządzenie oraz wszystkie jego podurządzenia
        /// </summary>
        public virtual void PowerDeviceOff()
        {
            if (((IDevice)this).GetState() == State.off)
                return;
            ((IDevice)_Printer).PowerOff();
            ((IDevice)_Scanner).PowerOff();
            ((IDevice)this).PowerOff();
        }

        /// <summary>
        /// Ustawia stan urządzenia na podstawie jego podurządzeń
        /// </summary>
        protected virtual void SetStateByDevice()
        {
            var printerState = ((IPrinter)_Printer).GetState();
            var scannerState = ((IScanner)_Scanner).GetState();
            // Gdy urządzenia maja taki sam stan
            if (printerState == scannerState)
                SetStateByDevice(printerState);
            // Gdy tylko jedno urządzeie jest włączone
            else if (printerState != State.off && scannerState == State.off)
                SetStateByDevice(printerState);
            else if (printerState == State.off && scannerState != State.off)
                SetStateByDevice(scannerState);
            // Gdy są włączone urządzenia i mają różne stany ustaw urządzenie na włączone
            else
                ((IDevice)this).PowerOn();
        }

        /// <summary>
        /// Ustawia stan urządzenia
        /// </summary>
        /// <param name="state">Stan</param>
        protected void SetStateByDevice(State state)
        {
            switch (state)
            {
                case State.on:
                    ((IDevice)this).PowerOn();
                    break;
                case State.off:
                    ((IDevice)this).PowerOff();
                    break;
                case State.standby:
                    ((IDevice)this).StandbyOn();
                    break;
            }
        }

        /// <summary>
        /// Sprawdza czy urządzenie jest wyłączone, gdy ma włączone oszczędzanie energii wyłącza je
        /// </summary>
        /// <returns>Zwraca inforacje czy urządzenie jest włączone (true)</returns>
        protected bool CheckState()
        {
            if (((IDevice)this).GetState() == State.off)
                return false;
            else if (((IDevice)this).GetState() == State.standby)
                ((IDevice)this).StandbyOff();
            return true;
        }

        /// <summary>
        /// Zwraca stan urządzenia
        /// </summary>
        /// <returns>Stan urządzenia</returns>
        public State GetDeviceState() => ((IDevice)this).GetState();

        #region Scanner
        /// <summary>
        ///  Wysyła żądanie włączenia skanera
        /// </summary>
        public void PowerOnScanner()
        {
            if (!CheckState())
                return;
            ((IDevice)_Scanner).PowerOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia tryb oszczędzania energii w skanerze
        /// </summary>
        public void StandbyOnScanner()
        {
            if (!CheckState())
                return;
            ((IDevice)_Scanner).StandbyOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia tryb oszczędzania energii w skanerze
        /// </summary>
        public void StandbyOffScanner()
        {
            if (!CheckState())
                return;
            ((IDevice)_Scanner).StandbyOff();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia skanera
        /// </summary>
        public void PowerOffScanner()
        {
            if (!CheckState())
                return;
            ((IDevice)_Scanner).PowerOff();
            SetStateByDevice();
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do zeskanowania</param>
        public void Scan(IDocument document)
        {
            if (!CheckState())
                return;
            ((IScanner)_Scanner).Scan(out document);
        }

        /// <summary>
        /// Wysyła żądanie zeskanowania dokumentu do określonego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
        public void Scan(IDocument document, IDocument.FormatType formatType)
        {
            if (!CheckState())
                return;
            ((IScanner)_Scanner).Scan(out document, formatType);
        }
        #endregion

        #region Printer
        /// <summary>
        ///  Wysyła żądanie włączenia drukarki
        /// </summary>
        public void PowerOnPrinter()
        {
            if (!CheckState())
                return;
            ((IDevice)_Printer).PowerOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie włączenia tryb oszczędzania energii w drukarce
        /// </summary>
        public void StandbyOnPrinter()
        {
            if (!CheckState())
                return;
            ((IDevice)_Printer).StandbyOn();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia tryb oszczędzania energii w drukarce
        /// </summary>
        public void StandbyOffPrinter()
        {
            if (!CheckState())
                return;
            ((IDevice)_Printer).StandbyOff();
            SetStateByDevice();
        }

        /// <summary>
        ///  Wysyła żądanie wyłączenia drukarki
        /// </summary>

        public void PowerOffPrinter()
        {
            if (!CheckState())
                return;
            ((IDevice)_Printer).PowerOff();
            SetStateByDevice();
        }

        /// <summary>
        /// Wysyła żądanie wydrukowania dokumentu
        /// </summary>
        /// <param name="document">Dokument do wydrukowania</param>
        public void Print(in IDocument document)
        {
            if (!CheckState())
                return;
            ((IPrinter)_Printer).Print(in document);
        }
        #endregion

        /// <summary>
        ///  Wysyła żądanie skanowania i drukowania dokumentów testowych
        /// </summary>
        public void ScanAndPrint()
        {
            if (!CheckState())
                return;
            ((IScanner)_Scanner).Scan(out IDocument doc, IDocument.FormatType.JPG);
            ((IPrinter)_Printer).Print(in doc);
        }
    }
}
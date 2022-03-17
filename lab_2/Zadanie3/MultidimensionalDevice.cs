namespace ver3
{
    public class MultidimensionalDevice : Copier
    {
        private Fax _fax;
        public MultidimensionalDevice(Printer printer, Scanner scanner, Fax fax) : base(printer, scanner)
        {
            _fax = fax;
        }

        new public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            _fax.PowerOn();
            base.PowerOn();
        }

        public new void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOff();
            base.PowerOff();
        }

        public void DownloadFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.DownloadFax();
        }

        public void SendFax(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _fax.SendFax(in document);
        }

        public void FullFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.FullFax();
        }

        public void FaxPrintAndScan()
        {
            if (state == IDevice.State.off)
                return;
            _fax.FullFax();
            _scanner.Scan(out IDocument document);
            _printer.Print(document);
        }

        public void PowerOnFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOn();
        }

        public void PowerOffFax()
        {
            if (state == IDevice.State.off)
                return;
            _fax.PowerOff();
        }
    }
}
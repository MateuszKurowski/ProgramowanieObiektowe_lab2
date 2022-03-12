using System;

namespace ver3
{
    public class Copier : BaseDevice
    {
        private Scanner _scanner;
        private Printer _printer;

        public Copier(Printer printer, Scanner scanner)
        {
            _scanner = scanner;
            _printer = printer;
        }

        new public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            _printer.PowerOn();
            _scanner.PowerOn();
            base.PowerOn();
        }

        public new void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            _printer.PowerOff();
            _scanner.PowerOff();
            base.PowerOff();
        }

        public void Scan(out IDocument document)
        {
            _scanner.Scan(out document);
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            _scanner.Scan(out document, formatType);
        }

        public void Print(in IDocument document)
        {
            _printer.Print(in document);
        }

        public void ScanAndPrint()
        {
            _scanner.Scan(out IDocument doc, IDocument.FormatType.JPG);
            _printer.Print(in doc);
        }
    }
}
using System;

namespace ver3
{
    public class Copier : BaseDevice
    {
        protected Scanner _scanner;
        protected Printer _printer;

        public Copier(Printer printer, Scanner scanner)
        {
            _scanner = scanner;
            _printer = printer;
        }

        public new void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            _printer.PowerOn();
            _scanner.PowerOn();
            base.PowerOn();
        }

        public void PowerOnScanner()
        {
            if (state == IDevice.State.on)
                return;
            _scanner.PowerOn();
        }

        public void PowerOnPrinter()
        {
            if (state == IDevice.State.on)
                return;
            _printer.PowerOn();
        }

        public new void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            _printer.PowerOff();
            _scanner.PowerOff();
            base.PowerOff();
        }
        
        public void PowerOffScanner()
        {
            if (state == IDevice.State.off)
                return;
            _scanner.PowerOff();
        }

        public void PowerOffPrinter()
        {
            if (state == IDevice.State.off)
                return;
            _printer.PowerOff();
        }

        public void Scan(IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _scanner.Scan(out document);
        }

        public void Scan(IDocument document, IDocument.FormatType formatType)
        {
            if (state == IDevice.State.off)
                return;
            _scanner.Scan(out document, formatType);
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            _printer.Print(in document);
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.off)
                return;
            _scanner.Scan(out IDocument doc, IDocument.FormatType.JPG);
            _printer.Print(in doc);
        }
    }
}
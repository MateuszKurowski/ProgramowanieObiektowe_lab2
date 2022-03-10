using System;

namespace ver2
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }
        new public int Counter { get; private set; }

        new public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            state = IDevice.State.on;
            Counter++;
            Console.WriteLine("Device is on ...");
        }

        new public void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }

        public void Scan(out IDocument document)
        {
            document = null;
            if (state == IDevice.State.off)
                return;
            ScanCounter++;
            Console.WriteLine($"{DateTime.Now} Scan: {ScanCounter}");
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (state == IDevice.State.off)
                return;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                    ScanCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;

                case IDocument.FormatType.JPG:
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                    ScanCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;

                case IDocument.FormatType.TXT:
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                    ScanCounter++;
                    Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
                    break;
            }
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            Print(in doc);
        }
    }
}
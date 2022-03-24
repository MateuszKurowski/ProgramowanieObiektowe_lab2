using System;

namespace ver1
{
    /// <summary>
    /// Kserokopiarka do obsługi drukarki i skanera
    /// </summary>
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        /// <summary>
        /// Liczba wydrukowanych dokumentów
        /// </summary>
        public int PrintCounter { get; set; }
        /// <summary>
        /// Liczba zeskanowanych dokumentów
        /// </summary>
        public int ScanCounter { get; set; }

        /// <summary>
        /// Jeśli urządzenie jest włączone drukuje dokument
        /// </summary>
        /// <param name="document">Dokument do drukowania</param>
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }

        /// <summary>
        /// Jeśli urządzenie jest włączone skanuje dokument
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        public void Scan(out IDocument document)
        {
            document = null;
            if (state == IDevice.State.off)
                return;
            ScanCounter++;
            Console.WriteLine($"{DateTime.Now} Scan: {ScanCounter}");
        }

        /// <summary>
        /// Jeśli urządzenie jest włączone skanuje dokument do podanego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
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

        /// <summary>
        ///  Wysyła żądanie skanowania i drukowania dokumentów testowych
        /// </summary>
        public void ScanAndPrint()
        {
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            Print(in doc);
        }
    }
}
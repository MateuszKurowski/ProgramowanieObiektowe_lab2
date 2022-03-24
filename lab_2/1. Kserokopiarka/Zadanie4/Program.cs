using ver4;

namespace ConsoleApp4
{
    internal static class Program
    {
        static void Main(string[] args)
        { 
            var xerox = new Copier();
            IScanner scanner = xerox;
            IPrinter printer = xerox;
            scanner.PowerOn();
            printer.PowerOn();
            scanner.StandbyOn();
            var scannerStatus = scanner.GetState();
            var printerStatus = printer.GetState();
            var xerosStatus = xerox.GetCopierState();
            xerox.CopierStandbyOn();
            scannerStatus = scanner.GetState();
            printerStatus = printer.GetState();
            xerosStatus = xerox.GetCopierState();
            var counter = xerox.CopierCounter;

            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.PrintByCopier(in doc1);

            IDocument doc2 = null;
            xerox.ScanByCopier(doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(printer.PrintCounter);
            System.Console.WriteLine(scanner.ScanCounter);
        }
    }
}
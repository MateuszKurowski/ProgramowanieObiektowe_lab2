using System;

using ver3;

namespace ConsoleApp3
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var xerox = new Copier(printer, scanner);
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2 = null;
            xerox.Scan(doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(printer.PrintCounter);
            System.Console.WriteLine(scanner.ScanCounter);
        }
    }
}

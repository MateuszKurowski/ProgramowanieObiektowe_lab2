using ver3;

namespace ConsoleApp3
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multiDevice = new MultidimensionalDevice(printer, scanner, fax);
            multiDevice.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            multiDevice.Print(in doc1);

            IDocument doc2 = null;
            multiDevice.Scan(doc2);

            multiDevice.DownloadFax();

            multiDevice.ScanAndPrint();
            System.Console.WriteLine(multiDevice.Counter);
            System.Console.WriteLine(printer.PrintCounter);
            System.Console.WriteLine(scanner.ScanCounter);
        }
    }
}
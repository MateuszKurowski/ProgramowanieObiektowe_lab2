using ver5;

namespace ConsoleApp5
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var xerox = new Copier(printer, scanner);
            xerox.PowerDeviceOn();
            xerox.StandbyOnScanner();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2 = null;
            xerox.Scan(doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(((IDevice)xerox).Counter);
            System.Console.WriteLine(((IPrinter)printer).PrintCounter);
            System.Console.WriteLine(((IScanner)scanner).ScanCounter);

            var fax = new Fax();
            scanner = new Scanner();
            printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerDeviceOn();
            multidimensionalDevice.StandbyOnPrinter();
            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.StandbyOnFax();
            multidimensionalDevice.FaxPrintAndScan();
        }
    }
}
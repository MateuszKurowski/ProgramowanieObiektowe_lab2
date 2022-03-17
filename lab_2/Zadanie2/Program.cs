using System;

using ver2;

namespace ConsoleApp2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var xerox = new MultifunctionalDevice();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.SendFax(in doc1);

            xerox.DownloadFax();

            xerox.Fax();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.FaxCounter);
            System.Console.WriteLine(xerox.DownloadFaxCounter);
        }
    }
}

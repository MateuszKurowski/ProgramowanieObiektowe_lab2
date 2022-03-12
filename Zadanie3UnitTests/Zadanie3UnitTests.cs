using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver3;
using System;
using System.IO;

namespace ver3UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestCopier
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void PrinterByCopier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        [TestMethod]
        public void ScannerByCopier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        [TestMethod]
        public void Printer_GetState_StateOff()
        {
            var printer = new Printer();
            printer.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        [TestMethod]
        public void Scanner_GetState_StateOff()
        {
            var scanner = new Scanner();
            scanner.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        [TestMethod]
        public void PrinterByCopier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        [TestMethod]
        public void ScannerByCopier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, scanner.GetState());
        }

        [TestMethod]
        public void Printer_GetState_StateOn()
        {
            var printer = new Printer();
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        [TestMethod]
        public void Scanner_GetState_StateOn()
        {
        var scanner = new Scanner();
            scanner.PowerOn();

        Assert.AreEqual(IDevice.State.on, scanner.GetState());
        }




    // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
    // wymagane przekierowanie konsoli do strumienia StringWriter
    [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, printer.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, scanner.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }

    }
}
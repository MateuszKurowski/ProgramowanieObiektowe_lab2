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

    /// <summary>
    /// Testy jednostkowe skanera
    /// </summary>
    [TestClass]
    public class UnitTestScanner
    {
        [TestMethod]
        public void Scanner_GetState_StateOff()
        {
            var scanner = new Scanner();
            scanner.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        [TestMethod]
        public void Scanner_GetState_StateOn()
        {
            var scanner = new Scanner();
            scanner.PowerOn();

            Assert.AreEqual(IDevice.State.on, scanner.GetState());
        }

        [TestMethod]
        public void Scanner_ScanCounter()
        {
            var scanner = new Scanner();
            scanner.PowerOn();
            scanner.Scan(out IDocument document);
            scanner.Scan(out IDocument document2);

            scanner.PowerOff();
            scanner.Scan(out IDocument document3);

            // 2 skanowania
            Assert.AreEqual(2, scanner.ScanCounter);
        }

        [TestMethod]
        public void Scanner_PowerOnCounter()
        {
            var scanner = new Scanner();
            scanner.PowerOn();
            scanner.Scan(out IDocument document);
            scanner.PowerOn();
            scanner.PowerOff();
            scanner.PowerOff();
            scanner.PowerOn();

            // 2 włączenia
            Assert.AreEqual(2, scanner.Counter);
        }
    }

    /// <summary>
    /// Testy jednostkowe faxu
    /// </summary>
    [TestClass]
    public class UnitTestFax
    {
        [TestMethod]
        public void Fax_GetState_StateOff()
        {
            var fax = new Fax();
            fax.PowerOff();

            Assert.AreEqual(IDevice.State.off, fax.GetState());
        }

        [TestMethod]
        public void Fax_GetState_StateOn()
        {
            var fax = new Fax();
            fax.PowerOn();

            Assert.AreEqual(IDevice.State.on, fax.GetState());
        }

        [TestMethod]
        public void Fax_FaxCounter()
        {
            var fax = new Fax();
            fax.PowerOn();
            fax.DownloadFax();
            fax.DownloadFax();
            
            fax.PowerOff();
            fax.DownloadFax();

            // 2 pobrane faxy
            Assert.AreEqual(2, fax.FaxCounter);
        }

        [TestMethod]
        public void Scanner_PowerOnCounter()
        {
            var fax = new Fax();
            fax.PowerOn();
            fax.DownloadFax();
            fax.PowerOn();
            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOn();

            // 2 włączenia
            Assert.AreEqual(2, fax.Counter);
        }
    }

    /// <summary>
    /// Testy jednostkowe drukarki
    /// </summary>
    [TestClass]
    public class UnitTestPrinter
    {
        [TestMethod]
        public void Printer_GetState_StateOff()
        {
            var printer = new Printer();
            printer.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        [TestMethod]
        public void Printer_GetState_StateOn()
        {
            var printer = new Printer();
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        [TestMethod]
        public void Printer_PrintCounter()
        {
            var printer = new Printer();
            printer.PowerOn();
            IDocument document = new ImageDocument("aaa.img");
            IDocument document2 = new PDFDocument("aaa.pdf");
            IDocument document3 = new TextDocument("aaa.txt");
            printer.Print(in document);
            printer.Print(in document2);

            printer.PowerOff();
            printer.Print(document3);

            // 2 pobrane faxy
            Assert.AreEqual(2, printer.PrintCounter);
        }

        [TestMethod]
        public void Printer_PowerOnCounter()
        {
            var printer = new Printer();
            printer.PowerOn();
            IDocument document = new ImageDocument("aaa.img");
            printer.Print(in document);
            printer.PowerOn();
            printer.PowerOff();
            printer.PowerOff();
            printer.PowerOn();

            // 2 włączenia
            Assert.AreEqual(2, printer.Counter);
        }
    }

    /// <summary>
    /// Testy jednostkowe kserokopiarki
    /// </summary>
    [TestClass]
    public class UnitTestCopier
    {
        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan kserokopiarki
        /// </summary>
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan drukarki korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void PrinterByCopier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan skanera korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void ScannerByCopier_GetState_StateOff()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan kserokopiarki
        /// </summary>
        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan drukarki korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void PrinterByCopier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan skanera korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void ScannerByCopier_GetState_StateOn()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

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
                IDocument doc1 = null;
                copier.Scan(doc1);
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
                IDocument doc1 = null;
                copier.Scan(doc1);
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
                IDocument doc1 = null;
                copier.Scan(doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(doc1, formatType: IDocument.FormatType.PDF);
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

        /// <summary>
        /// Wertfikuje poprawność liczenia wydrukowanych dokumentów
        /// </summary>
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
            copier.Scan(doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, printer.PrintCounter);
        }

        /// <summary>
        /// Wertfikuje poprawność liczenia zeskanowanych dokumentów
        /// </summary>
        [TestMethod]
        public void Copier_ScanCounter()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Scan(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Scan(doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, scanner.ScanCounter);
        }

        /// <summary>
        /// Wertfikuje poprawność liczenia uruchomień kserokopiarki
        /// </summary>
        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var scanner = new Scanner();
            var printer = new Printer();
            var copier = new Copier(printer, scanner);
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Scan(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Scan(doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(doc1);
            copier.PowerOn();

            copier.ScanAndPrint();
            copier.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, copier.Counter);
        }
    }

    /// <summary>
    /// Testy jednostkowe urządzenia wielofunkcyjnego
    /// </summary>
    [TestClass]
    public class UnitTestMultidimensionalDevice
    {
        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan urządzenia wielofunkcyjnego
        /// </summary>
        [TestMethod]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multidimensionalDevice.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan drukarki korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void PrinterBymultidimensionalDevice_GetState_StateOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, printer.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan skanera korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void ScannerBymultidimensionalDevice_GetState_StateOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, scanner.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan kserokopiarki
        /// </summary>
        [TestMethod]
        public void multidimensionalDevice_GetState_StateOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multidimensionalDevice.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan drukarki korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void PrinterBymultidimensionalDevice_GetState_StateOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            printer.PowerOn();

            Assert.AreEqual(IDevice.State.on, printer.GetState());
        }

        /// <summary>
        /// Weryfikuje czy ustawiony poprawny stan skanera korzystając z kserokopiarki
        /// </summary>
        [TestMethod]
        public void ScannerBymultidimensionalDevice_GetState_StateOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, scanner.GetState());
        }

        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Print_DeviceOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multidimensionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Print_DeviceOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multidimensionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Scan_DeviceOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = null;
                multidimensionalDevice.Scan(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Scan_DeviceOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = null;
                multidimensionalDevice.Scan(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void multidimensionalDevice_Scan_FormatTypeDocument()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = null;
                multidimensionalDevice.Scan(doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multidimensionalDevice.Scan(doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multidimensionalDevice.Scan(doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        /// <summary>
        /// Wertfikuje poprawność liczenia wydrukowanych dokumentów
        /// </summary>
        [TestMethod]
        public void multidimensionalDevice_PrintCounter()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multidimensionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multidimensionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, printer.PrintCounter);
        }

        /// <summary>
        /// Wertfikuje poprawność liczenia zeskanowanych dokumentów
        /// </summary>
        [TestMethod]
        public void multidimensionalDevice_ScanCounter()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multidimensionalDevice.Scan(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multidimensionalDevice.Scan(doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, scanner.ScanCounter);
        }

        /// <summary>
        /// Wertfikuje poprawność liczenia uruchomień kserokopiarki
        /// </summary>
        [TestMethod]
        public void multidimensionalDevice_PowerOnCounter()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.PowerOn();
            multidimensionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multidimensionalDevice.Scan(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multidimensionalDevice.Scan(doc2);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOff();
            multidimensionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multidimensionalDevice.Print(in doc3);

            multidimensionalDevice.PowerOff();
            multidimensionalDevice.Print(in doc3);
            multidimensionalDevice.Scan(doc1);
            multidimensionalDevice.PowerOn();

            multidimensionalDevice.ScanAndPrint();
            multidimensionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multidimensionalDevice.Counter);
        }

        // weryfikacja, czy po wywołaniu metody `DownloadFax` i włączonym faxie w napisie pojawia się tekst `Pobrano fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Fax_DeviceOn()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.DownloadFax();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Pobrano fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `DownloadFax` i włączonym faxie w napisie NIE pojawia się tekst `Pobrano fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void multidimensionalDevice_Fax_DeviceOff()
        {
            var fax = new Fax();
            var scanner = new Scanner();
            var printer = new Printer();
            var multidimensionalDevice = new MultidimensionalDevice(printer, scanner, fax);
            multidimensionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multidimensionalDevice.DownloadFax();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Pobrano fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}
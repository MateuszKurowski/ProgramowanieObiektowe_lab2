using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver2;
using System;
using System.IO;

namespace ver2UnitTests
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
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multifunctionalDevice.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multifunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, multifunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.ScanAndPrint();
            multifunctionalDevice.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonym faxie w napisie NIE pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.Fax();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Fax` i włączonym faxie w napisie pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.Fax();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonym faxie w napisie NIE pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_DownloadFax_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.DownloadFax();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Fax` i włączonym faxie w napisie pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_DownloadFax_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multifunctionalDevice.DownloadFax();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonym faxie w napisie NIE pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_SendFax_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("PDFFax.pdf");
                multifunctionalDevice.SendFax(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Fax` i włączonym faxie w napisie pojawia się słowo `fax`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_SendFax_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.SendFax(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_DownloadFaxCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.DownloadFax();
            multifunctionalDevice.DownloadFax();
            multifunctionalDevice.DownloadFax();

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.DownloadFax();
            multifunctionalDevice.DownloadFax();
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.Fax();
            multifunctionalDevice.Fax();

            // 5 pobranych faxów, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.DownloadFaxCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_Counter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.SendFax(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.SendFax(in doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.SendFax(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Fax();
            multifunctionalDevice.Fax();
            multifunctionalDevice.PowerOn();

            multifunctionalDevice.Fax();

            // 5 użycia faxu, gdy urządzenie włączone
            Assert.AreEqual(5, multifunctionalDevice.FaxCounter);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver2
{
    public interface IFax : IDevice
    {
        public int FaxCounter { get; set; } // Liczba użytych razy faxu
        public int DownloadFaxCounter { get; set; } // Liczba pobranych dokumentów za pomocą faxu
        public void SendFax(in IDocument document);
        public void DownloadFax();
        public void Fax();
    }

    public class MultifunctionalDevice : Copier, IFax
    {
        public int FaxCounter { get; set; }
        public int DownloadFaxCounter { get; set; }

        public void DownloadFax()
        {
            if (state == IDevice.State.off)
                return;
            FaxCounter++;
            DownloadFaxCounter++;
            Random random = new Random();
            IDocument document;
            switch(random.Next(3))
            {
                case 0:
                    document = new TextDocument("aaa.txt");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 1:
                    document = new PDFDocument("aaa.pdf");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;

                case 2:
                    document = new ImageDocument("aaa.jpg");
                    Console.WriteLine($"{DateTime.Now} Pobrano fax: {document.GetFileName()}");
                    break;
            }
        }

        public void SendFax(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Wysłano fax: {document.GetFileName()}");
        }

        public void Fax()
        {
            SendFax(new ImageDocument("Image.jpg"));
            DownloadFax();
        }
    }
}
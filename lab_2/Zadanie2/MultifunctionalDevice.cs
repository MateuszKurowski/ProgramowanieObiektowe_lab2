using System;

namespace ver2
{
    public interface IFax : IDevice
    {
        /// <summary>
        /// Liczba użycia faksu
        /// </summary>
        public int FaxCounter { get; set; }

        /// <summary>
        /// Liczba pobranych faksów
        /// </summary>
        public int DownloadFaxCounter { get; set; }

        /// <summary>
        /// Jeśli urządzenie jest włączone wysyła faks
        /// </summary>
        /// <param name="document">Dokument od wysłania</param>
        public void SendFax(in IDocument document);

        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera faks
        /// </summary>
        public void DownloadFax();

        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera i wysyła faks
        /// </summary>
        public void FullFax();
    }

    /// <summary>
    /// Urządzenie wielofunkcyjne do obsługi faksu i kserokopiarki (drukarki i skanera)
    /// </summary>
    public class MultifunctionalDevice : Copier, IFax
    {
        /// <summary>
        /// Liczba użycia faksu
        /// </summary>
        public int FaxCounter { get; set; }

        /// <summary>
        /// Liczba pobranych faksów
        /// </summary>
        public int DownloadFaxCounter { get; set; }

        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera faks
        /// </summary>
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

        /// <summary>
        /// Jeśli urządzenie jest włączone wysyła faks
        /// </summary>
        /// <param name="document">Dokument od wysłania</param>
        public void SendFax(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            FaxCounter++;
            Console.WriteLine($"{DateTime.Now} Wysłano fax: {document.GetFileName()}");
        }

        /// <summary>
        /// Jeśli urządzenie jest włączone pobiera i wysyła faks
        /// </summary>
        public void FullFax()
        {
            SendFax(new ImageDocument("Image.jpg"));
            DownloadFax();
        }
    }
}
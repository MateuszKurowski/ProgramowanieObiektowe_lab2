using System;

namespace ver3
{
    /// <summary>
    /// Obiekt imitujący drukarke
    /// </summary>
    public class Printer : BaseDevice, IPrinter
    {
        /// <summary>
        /// Liczba wydrukowanych dokumentów
        /// </summary>
        public int PrintCounter { get; set; }

        /// <summary>
        /// Jeśli urządzenie jest włączone drukuje dokument
        /// </summary>
        /// <param name="document">Dokument do drukowania</param>
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }
    }
}
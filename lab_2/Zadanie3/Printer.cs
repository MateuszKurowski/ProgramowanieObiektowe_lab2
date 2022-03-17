using System;

namespace ver3
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; set; }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
                return;
            PrintCounter++;
            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
        }
    }
}
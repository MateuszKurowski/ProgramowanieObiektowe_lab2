using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
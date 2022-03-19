﻿using System;

namespace ver3
{
    public interface IDevice
    {
        enum State { on, off };

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter { get; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            state = IDevice.State.on;
            Counter++;
            Console.WriteLine("Device is on ...");
        }

        public void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

    public interface IFax
    {
        public int FaxCounter { get; set; } // Liczba użytych razy faxu
        public int DownloadFaxCounter { get; set; } // Liczba pobranych dokumentów za pomocą faxu
        public void SendFax(in IDocument document);
        public void DownloadFax();
        public void FullFax();
    }

}
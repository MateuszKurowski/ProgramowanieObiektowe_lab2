using System;

namespace ver2
{
    /// <summary>
    /// Interfejs urządzenia
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Obsługiwane stany urządzeń
        /// </summary>
        enum State { off, on };

        /// <summary>
        /// Uruchamia urządzenie
        /// </summary>
        void PowerOn();

        /// <summary>
        /// Wyłącza urządzenie
        /// </summary>
        void PowerOff();

        /// <summary>
        /// Zwraca obecny stan urządzenia
        /// </summary>
        State GetState();

        /// <summary>
        /// Liczba uruchomień urządzeń
        /// </summary>
        int Counter { get; }
    }

    /// <summary>
    /// Abstracyjna klasa urządzeń
    /// </summary>
    public abstract class BaseDevice : IDevice
    {
        /// <summary>
        /// Obecny stan urządzenia
        /// </summary>
        protected IDevice.State state = IDevice.State.off;

        /// <summary>
        /// Zwraca stan urządzenia
        /// </summary>
        public IDevice.State GetState() => state;

        /// <summary>
        /// Uruchamia urządzenie
        /// </summary>
        public void PowerOn()
        {
            if (state == IDevice.State.on)
                return;
            state = IDevice.State.on;
            Counter++;
            Console.WriteLine("Device is on ...");
        }

        /// <summary>
        /// Wyłącza urządzenie
        /// </summary>
        public void PowerOff()
        {
            if (state == IDevice.State.off)
                return;
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        /// <summary>
        /// Liczba uruchomień urządzeń
        /// </summary>
        public int Counter { get; private set; } = 0;
    }

    /// <summary>
    /// Interfejs drukarki
    /// </summary>
    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Jeśli urządzenie jest włączone drukuje dokument
        /// </summary>
        /// <param name="document">Dokument do drukowania</param>
        void Print(in IDocument document);
    }

    /// <summary>
    /// Interfejs skanera
    /// </summary>
    public interface IScanner : IDevice
    {
        /// <summary>
        /// Jeśli urządzenie jest włączone skanuje dokument do podanego typu
        /// </summary>
        /// <param name="document">Dokument zeskanowany</param>
        /// <param name="formatType">Typ dokumentu</param>
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}
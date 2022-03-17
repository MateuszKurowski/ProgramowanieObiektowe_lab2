using System;

namespace ver4
{
    public interface IDevice
    {
        enum State { on, off, standby };

        public void PowerOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            Counter++;
            state = State.on;
        }
        void PowerOff() // wyłącza urządzenie, zmienia stan na `off
        {
            state = State.off;
        }
        void StandbyOn() // uruchamia urządzenie, zmienia stan na `on`
        {
            state = State.standby;
        }
        void StandbyOff() // wyłącza urządzenie, zmienia stan na `off
        {
            state = State.on;
        }
        abstract protected void SetState(State state);
        protected State state { get; set; }
        State GetState()// zwraca aktualny stan urządzenia
        {
            return state;
        }
        int Counter { get; protected set; }  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                              // np. liczbę uruchomień
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
        void Scan(out IDocument document);
    }

}
using System;
using System.Threading;

using static ver4.IDevice;

namespace ver4
{
    public class Copier : IPrinter, IScanner
    {
        private int _scanner = 0;
        private int _printer = 0;
        public int CopierCounter { get { return ((IDevice)this).Counter; } }
        private static State _State = State.off;
        public State GetCopierState()// zwraca aktualny stan urządzenia
        {
            return _State;
        }
        void IDevice.SetState(State state)
        {
            var printerState = ((IPrinter)this).GetState();
            var scannerState = ((IScanner)this).GetState();
            if (printerState == scannerState)
                _State = printerState;
            else if (printerState != State.off && scannerState == State.off)
                _State = printerState;
            else if (printerState == State.off && scannerState != State.off)
                _State = scannerState;
            else
                _State = State.on;
        }
        public void ScanByCopier(IDocument document)
        {
            if (((IScanner)this).GetState() == State.off)
                return;
            if (((IScanner)this).GetState() == State.standby)
                ((IScanner)this).StandbyOff();
            if (_scanner % 2 == 0)
            {
                ((IScanner)this).StandbyOn();
                Thread.Sleep(1000);
                ((IScanner)this).StandbyOff();
                _scanner = 0;
            }
            ((IPrinter)this).StandbyOn();
            ((IScanner)this).Scan(out document);
            _scanner++;
        }

        public void ScanByCopier(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (((IScanner)this).GetState() == State.off)
                return;
            if (((IScanner)this).GetState() == State.standby)
                ((IScanner)this).StandbyOff();
            if (_scanner > 0 && _scanner % 2 == 0)
            {
                ((IScanner)this).StandbyOn();
                Thread.Sleep(1000);
                ((IScanner)this).StandbyOff();
                _scanner = 0;
            }
            ((IPrinter)this).StandbyOn();
            ((IScanner)this).Scan(out document, formatType);
            _scanner++;
        }

        public void PrintByCopier(in IDocument document)
        {
            if (((IPrinter)this).GetState() == State.off)
                return;
            if (((IPrinter)this).GetState() == State.standby)
                ((IPrinter)this).StandbyOff();
            if (_printer > 0 &&_printer % 3 == 0)
            {
                ((IPrinter)this).StandbyOn();
                Thread.Sleep(1000);
                ((IPrinter)this).StandbyOff();
                _printer = 0;
            }
            ((IScanner)this).StandbyOn();
            ((IPrinter)this).Print(in document);
            _printer++;
        }

        public void ScanAndPrint()
        {
            if (GetCopierState() == State.off)
                return;
            else if (GetCopierState() == State.standby)
                CopierStandbyOn();
            IDocument doc = null;
            ScanByCopier(out doc, IDocument.FormatType.JPG);
            if (doc == null)
                return;
            PrintByCopier(in doc);
        }

        public void CopierStandbyOn()
        {
            ((IScanner)this).StandbyOn();
            ((IPrinter)this).StandbyOn();
        }
        public void CopierStandbyOff()
        {
            ((IScanner)this).StandbyOff();
            ((IPrinter)this).StandbyOff();
        }

    }
}
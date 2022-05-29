using System;
using System.IO;

namespace TempElementsLib
{
    public class TempTxtFile : TempFile
    {
        public readonly TextReader txtReader;
        public readonly TextWriter txtWriter;
        private bool _IsDestroyed;

        public TempTxtFile() : base(@"C:\Users\USER\AppData\Local\Temp\" + Guid.NewGuid() + ".txt")
        {
            txtReader = new StreamReader(FileStream_);
            txtWriter = new StreamWriter(FileStream_);
            _IsDestroyed = false;
        }

        public TempTxtFile(string pathName) : base(pathName)
        {
            txtReader = new StreamReader(FileStream_);
            txtWriter = new StreamWriter(FileStream_);
            _IsDestroyed = false;
        }

        public string ReadAllText() => txtReader.ReadToEnd();

        public string ReadLine() => txtReader.ReadLine();

        public void Write(string value)
        {
            txtWriter.Write(value);
            txtWriter.Flush();
        }

        public void WriteLine(string value)
        {
            txtWriter.WriteLine(value);
            txtWriter.Flush();
        }

        ~TempTxtFile() => Dispose(false);

        protected new void Dispose(bool disposing)
        {
            if (_IsDestroyed)
            {
                return;
            }
            try
            {
                if (disposing)
                {
                    txtReader.Close();
                    txtReader.Dispose();
                    txtWriter.Close();
                    txtWriter.Dispose();
                }
                base.Dispose(false);
            }
            catch (Exception)
            {
                throw;
            }

            _IsDestroyed = true;
        }
    }
}
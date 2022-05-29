using System;
using System.IO;
using System.Text;

using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempFile : ITempFile, IMove
    {
        public readonly FileStream FileStream_;
        public readonly FileInfo FileInfo_;
        public string FilePath => FileInfo_.FullName;
        public bool IsDestroyed => _IsDestroyed;
        private bool _IsDestroyed;

        public TempFile()
        {
            var fileName = @"C:\Users\USER\AppData\Local\Temp\" + Guid.NewGuid() + ".tmp";
            FileStream_ = new FileStream(FileInfo_.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileInfo_ = new FileInfo(fileName);
            _IsDestroyed = false;
        }
        public TempFile(string fileName)
        {
            FileInfo_ = new FileInfo(fileName);
            FileStream_ = new FileStream(FileInfo_.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _IsDestroyed = false;
        }

        ~TempFile() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_IsDestroyed)
            {
                return;
            }
            try
            {
                if (disposing)
                {
                    FileStream_.Flush();
                    FileStream_.Close();
                    FileStream_.Dispose();
                }
                FileInfo_.Delete();
            }
            catch (Exception)
            {
                throw;
            }

            _IsDestroyed = true;
        }

        public void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            FileStream_.Write(info, 0, info.Length);
            FileStream_.Flush();
        }
        
        public void Move(string pathName) => FileInfo_.MoveTo(pathName);
    }
}
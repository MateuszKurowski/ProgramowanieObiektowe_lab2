using System;
using System.IO;

using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempDir : ITempDir, IMove
    {
        public readonly DirectoryInfo DirectoryInfo_;
        public string DirPath => DirectoryInfo_.FullName;
        public bool IsEmpty => DirectoryInfo_.GetFiles().Length == 0;
        public bool IsDestroyed => _IsDestroyed;
        private bool _IsDestroyed;

        public TempDir()
        {
            var directoryName = @"C:\Users\USER\AppData\Local\Temp\" + Guid.NewGuid();
            DirectoryInfo_ = new DirectoryInfo(directoryName);
            _IsDestroyed = false;
        }
        public TempDir(string directoryName)
        {
            DirectoryInfo_ = new DirectoryInfo(directoryName);
            _IsDestroyed = false;
        }

        ~TempDir() => Dispose(false);

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
                    DirectoryInfo_.Delete();
                }
            }
            catch (Exception)
            {
                throw;
            }

            _IsDestroyed = true;
        }

        public void Empty()
        {
            foreach (FileInfo file in DirectoryInfo_.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in DirectoryInfo_.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }

        public void Move(string pathName) => DirectoryInfo_.MoveTo(pathName);
    }
}

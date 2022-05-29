using System;
using System.Collections.Generic;

using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempElementsList : ITempElements
    {
        private bool disposed;
        private readonly List<ITempElement> elements = new List<ITempElement>();

        public IReadOnlyCollection<ITempElement> Elements => elements;

        ~TempElementsList() => Dispose(false);

        public T AddElement<T>() where T : ITempElement, new()
        {
            T element = new T();
            elements.Add(element);
            return element;
        }

        public void DeleteElement<T>(T element) where T : ITempElement, new()
        {
            if (elements.Find(x => x.Equals(element)) != null)
            {
                element.Dispose();
                elements.Remove(element);
            }
        }

        public void MoveElementTo<T>(T element, string newPath) where T : ITempElement, IMove, new() => element.Move(newPath);

        public void RemoveDestroyed()
            => elements.RemoveAll((e) => e.IsDestroyed);

        public bool IsEmpty => ((ITempElements)this).IsEmpty;


        #region Dispose section ==============================================
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    if (disposing)
                    {
                        foreach (var element in elements)
                        {
                            element.Dispose();
                        }
                       elements.Clear();
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~TempDirsAndFolders()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
using TempElementsLib;

using System;
using System.IO;

namespace TempElementsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Zadanie 1
            using (TempFile tempFile1 = new TempFile("test.tmp"))
            {
                tempFile1.AddText("Testowe wpisanie binarne tekstu");
                Console.ReadKey();
            }
            Console.ReadKey();

            TempFile tempFile2 = new TempFile("test.tmp");
            try
            {
                tempFile2.AddText("Testowe wpisanie binarne tekstu");
                Console.ReadKey();
                tempFile2.Dispose();
                tempFile2.AddText("Tekst wpisany po zamknięciu");
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();

            // Zadanie 2
            using (TempTxtFile tempFile3 = new TempTxtFile("test.txt"))
            {
                tempFile3.Write("Write");
                tempFile3.WriteLine("WriteLine");
                Console.ReadKey();
            }
            Console.ReadKey();

            // Zadanie 3
            using (TempDir tempDirectory = new TempDir("KatalogTestowy"))
            {
                Console.ReadKey();
                tempDirectory.Dispose();
                Console.ReadKey();
            }
            Console.ReadKey();

            // Zadanie 4
            TempElementsList tempList = new TempElementsList();
            TempFile tempTxtFile = tempList.AddElement<TempFile>();
            tempTxtFile.AddText("Testowe wpisanie binarne tekstu");
            TempFile tempTxtFile2 = tempList.AddElement<TempFile>();
            tempTxtFile2.AddText("Wpis do drugiego pliku");
            tempList.MoveElementTo(tempTxtFile, Path.GetTempPath() + "PrzeniesionyPlik.tmp");
            tempList.Dispose();
        }
    }
}
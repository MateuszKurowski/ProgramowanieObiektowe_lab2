using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Zadanie3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "../../../../TextXML.txt";
            var fileContent = string.Empty;
            try
            {
                using StreamReader reader = new StreamReader(path);
                fileContent = reader.ReadToEnd();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nie ma takiego pliku.");
                return;
            }
            var index = fileContent.IndexOf("\r\n");
            var year = fileContent[..index];

            XDocument document = XDocument.Parse(fileContent[(index + 2)..]);
            var results = document.Descendants("Customer")
                                    .Where(x => x.Element("Orders").HasElements)
                                    .OrderBy(x => x.Element("Country")?.Value)
                                    .ThenBy(x => x.Element("City")?.Value)
                                    .ThenBy(x => x.Element("CompanyName")?.Value)
                                    .Select(x => x.Element("CompanyName")?.Value)
                                    .ToList();

            if (results == null || results.Count < 1)
                Console.WriteLine("empty");
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }


        }
    }
}

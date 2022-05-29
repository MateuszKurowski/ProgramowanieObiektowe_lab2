using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace LingToXml
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = "PlikXML.txt";

            var fileContent = string.Empty;
            using (StreamReader streamReader = new StreamReader(path))
            {
                fileContent = streamReader.ReadToEnd();
            }
            var index = fileContent.IndexOf("\r\n");
            var year = fileContent.Substring(0, index);

            XDocument xmlDocument = XDocument.Parse(fileContent.Substring(index + 2));
            var results = xmlDocument.Descendants("Customer")
                                    .Where(x => x.Element("Orders").HasElements)
                                    .OrderBy(x => x.Element("Country").Value)
                                    .ThenBy(x => x.Element("City").Value)
                                    .ThenBy(x => x.Element("CompanyName").Value)
                                    .Select(x => x.Element("CompanyName").Value)
                                    .ToList();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}

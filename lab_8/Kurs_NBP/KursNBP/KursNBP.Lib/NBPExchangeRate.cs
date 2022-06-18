using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace KursNBP.Lib
{
    /// <summary>
    /// Downloads and calculate data about exchange rate in given range.
    /// </summary>
    public class NBPExchangeRate
    {
        public string Currency { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

        public double AverageExchangeRate { get; set; }
        public double StandardDeviation { get; set; }
        public double MinimumExchangeRate { get; set; }
        public double MaximumExchangeRate { get; set; }

        private readonly List<ExchangeRateNPB> _ExchangeRates = new List<ExchangeRateNPB>();
        private readonly List<string> _FileNames = new List<string>();

        private const string _NBPUrl = "https://www.nbp.pl/kursy/xml/";
        private readonly string _Path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName) + "\\ExchangeRatesXML";

        /// <summary>
        /// Initializes a new instance of the <see cref="NBPExchangeRate"/> class.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public NBPExchangeRate(string currency, DateTime startDate, DateTime endDate)
        {
            Currency = currency;
            StartDate = startDate;
            EndDate = endDate;

            DownloadExchangeRate();
            ProcessXmls();
            CalculateData();
        }

        /// <summary>
        /// Downloads the exchange rates as XML to dedicated folder.
        /// </summary>
        private void DownloadExchangeRate()
        {
            WebClient client = new WebClient();
            var fileNamesInNBP = client.DownloadString(_NBPUrl + "dir.aspx?tt=A");
            var startDay = int.Parse(StartDate.ToString("dd"));
            var startMonth = int.Parse(StartDate.ToString("MM"));
            var startYear = int.Parse(StartDate.ToString("yy"));

            var endDay = int.Parse(EndDate.ToString("dd"));
            var endMonth = int.Parse(EndDate.ToString("MM"));
            var endYear = int.Parse(EndDate.ToString("yy"));

            var tempDay = startDay;
            var tempMonth = startMonth;
            var tempYear = startYear;

            if (startYear < endYear) // Poprzednie lata
            {
                for (; tempYear < endYear; tempYear++)
                {
                    var regexPastYear = new Regex($"<a href=\"a[0-9]{{3}}z{tempYear:00}[0-9]{{4}}.xml\"");
                    foreach (Match matchPastYear in regexPastYear.Matches(fileNamesInNBP))
                    {
                        if (matchPastYear?.Value == null || matchPastYear?.Value?.Length < 1)
                            continue;
                        _FileNames.Add(matchPastYear?.Value?.Substring(9, 15));
                    }
                }
            }

            if (tempYear == endYear) // Poprzednie miesiące
            {
                for (; tempMonth < endMonth; tempMonth++)
                {
                    for (int i = 0; i <= 31; i++)
                    {
                        var regexPastYear = new Regex($"<a href=\"a[0-9]{{3}}z{tempYear:00}{tempMonth:00}{i:00}.xml\"");
                        foreach (Match matchPastYear in regexPastYear.Matches(fileNamesInNBP))
                        {
                            if (matchPastYear?.Value == null || matchPastYear?.Value?.Length < 1)
                                continue;
                            _FileNames.Add(matchPastYear?.Value?.Substring(9, 15));
                        }
                    }
                }
                for (; tempDay <= endDay; tempDay++) // Ostatni miesiąc
                {
                    var regexPastYear = new Regex($"<a href=\"a[0-9]{{3}}z{tempYear:00}{tempMonth:00}{tempDay:00}.xml\"");
                    foreach (Match matchPastYear in regexPastYear.Matches(fileNamesInNBP))
                    {
                        if (matchPastYear?.Value == null || matchPastYear?.Value?.Length < 1)
                            continue;
                        _FileNames.Add(matchPastYear?.Value?.Substring(9, 15));
                    }
                }
            }

            if (_FileNames.Count < 1)
                throw new Exception("Query returned no result");

            if (_FileNames.Count > 0 && !Directory.Exists(_Path))
                Directory.CreateDirectory(_Path);

            foreach (var fileName in _FileNames)
            {
                client.DownloadFile(_NBPUrl + fileName, _Path + "\\" + fileName);
            }
        }

        /// <summary>
        /// Processes all XML files.
        /// </summary>
        private void ProcessXmls()
        {
            foreach (var documentName in _FileNames)
            {
                var document = XDocument.Parse(File.ReadAllText(_Path + "\\" + documentName, Encoding.GetEncoding("ISO-8859-1")));
                if (document == null)
                    continue;
                var node = document.Descendants("tabela_kursow");
                var exchangeRate = new ExchangeRateNPB();
                var dateString = document?
                                        .Descendants("tabela_kursow")?
                                        .Elements("data_publikacji")?
                                        .FirstOrDefault()?
                                        .Value;
                var valueString = document?
                                .Descendants("tabela_kursow")?
                                .Elements("pozycja")?
                                .Where(x => x.Element("kod_waluty").Value == Currency)?
                                .Elements("kurs_sredni")?
                                .FirstOrDefault()?
                                .Value;

                if (!string.IsNullOrWhiteSpace(dateString) || !string.IsNullOrWhiteSpace(valueString))
                    continue;

                if (!double.TryParse(valueString, out var value))
                    continue;
                if (!DateTime.TryParse(dateString, out var date))
                    continue;
                exchangeRate.ExchangeRate = value;
                exchangeRate.Date = date;
                _ExchangeRates.Add(exchangeRate);
            }

            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(_Path);
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                di.Delete(true);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Calculates data on exchange rates.
        /// </summary>
        private void CalculateData()
        {
            if (_ExchangeRates.Count < 1)
                return;

            var listOfExchanges = _ExchangeRates.ConvertAll(x => x.ExchangeRate);
            AverageExchangeRate = listOfExchanges.Sum() / listOfExchanges.Count;
            MaximumExchangeRate = listOfExchanges.Max();
            MinimumExchangeRate = listOfExchanges.Min();

            var average = listOfExchanges.Average();
            var sum = listOfExchanges.Sum(d => Math.Pow(d - average, 2));
            StandardDeviation = Math.Sqrt(sum / listOfExchanges.Count);
        }

        /// <summary>
        /// Convert course data in the given range to a string
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var result = string.Empty;
            result += $"Date range: {StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
            result += Environment.NewLine;
            result += Environment.NewLine;
            result += $"Average rate (arithmetic mean): {AverageExchangeRate:0.##}";
            result += Environment.NewLine;
            result += $"Standard deviation: {StandardDeviation:0.##}";
            result += Environment.NewLine;
            result += $"Minimum exchange rate: {MinimumExchangeRate:0.##}";
            result += Environment.NewLine;
            result += $"Maximum exchange rate: {MaximumExchangeRate:0.##}";
            result += Environment.NewLine;
            result += Environment.NewLine;
            result += Environment.NewLine;

            var min = _ExchangeRates.Where(x => x.ExchangeRate == MinimumExchangeRate).Select(x => x.Date);
            if (min.Count() == 1)
                result += "Exchange rate was minimal in day: ";
            else result += "Exchange rate was minimal in days: ";
            result += Environment.NewLine;
            foreach (var date in min)
            {
                result += date.ToShortDateString();
                result += Environment.NewLine;
            }
            result += Environment.NewLine;

            var max = _ExchangeRates.Where(x => x.ExchangeRate == MaximumExchangeRate).Select(x => x.Date);
            if (max.Count() == 1)
                result += "Exchange rate was maximum in day: ";
            else result += "Exchange rate was maximum in days: ";
            result += Environment.NewLine;
            foreach (var date in max)
            {
                result += date.ToShortDateString();
                result += Environment.NewLine;
            }
            return result;
        }

        private struct ExchangeRateNPB
        {
            public double ExchangeRate { get; set; }
            public DateTime Date { get; set; }
        }
    }

    public static class AvailableCurrency
    {
        public static readonly string[] AvailabeCurrency =
        {
            "USD",
            "EUR",
            "CHF",
            "GBP"
        };
    }
}
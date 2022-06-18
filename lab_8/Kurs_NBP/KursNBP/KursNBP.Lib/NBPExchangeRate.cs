using System;
using System.Xml.Linq;

namespace KursNBP.Lib
{
    public class NBPExchangeRate
    {
        public string Currency { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        private XDocument _xDocument;

        public decimal AvverageExchangeRate { get; set; }
        public double StandardDeviation { get; set; }
        public decimal MinimumExchangeRate { get; set; }
        public decimal MaximumExchangeRate { get; set; }

        public NBPExchangeRate(string currency, DateTime startDate, DateTime endDate)
        {
            Currency = currency;
            StartDate = startDate;
            EndDate = endDate;

            DownloadExchangeRate();

            ProcessXml();
        }

        private void DownloadExchangeRate()
        {

        }

        private void ProcessXml()
        {
            
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
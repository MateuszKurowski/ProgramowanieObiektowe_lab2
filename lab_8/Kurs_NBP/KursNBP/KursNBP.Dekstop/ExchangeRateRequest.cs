using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursNBP.Dekstop
{
    public class ExchangeRateRequest : IDataErrorInfo
    {
        string IDataErrorInfo.this[string columnName] => throw new NotImplementedException();

        public string Currency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        string IDataErrorInfo.Error => throw new NotImplementedException();
    }
}

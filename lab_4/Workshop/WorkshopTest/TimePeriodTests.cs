using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WorkshopLib;

namespace WorkshopTest
{
    [TestClass]
    public class TimePeriodUnitTestsConstructors
    {
        #region Constructor tests ======================
        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)1, (long)1, (long)1, (long)3661)]
        [DataRow((long)5, (long)5, (long)5, (long)18305)]
        [DataRow((long)15, (long)40, (long)55, (long)56455)]
        public void Constructor_3params(long hours, long minutes, long seconds, long expectedTime)
        {
            TimePeriod t = new TimePeriod(hours, minutes, seconds);

            Assert.AreEqual(expectedTime, t.Time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)1, (long)61, (long)-5)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_OutOfRange(long hours, long minutes, long seconds)
        {
            TimePeriod t = new TimePeriod(hours, minutes, seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)1, (long)1, (long)3660)]
        [DataRow((long)5, (long)5, (long)18300)]
        [DataRow((long)15, (long)40, (long)56400)]
        public void Constructor_2params(long hours, long minutes, long expectedTime)
        {
            TimePeriod t = new TimePeriod(hours, minutes);

            Assert.AreEqual(expectedTime, t.Time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)1, (long)-5)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_OutOfRange(long hours, long minutes)
        {
            TimePeriod t = new TimePeriod(hours, minutes);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)1, (long)1)]
        [DataRow((long)5, (long)5)]
        [DataRow((long)15, (long)15)]
        public void Constructor_1param(long seconds, long expectedTime)
        {
            TimePeriod t = new TimePeriod(seconds);

            Assert.AreEqual(expectedTime, t.Time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((long)-25)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_OutOfRange(long hours)
        {
            TimePeriod t = new TimePeriod(hours);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("1:1:1", (long)3661)]
        [DataRow("5:5:5", (long)18305)]
        [DataRow("15:40:40", (long)56440)]
        public void Constructor_Stringparam(string time, long expectedTime)
        {
            TimePeriod t = new TimePeriod(time);

            Assert.AreEqual(expectedTime, t.Time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("1;30;40")]
        [DataRow("1;30:40")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Stringparam_Badsyntax(string time)
        {
            TimePeriod t = new TimePeriod(time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("-25:61:40")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Stringparam_OutOfRange(string time)
        {
            TimePeriod t = new TimePeriod(time);
        }
        #endregion

        #region ToString
        [TestMethod, TestCategory("ToString")]
        [DataRow((long)1, (long)1, (long)1, "1:01:01")]
        [DataRow((long)5, (long)5, (long)5, "5:05:05")]
        [DataRow((long)15, (long)40, (long)15, "15:40:15")]
        public void ToString_Overload(long hours, long minutes, long seconds, string expectedString)
        {
            TimePeriod t = new TimePeriod(hours, minutes, seconds);
            Assert.AreEqual(t.ToString(), expectedString);
        }
        #endregion

        #region Equals
        [TestMethod, TestCategory("Equals")]
        [DataRow((long)1, (long)1, (long)1, (long)1, (long)1, (long)1, true)]
        [DataRow((long)1, (long)2, (long)5, (long)1, (long)2, (long)1, false)]
        [DataRow((long)15, (long)40, (long)3, (long)15, (long)40, (long)3, true)]
        [DataRow((long)15, (long)40, (long)55, (long)15, (long)37, (long)4, false)]
        public void Equals(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, bool areEqual)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1.Equals(t2), areEqual);
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow((long)1, (long)1, (long)1, (long)1, (long)1, (long)1, true)]
        [DataRow((long)1, (long)2, (long)5, (long)1, (long)2, (long)1, false)]
        [DataRow((long)15, (long)40, (long)3, (long)15, (long)40, (long)3, true)]
        [DataRow((long)15, (long)40, (long)55, (long)15, (long)37, (long)4, false)]
        public void Equals_Operator(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, bool areEqual)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 == t2, areEqual);
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow((long)1, (long)1, (long)1, (long)1, (long)1, (long)1, false)]
        [DataRow((long)1, (long)2, (long)5, (long)1, (long)2, (long)1, true)]
        [DataRow((long)15, (long)40, (long)3, (long)15, (long)40, (long)3, false)]
        [DataRow((long)15, (long)40, (long)55, (long)15, (long)37, (long)4, true)]
        public void NotEquals_Operator(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, bool areEqual)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 != t2, areEqual);
        }
        #endregion

        #region CompareTo
        [TestMethod, TestCategory("CompareTo")]
        [DataRow((long)1, (long)1, (long)2, (long)1, (long)1, (long)1, 1)]
        [DataRow((long)1, (long)2, (long)1, (long)1, (long)1, (long)1, 1)]
        [DataRow((long)2, (long)1, (long)1, (long)1, (long)1, (long)1, 1)]
        [DataRow((long)15, (long)40, (long)3, (long)17, (long)40, (long)3, -1)]
        public void CompareTo(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, int compareResult)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1.CompareTo(t2), compareResult);
        }

        [TestMethod, TestCategory("CompareTo")]
        [DataRow((long)1, (long)1, (long)2, (long)1, (long)1, (long)1, true)]
        [DataRow((long)1, (long)2, (long)1, (long)1, (long)1, (long)1, true)]
        [DataRow((long)2, (long)1, (long)1, (long)1, (long)1, (long)1, true)]
        [DataRow((long)15, (long)40, (long)3, (long)17, (long)40, (long)3, false)]
        public void CompareTo_operatorMore(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, bool compareResult)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 > t2, compareResult);
        }

        [TestMethod, TestCategory("CompareTo")]
        [DataRow((long)1, (long)1, (long)2, (long)1, (long)1, (long)1, false)]
        [DataRow((long)1, (long)2, (long)1, (long)1, (long)1, (long)1, false)]
        [DataRow((long)2, (long)1, (long)1, (long)1, (long)1, (long)1, false)]
        [DataRow((long)15, (long)40, (long)3, (long)17, (long)40, (long)3, true)]
        public void CompareTo_operatorLess(long hours1, long minutes1, long seconds1, long hours2, long minutes2, long seconds2, bool compareResult)
        {
            TimePeriod t1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod t2 = new TimePeriod(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 < t2, compareResult);
        }
        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

using WorkshopLib;

namespace WorkshopTest
{
    [TestClass]
    public class TimeUnitTestsConstructors
    {
        #region Constructor tests ======================
        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1)]
        [DataRow((byte)5, (byte)5, (byte)5, (byte)5, (byte)5, (byte)5)]
        [DataRow((byte)15, (byte)40, (byte)55, (byte)15, (byte)40, (byte)55)]
        public void Constructor_3params(byte hours, byte minutes, byte seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours, minutes, seconds);

            Assert.AreEqual(expectedHours, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)61, (byte)15)]
        [DataRow((byte)25, (byte)5, (byte)3)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_OutOfRange(byte hours, byte minutes, byte seconds)
        {
            Time t = new Time(hours, minutes, seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1)]
        [DataRow((byte)5, (byte)5, (byte)5, (byte)5)]
        [DataRow((byte)15, (byte)40, (byte)15, (byte)40)]
        public void Constructor_2params(byte hours, byte minutes, byte expectedHours, byte expectedMinutes)
        {
            Time t = new Time(hours, minutes);

            Assert.AreEqual(expectedHours, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)61)]
        [DataRow((byte)25, (byte)5)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_OutOfRange(byte hours, byte minutes)
        {
            Time t = new Time(hours, minutes);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)1)]
        [DataRow((byte)5, (byte)5)]
        [DataRow((byte)15, (byte)15)]
        public void Constructor_1param(byte hours, byte expectedHours)
        {
            Time t = new Time(hours);

            Assert.AreEqual(expectedHours, t.Hours);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)25)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_OutOfRange(byte hours)
        {
            Time t = new Time(hours);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("1:1:1", (byte)1, (byte)1, (byte)1)]
        [DataRow("5:5:5", (byte)5, (byte)5, (byte)5)]
        [DataRow("15:40:40", (byte)15, (byte)40, (byte)40)]
        public void Constructor_Stringparam(string time, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(time);

            Assert.AreEqual(expectedHours, t.Hours);
            Assert.AreEqual(expectedMinutes, t.Minutes);
            Assert.AreEqual(expectedSeconds, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("1;30;40")]
        [DataRow("1;30:40")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Stringparam_Badsyntax(string time)
        {
            Time t = new Time(time);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("25:61:40")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_Stringparam_OutOfRange(string time)
        {
            Time t = new Time(time);
        }
        #endregion

        #region ToString
        [TestMethod, TestCategory("ToString")]
        [DataRow((byte)1, (byte)1, (byte)1, "01:01:01")]
        [DataRow((byte)5, (byte)5, (byte)5, "05:05:05")]
        [DataRow((byte)15, (byte)40, (byte)15, "15:40:15")]
        public void ToString_Overload(byte hours, byte minutes, byte seconds, string expectedString)
        {
            Time t = new Time(hours, minutes, seconds);
            Assert.AreEqual(t.ToString(), expectedString);
        }
        #endregion

        #region Equals
        [TestMethod, TestCategory("Equals")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, true)]
        [DataRow((byte)1, (byte)2, (byte)5, (byte)1, (byte)2, (byte)1, false)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)15, (byte)40, (byte)3, true)]
        [DataRow((byte)15, (byte)40, (byte)55, (byte)15, (byte)37, (byte)4, false)]
        public void Equals(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, bool areEqual)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1.Equals(t2), areEqual);
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, true)]
        [DataRow((byte)1, (byte)2, (byte)5, (byte)1, (byte)2, (byte)1, false)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)15, (byte)40, (byte)3, true)]
        [DataRow((byte)15, (byte)40, (byte)55, (byte)15, (byte)37, (byte)4, false)]
        public void Equals_Operator(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, bool areEqual)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 == t2, areEqual);
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow((byte)1, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, false)]
        [DataRow((byte)1, (byte)2, (byte)5, (byte)1, (byte)2, (byte)1, true)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)15, (byte)40, (byte)3, false)]
        [DataRow((byte)15, (byte)40, (byte)55, (byte)15, (byte)37, (byte)4, true)]
        public void NotEquals_Operator(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, bool areEqual)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 != t2, areEqual);
        }
        #endregion

        #region CompareTo
        [TestMethod, TestCategory("CompareTo")]
        [DataRow((byte)1, (byte)1, (byte)2, (byte)1, (byte)1, (byte)1, 1)]
        [DataRow((byte)1, (byte)2, (byte)1, (byte)1, (byte)1, (byte)1, 1)]
        [DataRow((byte)2, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, 1)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)17, (byte)40, (byte)3, -1)]
        public void CompareTo(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, int compareResult)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1.CompareTo(t2), compareResult);
        }

        [TestMethod, TestCategory("CompareTo")]
        [DataRow((byte)1, (byte)1, (byte)2, (byte)1, (byte)1, (byte)1, true)]
        [DataRow((byte)1, (byte)2, (byte)1, (byte)1, (byte)1, (byte)1, true)]
        [DataRow((byte)2, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, true)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)17, (byte)40, (byte)3, false)]
        public void CompareTo_operatorMore(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, bool compareResult)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 > t2, compareResult);
        }

        [TestMethod, TestCategory("CompareTo")]
        [DataRow((byte)1, (byte)1, (byte)2, (byte)1, (byte)1, (byte)1, false)]
        [DataRow((byte)1, (byte)2, (byte)1, (byte)1, (byte)1, (byte)1, false)]
        [DataRow((byte)2, (byte)1, (byte)1, (byte)1, (byte)1, (byte)1, false)]
        [DataRow((byte)15, (byte)40, (byte)3, (byte)17, (byte)40, (byte)3, true)]
        public void CompareTo_operatorLess(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2, bool compareResult)
        {
            Time t1 = new Time(hours1, minutes1, seconds1);
            Time t2 = new Time(hours2, minutes2, seconds2);
            Assert.AreEqual(t1 < t2, compareResult);
        }
        #endregion
    }
}
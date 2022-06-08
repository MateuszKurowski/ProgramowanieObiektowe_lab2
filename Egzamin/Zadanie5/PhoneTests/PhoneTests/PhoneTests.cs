using Microsoft.VisualStudio.TestTools.UnitTesting;

using PhoneLib;

using System;

namespace PhoneTests
{
    [TestClass]
    public class PhoneTests
    {
        [TestMethod]
        public void Construtor_good()
        {
            var owner = "test";
            var number = "123456789";
            Phone p = new Phone(owner, number);

            Assert.AreEqual(owner, p.Owner);
            Assert.AreEqual(number, p.PhoneNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_null()
        {
            Phone p = new Phone("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_null2()
        {
            Phone p = new Phone(null, "123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_null3()
        {
            Phone p = new Phone(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_null_empty()
        {
            Phone p = new Phone("", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_null_empty2()
        {
            Phone p = new Phone(null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_empty()
        {
            Phone p = new Phone("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_empty2()
        {
            Phone p = new Phone("", "123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_empty3()
        {
            Phone p = new Phone("test", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_bad_number()
        {
            Phone p = new Phone("test", "12345678");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Construtor_bad_number2()
        {
            Phone p = new Phone("test", "123a56789");
        }
    }
}
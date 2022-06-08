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

        [TestMethod]
        public void AddContact_good()
        {
            var number = "123456789";
            var p = new Phone("Owner", "987654321");
            for (int i = 0; i < 3; i++)
            {
                p.AddContact("test" + i, number);
            }
            Assert.AreEqual(3, p.Count);
        }

        [TestMethod]
        public void AddContact_bad()
        {
            var number = "123456789";
            var p = new Phone("Owner", "987654321");
            p.AddContact("test", number);
            var result = p.AddContact("test", number);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddContact_book_is_full()
        {
            var number = "123456789";
            var p = new Phone("Owner", "987654321");
            for (int i = 0; i < 100; i++)
            {
                p.AddContact("test" + i, number);
            }
            p.AddContact("test pelny", number);
            Assert.AreEqual(3, p.Count);
        }

        [TestMethod]
        public void Call_good()
        {
            var number = "123456789";
            var name = "test";
            var p = new Phone("Owner", "987654321");
            p.AddContact(name, number);
            var result = p.Call(name);
            Assert.AreEqual($"Calling {number} ({name}) ...", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Call_bad()
        {
            var number = "123456789";
            var p = new Phone("Owner", "987654321");
            p.AddContact("nietest", number);
            var result = p.Call("test");
            Assert.AreEqual("error", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Call_bad2()
        {
            var p = new Phone("Owner", "987654321");
            var result = p.Call("test");
            Assert.AreEqual("error", result);
        }
    }
}
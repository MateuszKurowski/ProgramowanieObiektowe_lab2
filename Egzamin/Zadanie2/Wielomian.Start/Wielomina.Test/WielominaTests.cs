using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyMath;

namespace Tests
{
    [TestClass]
    public class WielomianTests
    {
        [TestMethod]
        public void StopienTest()
        {
            Wielomian w1 = new Wielomian();
            Wielomian w2 = new Wielomian(3);
            Wielomian w3 = new Wielomian(2, 3, 4, 5);
            Wielomian w4 = new Wielomian(2, 0, 0, 1);

            Assert.AreEqual(w1.Stopien, 0);
            Assert.AreEqual(w2.Stopien, 0);
            Assert.AreEqual(w3.Stopien, 3);
            Assert.AreEqual(w4.Stopien, 3);
        }

        [TestMethod]
        public void DodawanieTest()
        {
            Wielomian w1 = new Wielomian();
            Wielomian w2 = new Wielomian(3);
            Wielomian w3 = new Wielomian(2, 3, 4, 5);
            Wielomian w4 = new Wielomian(2, 0, 0, 1);

            Assert.AreEqual(w1 + w2, new Wielomian(3));
            Assert.AreEqual(w3 + w4, new Wielomian(4, 3, 4, 6));
        }
    }
}

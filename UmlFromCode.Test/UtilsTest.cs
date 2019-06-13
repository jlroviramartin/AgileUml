using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UmlFromCode.Test
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestNumberOfSetBits()
        {
            Assert.AreEqual(
                expected: 32,
                actual: Utils.NumberOfSetBits(unchecked((uint) -1)));

            Assert.AreEqual(
                expected: 0,
                actual: Utils.NumberOfSetBits(unchecked((uint) 0)));

            Assert.AreEqual(
                expected: 2,
                actual: Utils.NumberOfSetBits(unchecked((uint) 3)));

            Assert.AreEqual(
                expected: 10,
                actual: Utils.NumberOfSetBits(unchecked((uint) 7863)));

            Assert.AreEqual(
                expected: 25,
                actual: Utils.NumberOfSetBits(unchecked((uint) -5000)));
        }

        [TestMethod]
        public void TestNumberOfSetBitsLong()
        {
            Assert.AreEqual(
                expected: 64,
                actual: Utils.NumberOfSetBits(unchecked((ulong) -1L)));

            Assert.AreEqual(
                expected: 0,
                actual: Utils.NumberOfSetBits(unchecked((ulong) 0)));

            Assert.AreEqual(
                expected: 3,
                actual: Utils.NumberOfSetBits(unchecked((ulong) 7)));

            Assert.AreEqual(
                expected: 10,
                actual: Utils.NumberOfSetBits(unchecked((ulong) 7863)));

            Assert.AreEqual(
                expected: 57,
                actual: Utils.NumberOfSetBits(unchecked((ulong) -5000)));
        }
    }
}

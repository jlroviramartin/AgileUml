using Microsoft.VisualStudio.TestTools.UnitTesting;
using UmlFromCode.Selectors;

namespace UmlFromCode.Test
{
    [TestClass]
    public class SimpleRegexSelectorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.c").Select("a.b.c"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.c").Select("a.b.e"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.c").Select("a.b"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.c").Select("a.b.c.d"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.c").Select(""));
            Assert.IsTrue(
                new SimpleRegexSelector("").Select(""));
            Assert.IsFalse(
                new SimpleRegexSelector("").Select("a.b.c"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.*").Select("a.b.c"));
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.*").Select("a.b.e"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.*").Select("a.b"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.*").Select("a.f"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.*").Select("a.b.c.d"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.*").Select(""));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.**").Select("a.b.c"));
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.**").Select("a.b.e"));

            Assert.IsTrue(
                new SimpleRegexSelector("a.b.**").Select("a.b.c.d"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.**").Select("a.b"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.**").Select("a.f"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.**").Select(""));
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.x?x").Select("a.b.xcx"));
            Assert.IsTrue(
                new SimpleRegexSelector("a.b.x?x").Select("a.b.xex"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.x?x").Select("a.b.xx"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.x?x").Select("a.b"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.x?x").Select("a.f"));

            Assert.IsFalse(
                new SimpleRegexSelector("a.b.x?x").Select("a.b.c.d"));
            Assert.IsFalse(
                new SimpleRegexSelector("a.b.x?x").Select(""));
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.IsTrue(
                new SimpleRegexSelector("aa.bb.c?.*.**").Select("aa.bb.cc.dd.ee.ff"));
            Assert.IsTrue(
                new SimpleRegexSelector("aa.bb.c?.*.**").Select("aa.bb.cc.dd.ee"));

            Assert.IsFalse(
                new SimpleRegexSelector("aa.bb.c?.*.**").Select("aa.bb.cc.dd"));
            Assert.IsFalse(
                new SimpleRegexSelector("aa.bb.c?.*.**").Select("aa.bb.ac.dd.ee.ff"));
            Assert.IsFalse(
                new SimpleRegexSelector("aa.bb.c?.*.**").Select("aa.bb.ac.dd.ee"));
        }
    }
}

using CodeStatistics.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatistics.Tests{
    [TestClass]
    public class ParseUtilsTests{
        [TestMethod]
        public void TestStringExtract(){
            string res;

            Assert.IsTrue("...do not touch letters".ExtractStart("...",out res));
            Assert.AreEqual("do not touch letters",res);
            Assert.IsFalse("..do not touch letters".ExtractStart("...",out res));

            Assert.IsTrue("do not touch letters...".ExtractEnd("...",out res));
            Assert.AreEqual("do not touch letters",res);
            Assert.IsFalse("do not touch letters..".ExtractEnd("...",out res));

            Assert.IsTrue("...do not touch letters___".ExtractBoth("...","___",out res));
            Assert.AreEqual("do not touch letters",res);
            Assert.IsFalse("___do not touch letters...".ExtractBoth("...","___",out res));
            Assert.IsFalse("...do not touch letters".ExtractBoth("...","___",out res));
            Assert.IsFalse("do not touch letters___".ExtractBoth("...","___",out res));
        }

        [TestMethod]
        public void TestStringRemove(){
            Assert.AreEqual("this is the end","this is the end, or not".RemoveFrom(","));
            Assert.AreEqual("this is the end","this is the end".RemoveFrom(","));
            Assert.AreEqual("this is the end","no way, this is the end".RemoveTo(", "));
            Assert.AreEqual("this is the end","this is the end".RemoveTo(", "));
        }
    }
}

using CodeStatistics.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class ParseUtilsTests{
        [TestMethod]
        public void TestStringExtract(){
            Assert.AreEqual("do not touch letters","...do not touch letters".ExtractStart("..."));
            Assert.AreEqual("..do not touch letters","..do not touch letters".ExtractStart("..."));
            Assert.AreEqual("do not touch letters","do not touch letters...".ExtractEnd("..."));
            Assert.AreEqual("do not touch letters..","do not touch letters..".ExtractEnd("..."));
            Assert.AreEqual("do not touch letters","...do not touch letters___".ExtractBoth("...","___"));
            Assert.AreEqual("do not touch letters__","...do not touch letters__".ExtractBoth("...","___"));
            Assert.AreEqual("..do not touch letters","..do not touch letters___".ExtractBoth("...","___"));
            Assert.AreEqual("..do not touch letters__","..do not touch letters__".ExtractBoth("...","___"));
        }

        [TestMethod]
        public void TestStringExtractOut(){
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

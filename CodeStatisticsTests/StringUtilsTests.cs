using CodeStatistics.Handling.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class StringUtilsTests{
        [TestMethod]
        public void TestCapitalizeFirst(){
            Assert.AreEqual("", "".CapitalizeFirst());
            Assert.AreEqual(" ", " ".CapitalizeFirst());
            Assert.AreEqual("X", "x".CapitalizeFirst());
            Assert.AreEqual("X", "X".CapitalizeFirst());
            Assert.AreEqual("Hello", "hello".CapitalizeFirst());
            Assert.AreEqual("HELLO", "hELLO".CapitalizeFirst());
            Assert.AreEqual("HELLO", "HELLO".CapitalizeFirst());
        }
    }
}

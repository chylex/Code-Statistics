using CodeStatistics.Handling.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class ParseUtilsTests{
        [TestMethod]
        public void TestCharacterCount(){
            Assert.AreEqual("no change".Length, ParseUtils.CountCharacters("no change",4));
            Assert.AreEqual("_converted one tab".Length, ParseUtils.CountCharacters("    converted one tab",4));
            Assert.AreEqual("__converted two tabs".Length, ParseUtils.CountCharacters("        converted two tabs",4));
            Assert.AreEqual("only    convert    leading    spaces    ".Length, ParseUtils.CountCharacters("only    convert    leading    spaces    ",4));

            Assert.AreEqual("__".Length, ParseUtils.CountCharacters("     ",4)); // 5 spaces => 1 tab 1 space
            Assert.AreEqual("___".Length, ParseUtils.CountCharacters("   ",1)); // 3 spaces => 3 tabs
        }
    }
}

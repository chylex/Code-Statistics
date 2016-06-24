using LanguageJava.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests.Languages.Java{
    [TestClass]
    public class JavaParseUtilsTests{
        [TestMethod]
        public void TestFullToSimpleName(){
            Assert.AreEqual("SimpleName",JavaParseUtils.FullToSimpleName("this.is.a.full.name.SimpleName"));
            Assert.AreEqual("SimpleName",JavaParseUtils.FullToSimpleName("SimpleName"));
            Assert.AreEqual(string.Empty,JavaParseUtils.FullToSimpleName("broken.name."));
        }
    }
}

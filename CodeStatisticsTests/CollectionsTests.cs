using CodeStatisticsCore.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class CollectionsTests{
        [TestMethod]
        public void TestCharacterRangeSet(){
            CharacterRangeSet set = new CharacterRangeSet{
                { 1, 10 },
                { 16, 35 },
                { 59, 59 },
                { 61, 63 }
            };

            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(9));
            Assert.IsTrue(set.Contains(10));
            Assert.IsTrue(set.Contains(18));
            Assert.IsTrue(set.Contains(24));
            Assert.IsTrue(set.Contains(59));
            Assert.IsTrue(set.Contains(61));
            Assert.IsTrue(set.Contains(62));

            Assert.IsFalse(set.Contains(0));
            Assert.IsFalse(set.Contains(11));
            Assert.IsFalse(set.Contains(13));
            Assert.IsFalse(set.Contains(39));
            Assert.IsFalse(set.Contains(58));
            Assert.IsFalse(set.Contains(60));
            Assert.IsFalse(set.Contains(100));
        }
    }
}

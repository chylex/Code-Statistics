using CodeStatistics.Handling.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class CodeParserTests{
        [TestMethod]
        public void TestValidSkip(){
            string code = "   \n  x    yz { content { more } abc }";

            // base
            CodeParser parser = new CodeParser(code);
            Assert.AreEqual(' ',parser.Char);
            Assert.IsFalse(parser.IsEOF);

            parser.SkipSpaces();
            Assert.AreEqual('x',parser.Char);

            parser.SkipTo('y');
            Assert.AreEqual('y',parser.Char);

            Assert.AreEqual('z',parser.Next());
            Assert.AreEqual('z',parser.Char);

            // block 1
            CodeParser block1 = parser.SkipBlockGet('{','}');
            Assert.AreEqual(" content { more } abc ",block1.Contents);

            block1.SkipSpaces();
            Assert.AreEqual('c',block1.Char);

            // block 2
            CodeParser block2 = block1.SkipBlockGet('{','}');
            Assert.AreEqual(" more ",block2.Contents);

            // block 1
            Assert.AreEqual(' ',block1.Char);
            Assert.IsFalse(block1.IsEOF);

            // base
            Assert.AreEqual('\0',parser.Char);
            Assert.IsTrue(parser.IsEOF);
        }

        [TestMethod]
        public void TestInvalidSkip(){
            string code = "1  [ { my block } ]  ";

            // base
            CodeParser parser = new CodeParser(code);

            Assert.AreEqual('1',parser.Char);
            Assert.IsFalse(parser.IsEOF);
            parser.SkipTo('(');
            Assert.AreEqual('1',parser.Char);
            Assert.IsFalse(parser.IsEOF);

            // block
            CodeParser block = parser.SkipBlockGet('(',')');
            Assert.AreEqual('\0',block.Char);
            Assert.IsTrue(block.IsEOF);
        }
    }
}

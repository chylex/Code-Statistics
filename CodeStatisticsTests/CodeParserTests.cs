using CodeStatistics.Handling.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class CodeParserTests{
        [TestMethod]
        public void TestClone(){
            CodeParser parser1 = new CodeParser("abc__efg"){
                IsWhiteSpace = chr => chr == '_'
            };

            parser1.SkipTo('_').SkipSpaces();
            Assert.AreEqual('e',parser1.Char);

            // cloned with same code
            CodeParser parser2 = parser1.Clone();
            Assert.AreEqual('a',parser2.Char);

            parser2.SkipTo('_').SkipSpaces();
            Assert.AreEqual('e',parser2.Char);

            // cloned with different code
            CodeParser parser3 = parser1.Clone("x_z");
            Assert.AreEqual('x',parser3.Char);

            parser3.Skip().SkipSpaces();
            Assert.AreEqual('z',parser3.Char);
        }

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
            CodeParser block1 = parser.ReadBlock('{','}');
            CodeParser block1Cloned = block1.Clone();

            Assert.AreEqual(" content { more } abc ",block1.Contents);

            block1.SkipSpaces();
            Assert.AreEqual('c',block1.Char);

            block1Cloned.SkipTo('}').SkipTo('a').Skip();
            Assert.AreEqual('b',block1Cloned.Char);

            // block 2
            CodeParser block2 = block1.ReadBlock('{','}');
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
            CodeParser block = parser.ReadBlock('(',')');
            Assert.AreEqual('\0',block.Char);
            Assert.IsTrue(block.IsEOF);
        }

        [TestMethod]
        public void TestSkipMatch(){
            string code = "this is a basic skip test";

            CodeParser parser1 = new CodeParser(code);
            Assert.IsTrue(parser1.SkipIfMatch("this is a "));
            Assert.AreEqual('b',parser1.Char);

            CodeParser parser2 = new CodeParser(code);
            Assert.IsTrue(parser2.SkipIfMatch("^.his^s"));
            Assert.AreEqual('i',parser2.Char);

            CodeParser parser3 = new CodeParser(code);
            Assert.IsFalse(parser3.SkipIfMatch("abcd"));
            Assert.IsFalse(parser3.SkipIfMatch("this is a basic skop test"));
            Assert.IsFalse(parser3.SkipIfMatch("thi^s"));
            Assert.AreEqual('t',parser3.Char);
        }

        [TestMethod]
        public void TestIdentifier(){
            // simple java annotation
            CodeParser parser1 = new CodeParser("@MyIdentifier");

            Assert.AreEqual(string.Empty,parser1.ReadIdentifier());
            Assert.AreEqual('@',parser1.Char);

            parser1.Next();
            Assert.AreEqual("MyIdentifier",parser1.ReadIdentifier());

            Assert.AreEqual('\0',parser1.Char);
            Assert.IsTrue(parser1.IsEOF);

            // complex java annotation
            CodeParser parser2 = new CodeParser("@MyIdentifier(var = true)"){
                IsValidIdentifier = str => str != "true"
            };

            Assert.AreEqual(string.Empty,parser2.ReadIdentifier());
            Assert.AreEqual('@',parser2.Char);

            parser2.Next();
            Assert.AreEqual("MyIdentifier",parser2.ReadIdentifier());

            // complex java annotation - variables
            CodeParser parser2Vars = parser2.ReadBlock('(',')');

            Assert.AreEqual("var",parser2Vars.ReadIdentifier());

            parser2Vars.SkipTo('=').Skip().SkipSpaces();
            Assert.AreEqual('t',parser2Vars.Char);
            Assert.AreEqual(string.Empty,parser2Vars.ReadIdentifier());
        }
    }
}

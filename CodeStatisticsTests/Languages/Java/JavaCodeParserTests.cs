using CodeStatistics.Handling.Languages.Java;
using CodeStatistics.Handling.Languages.Java.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests.Languages.Java{
    [TestClass]
    public class JavaCodeParserTests{
        [TestMethod]
        public void TestAnnotations(){
            Annotation? annotation1 = new JavaCodeParser("@SimpleAnnotation").ReadAnnotation();
            Assert.IsTrue(annotation1.HasValue);
            Assert.AreEqual("SimpleAnnotation",annotation1.Value.SimpleName);

            Annotation? annotation2 = new JavaCodeParser("@ AnnotationWithSpace ").ReadAnnotation();
            Assert.IsTrue(annotation2.HasValue);
            Assert.AreEqual("AnnotationWithSpace",annotation2.Value.SimpleName);

            Annotation? annotation3 = new JavaCodeParser("@full.name.Annotation").ReadAnnotation();
            Assert.IsTrue(annotation3.HasValue);
            Assert.AreEqual("Annotation",annotation3.Value.SimpleName);

            Annotation? annotation4 = new JavaCodeParser("@AnnotationWithArguments(var = true, other = 15)").ReadAnnotation();
            Assert.IsTrue(annotation4.HasValue);
            Assert.AreEqual("AnnotationWithArguments",annotation4.Value.SimpleName);

            Annotation? annotation5 = new JavaCodeParser("@  all.Combined (var = '') ").ReadAnnotation();
            Assert.IsTrue(annotation5.HasValue);
            Assert.AreEqual("Combined",annotation5.Value.SimpleName);

            Annotation? annotation6 = new JavaCodeParser("Invalid").ReadAnnotation();
            Assert.IsFalse(annotation6.HasValue);

            Annotation? annotation7 = new JavaCodeParser("@").ReadAnnotation();
            Assert.IsFalse(annotation7.HasValue);

            Annotation? annotation8 = new JavaCodeParser("@123").ReadAnnotation();
            Assert.IsFalse(annotation8.HasValue);
        }

        [TestMethod]
        public void TestBalanceSkip(){
            JavaCodeParser parser1 = new JavaCodeParser("simple;D");
            parser1.SkipToIfBalanced(';');
            Assert.AreEqual(';',parser1.Char);
            Assert.AreEqual('D',parser1.Next());

            JavaCodeParser parser2 = new JavaCodeParser("find ( ;semicolon {[ ( ; ) ; ]} ; keep looking ) ;-)");
            parser2.SkipToIfBalanced(';');
            Assert.AreEqual(';',parser2.Char);
            Assert.AreEqual('-',parser2.Next());
            Assert.AreEqual(')',parser2.Next());

            JavaCodeParser parser3 = new JavaCodeParser("nope");
            parser3.SkipToIfBalanced(';');
            Assert.AreEqual('n',parser3.Char);

            JavaCodeParser parser4 = new JavaCodeParser("cannot find (;;;)");
            parser4.SkipToIfBalanced(';');
            Assert.AreEqual('c',parser4.Char);

            JavaCodeParser parser5 = new JavaCodeParser("( invalid [ balance ) ] ;");
            parser5.SkipToIfBalanced(';');
            Assert.AreEqual('(',parser5.Char);
        }
    }
}

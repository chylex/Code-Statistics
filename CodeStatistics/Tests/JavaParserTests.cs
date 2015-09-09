using CodeStatistics.Handlers.Objects.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodeStatistics.Tests{
    [TestClass]
    public class JavaParserTests{
        [TestMethod]
        public void TestJavaIdentifiers(){
            Assert.IsTrue(JavaParseUtils.IsJavaIdentifier("ClassName"),"ClassName is supposed to be valid");
            Assert.IsTrue(JavaParseUtils.IsJavaIdentifier("ClassName123"),"ClassName123 is supposed to be valid");
            Assert.IsTrue(JavaParseUtils.IsJavaIdentifier("myMethod"),"myMethod is supposed to be valid");
            Assert.IsTrue(JavaParseUtils.IsJavaIdentifier("my_method$"),"my_method$ is supposed to be valid");

            Assert.IsFalse(JavaParseUtils.IsJavaIdentifier("0ClassName123"),"0ClassName123 is supposed to be invalid");
            Assert.IsFalse(JavaParseUtils.IsJavaIdentifier("Class Name"),"Class Name is supposed to be invalid");
            Assert.IsFalse(JavaParseUtils.IsJavaIdentifier("Class.Name"),"Class.Name is supposed to be invalid");
        }

        [TestMethod]
        public void TestGetType(){
            KeyValuePair<JavaType,string> kvp;
            
            kvp = JavaParseUtils.GetType("public class MyClassName implements Things");
            Assert.AreEqual(JavaType.Class,kvp.Key);
            Assert.AreEqual("MyClassName",kvp.Value);

            kvp = JavaParseUtils.GetType("interface MyInterface extends Stuff");
            Assert.AreEqual(JavaType.Interface,kvp.Key);
            Assert.AreEqual("MyInterface",kvp.Value);

            kvp = JavaParseUtils.GetType("private enum Enum{");
            Assert.AreEqual(JavaType.Enum,kvp.Key);
            Assert.AreEqual("Enum",kvp.Value);

            kvp = JavaParseUtils.GetType("public @interface MyAnnotation {");
            Assert.AreEqual(JavaType.Annotation,kvp.Key);
            Assert.AreEqual("MyAnnotation",kvp.Value);
        }
    }
}

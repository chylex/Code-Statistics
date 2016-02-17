﻿using System.Collections.Generic;
using CodeStatistics.Handlers.Objects.Java;
using CodeStatistics.Handlers.Objects.Java.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class JavaParserTests{
        [TestMethod]
        public void TestRegex(){
            Assert.AreEqual(@"test string  and then ",JavaParseUtils.Strings.Replace(@"test string ""remove this"" and then 'this'",""));
            Assert.AreEqual(@"...",JavaParseUtils.Strings.Replace(@"""remove"".""all"".""of"".""these""",""));
            Assert.AreEqual("a\nb",JavaParseUtils.Strings.Replace("a\"remove\"\nb\"multiline\"",""));
            Assert.AreEqual(@"do not change ""this fake' string",JavaParseUtils.Strings.Replace(@"do not change ""this fake' string",""));

            Assert.AreEqual("this is not a comment",JavaParseUtils.CommentOneLine.Replace("this is not a comment// and this is",""));
            Assert.AreEqual("line one \nline two ",JavaParseUtils.CommentOneLine.Replace("line one // comment\nline two // comment",""));

            Assert.AreEqual("hello ",JavaParseUtils.CommentMultiLine.Replace("hello /* world */",""));
            Assert.AreEqual("hello--world",JavaParseUtils.CommentMultiLine.Replace("hello-/*\nline 1\nline 2*/-world",""));
        }

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

            kvp = JavaParseUtils.GetType("class.invalid");
            Assert.AreEqual(JavaType.Invalid,kvp.Key);
            Assert.AreEqual(null,kvp.Value);

            kvp = JavaParseUtils.GetType("fail");
            Assert.AreEqual(JavaType.Invalid,kvp.Key);
            Assert.AreEqual(null,kvp.Value);
        }
    }
}
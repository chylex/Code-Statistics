using CodeStatistics.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeStatisticsTests{
    [TestClass]
    public class IOUtilsTests{
        [TestMethod]
        public void TestFindRootPath(){
            Assert.AreEqual(@"C:\Projects", IOUtils.FindRootPath(new []{ @"C:\Projects" }));
            Assert.AreEqual(@"C:\Projects\file.html", IOUtils.FindRootPath(new []{ @"C:\Projects\file.html" }));
            Assert.AreEqual(@"C:\Projects\file.html", IOUtils.FindRootPath(new []{ @"C:\Projects\file.html", @"C:\Projects\file.html" }));

            Assert.AreEqual(@"C:\Projects", IOUtils.FindRootPath(new []{ @"C:\Projects\MyProject", @"C:\Projects\OtherProject" }));
            Assert.AreEqual(@"C:\Projects", IOUtils.FindRootPath(new []{ @"C:\Projects\MyProject\Build", @"C:\Projects\MyProject\Code", @"C:\Projects\OtherProject\Code" }));
            Assert.AreEqual(@"C:\Projects\MyProject", IOUtils.FindRootPath(new []{ @"C:\Projects\MyProject", @"C:\Projects\MyProject\Build", @"C:\Projects\MyProject\Code" }));

            Assert.AreEqual(@"C:\Projects", IOUtils.FindRootPath(new []{ @"C:\Projects\", @"C:\Projects\" }));
            Assert.AreEqual(@"C:\Projects", IOUtils.FindRootPath(new []{ @"C:\\\Projects\", @"C:\\Projects\\" }));
            Assert.AreEqual(@"C:\", IOUtils.FindRootPath(new []{ @"C:\", @"C:\" }));
            Assert.AreEqual(@"C:\", IOUtils.FindRootPath(new []{ @"C:\Abc", @"C:\Def" }));
            Assert.AreEqual(@"C:\", IOUtils.FindRootPath(new []{ @"C:", @"C:" }));
            Assert.AreEqual(@"Test", IOUtils.FindRootPath(new []{ @"Test", @"Test" })); // no trailing slash

            Assert.AreEqual(@"", IOUtils.FindRootPath(new []{ @"C:\Folder", @"D:\Folder", @"E:\Folder" }));
            Assert.AreEqual(@"", IOUtils.FindRootPath(new []{ @"Invalid 1", @"Invalid 2" }));
        }
    }
}

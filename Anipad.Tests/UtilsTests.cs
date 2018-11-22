using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anipad.Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void ExtractFilesToOpenFromCommandLineArgs_NoArgs_ArrWithNull()
        {
            string[] args = new string[] {"anipad.exe"};

            string[] fileNames = Utils.ExtractFilesToOpenFromCommandLineArgs(args);

            Assert.AreEqual(fileNames.Length, 1);
            Assert.IsNull(fileNames[0]);
        }

        [TestMethod]
        public void ExtractFilesToOpenFromCommandLineArgs_SingleArg_SameArg()
        {
            string file = "file.txt";
            string[] args = new string[] { "anipad.exe", file };

            string[] fileNames = Utils.ExtractFilesToOpenFromCommandLineArgs(args);

            Assert.AreEqual(fileNames.Length, 1);
            Assert.AreEqual(fileNames[0], file);
        }

        [TestMethod]
        public void ExtractFilesToOpenFromCommandLineArgs_MultipleArgs_SameArgs()
        {
            string file1 = "file1.txt";
            string file2 = "file2.txt";
            string file3 = "file3.txt";
            string[] args = new string[] { "anipad.exe", file1, file2, file3 };

            string[] fileNames = Utils.ExtractFilesToOpenFromCommandLineArgs(args);

            Assert.AreEqual(fileNames.Length, 3);
            Assert.AreEqual(fileNames[0], file1);
            Assert.AreEqual(fileNames[1], file2);
            Assert.AreEqual(fileNames[2], file3);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anipad.Tests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void BuildTitle_withChanges_withStar()
        {
            string currentFilename = "test";
            bool anyChangeMade = true;

            string result = Helpers.BuildTitle(currentFilename, anyChangeMade);

            Assert.AreEqual(result, $"*{currentFilename} - {Constants.AppName}");
        }

        [TestMethod]
        public void BuildTitle_withoutChanges_withoutStar()
        {
            string currentFilename = "test";
            bool anyChangeMade = false;

            string result = Helpers.BuildTitle(currentFilename, anyChangeMade);

            Assert.AreEqual(result, $"{currentFilename} - {Constants.AppName}");
        }
    }
}

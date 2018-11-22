using System;
using Anipad.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anipad.Tests.ModelsTests
{
    [TestClass]
    public class BackgroundImageManagerTests
    {
        [TestMethod]
        public void AfterConstructor_CurrentIsDefault()
        {
            var backgroundImageManager = new BackgroundImageManager();

            Assert.AreEqual(backgroundImageManager.Current.Title, BackgroundImage.Default.Title);
            Assert.AreEqual(backgroundImageManager.Current.Filename, BackgroundImage.Default.Filename);
        }

        [TestMethod]
        public void Set_ChangesCurrent()
        {
            var backgroundImageManager = new BackgroundImageManager();
            var backgroundImage = new BackgroundImage("title", "filename");

            backgroundImageManager.Set(backgroundImage);

            Assert.AreEqual(backgroundImageManager.Current, backgroundImage);
        }

        [TestMethod]
        public void Reset_CurrentIsDefault()
        {
            var backgroundImageManager = new BackgroundImageManager();
            var backgroundImage = new BackgroundImage("title", "filename");

            backgroundImageManager.Set(backgroundImage);
            backgroundImageManager.Reset();

            Assert.AreEqual(backgroundImageManager.Current.Title, BackgroundImage.Default.Title);
            Assert.AreEqual(backgroundImageManager.Current.Filename, BackgroundImage.Default.Filename);
        }

    }
}

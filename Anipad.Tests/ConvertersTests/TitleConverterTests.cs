using System;
using Anipad.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Anipad.Tests.ConvertersTests
{
    [TestClass]
    public class TitleConverterTests
    {
        [TestMethod]
        public void ConvertBack_Always_ReturnsNull()
        {
            var titleConverter = new TitleConverter();
            string param1 = "abc";
            for (int i = 0; i < 2; ++i)
            {
                bool param2 = (i != 0);
                string value = Helpers.BuildTitle(param1, param2);
                object[] result = titleConverter.ConvertBack(value, new Type[]{typeof(string), typeof(bool)}, null, null);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void Convert_TakeFirst2Params_CallsHelpersBuildTitle()
        {
            var titleConverter = new TitleConverter();

            string param1 = "abc";
            for (int i = 0; i < 2; ++i)
            {
                bool param2 = (i != 0);
                string result = (string)titleConverter.Convert(new object[] {param1, param2}, typeof(string), null, null);
                Assert.AreEqual(result, Helpers.BuildTitle(param1, param2));
            }

        }
        
    }
}

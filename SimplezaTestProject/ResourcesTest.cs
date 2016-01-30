using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpleza.Resources;

namespace SimplezaTestProject
{
    [TestClass]
    public class ResourcesTest
    {
        [TestMethod]
        public void LocalizedStringsNotNull()
        {
            LocalizedStrings locStrings = new LocalizedStrings();


            Assert.IsNotNull(locStrings.Resource1);
            Assert.IsNotNull(locStrings);
        }

       
    }
}

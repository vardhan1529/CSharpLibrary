using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CSharpLibrary.MockRepo;

namespace CSharpLibrary.Tests
{
    [TestClass]
    public class CountriesTest
    {
        private Mock<ICountryRepo> countryMock;

        public CountriesTest()
        {
            countryMock = new Mock<ICountryRepo>();
        }

        [TestMethod]
        public void VerifyGetCountryNamesCount()
        {
            Program p = new Program(countryMock.Object);
            countryMock.Setup(m => m.GetCountryNames()).Returns(new System.Collections.Generic.List<string>() { "Test", "TEst1" });
            var countries = p.GetCountries();
            Assert.IsNotNull(countries);
            Assert.AreEqual(countries.Count, 2);
        }

        [TestMethod]
        public void VerifyGetCountryNamesNoData()
        {
            Program p = new Program(countryMock.Object);
            countryMock.Setup(m => m.GetCountryNames()).Returns(new System.Collections.Generic.List<string>());
            var countries = p.GetCountries();
            Assert.IsNotNull(countries);
            Assert.AreEqual(countries.Count, 0);
        }

        [TestMethod]
        public void VerifyGetCountryNamesConvertedToLower()
        {
            Program p = new Program(countryMock.Object);
            countryMock.Setup(m => m.GetCountryNames()).Returns(new System.Collections.Generic.List<string>() { "Test", "TEst1" });
            var countries = p.GetCountries();
            Assert.IsNotNull(countries);
            Assert.AreEqual(countries.Count, 2);
            Assert.AreEqual(countries[0], "test");
            Assert.AreEqual(countries[1], "test1");
        }
    }
}

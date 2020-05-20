using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Cas31.PageObjects;

namespace Cas31
{
    class TestClass
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestGoogleSearch()
        {
            HomePage naslovna = new HomePage(driver);
            ResultsPage rezultati;

            naslovna.GoToPage();
            rezultati = naslovna.SearchFor("C# Selenium PageObject Model");

            Assert.Greater(rezultati.NumberOfResults, 0);
        }

        [Test]
        public void TestClickOnPrivacy()
        {
            HomePage naslovna = new HomePage(driver);
            naslovna.GoToPage();
            naslovna.ClickOnPrivacy();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}

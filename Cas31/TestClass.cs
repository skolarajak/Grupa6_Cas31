using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Cas31.PageObjects;
using QaHomePage = Cas31.PageObjects.Qa.Rs.HomePage;
using QaListPage = Cas31.PageObjects.Qa.Rs.ListPage;

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

        [Test]
        [Category("qa.rs")]
        public void TestQaListFemale()
        {
            QaListPage lista;
            QaHomePage naslovna = new QaHomePage(driver);
            naslovna.GoToPage();
            lista = naslovna.ClickOnListLink();
            Assert.GreaterOrEqual(lista.FemaleUsers, 40);
        }

        [Test]
        [Category("qa.rs")]
        public void TestQaListMale()
        {
            QaListPage lista;
            QaHomePage naslovna = new QaHomePage(driver);
            naslovna.GoToPage();
            lista = naslovna.ClickOnListLink();
            Assert.GreaterOrEqual(lista.MaleUsers, 40);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}

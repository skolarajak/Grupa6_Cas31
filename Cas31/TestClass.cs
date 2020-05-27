using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Cas31.PageObjects;
using QaHomePage = Cas31.PageObjects.Qa.Rs.HomePage;
using QaListPage = Cas31.PageObjects.Qa.Rs.ListPage;
using QaRegisterPage = Cas31.PageObjects.Qa.Rs.RegisterPage;
using ShopQaHomePage = Cas31.PageObjects.Shop.Qa.Rs.HomePage;
using ShopQaLoginPage = Cas31.PageObjects.Shop.Qa.Rs.LoginPage;
using Excel = Microsoft.Office.Interop.Excel;
using Cas31.Libraries;

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

        [Test]
        [Category("qa.rs")]
        public void TestQaRegister()
        {
            QaRegisterPage registracija;
            QaHomePage naslovna = new QaHomePage(driver);
            naslovna.GoToPage();
            registracija = naslovna.ClickOnRegisterLink();
            registracija.FillFirstName("Jason");
            registracija.LastName.SendKeys("Woorheese");
            registracija.UserName.SendKeys("jasonw");
            registracija.Email.SendKeys("jason@woorheese.oorg");
            registracija.Phone.SendKeys("00381123456789");
            registracija.Country.SelectByText("Serbia");
            registracija.City.SelectByText("Novi Sad");
            registracija.Address.SendKeys("Main st. 180");
            registracija.GenderM.Click();
            registracija.Newsletter.Click();
            registracija.Promotions.Click();
            registracija.RegisterButton.Click();
        }

        [Test]
        [Category("shop.qa.rs")]
        public void TestShopQaRsLogin()
        {
            CSVHandler CSV = new CSVHandler();
            Excel.Worksheet Sheet = CSV.OpenCSV(@"D:\Kurs\kurs-ddt.csv");

            int rows = Sheet.UsedRange.Rows.Count;
            int columns = Sheet.UsedRange.Columns.Count;
            TestContext.WriteLine("Rows: {0}, Columns: {1}", rows, columns);

            string name;
            string username;
            string password;
            string expected;
            string description;
            bool hasFailedExpected = false;

            for (int i = 2; i <= rows; i++)
            {
                name = Sheet.Cells[i, 1].Value;
                username = Sheet.Cells[i, 2].Value;
                password = Sheet.Cells[i, 3].Value;
                expected = Sheet.Cells[i, 4].Value;
                description = Sheet.Cells[i, 5].Value;
                TestContext.Write("{0}: ", name);

                ShopQaHomePage home = new ShopQaHomePage(driver);
                home.GoToPage();
                ShopQaLoginPage login = home.ClickOnLoginLink();
                home = login.Login(username, password);

                if (home.WelcomeBack != null) // Successful login
                {
                    if (expected == "pass")
                    {
                        TestContext.Write(" PASS ");
                    }  else
                    {
                        TestContext.Write(" FAIL ");
                        hasFailedExpected = true;
                    }
                }
                else // Unsuccessful login
                {
                    if (expected == "fail")
                    {
                        TestContext.Write(" PASS ");
                    }
                    else
                    {
                        TestContext.Write(" FAIL ");
                        hasFailedExpected = true;
                    }
                }

                TestContext.WriteLine(" ({0})", description);

                IWebElement logout = home.LinkLogout;
                logout?.Click();

            }

            CSV.Close();

            if (hasFailedExpected)
            {
                Assert.Fail("Some tests have unmet expected results.");
            } else
            {
                Assert.Pass();
            }

        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Close();
            }
        }
    }
}

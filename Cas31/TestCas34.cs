using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Cas31.PageObjects;
using ShopQaHomePage = Cas31.PageObjects.Shop.Qa.Rs.HomePage;
using ShopQaLoginPage = Cas31.PageObjects.Shop.Qa.Rs.LoginPage;
using ShopQaCartPage = Cas31.PageObjects.Shop.Qa.Rs.CartPage;
using ShopQaConfirmationPage = Cas31.PageObjects.Shop.Qa.Rs.ConfirmationPage;
using Excel = Microsoft.Office.Interop.Excel;
using Cas31.Libraries;

namespace Cas31
{
    class TestCas34
    {
        private IWebDriver driver;
        private CSVHandler CSV;

        [Test]
        [Category("Cas34")]
        public void TestShopQaRsOrderPro()
        {
            Excel.Worksheet Sheet;
            Sheet = this.CSV.OpenCSV(@"D:\Kurs\login.csv");
            int rows = Sheet.UsedRange.Rows.Count;
            int columns = Sheet.UsedRange.Columns.Count;

            if ((rows <= 1) || (columns < 2))
            {
                this.LogLine("FAIL - Not enough data to proceed with login");
                Assert.Fail("Not enough data to proceed with login");
            }
            string username = Sheet.Cells[2, 1].Value;
            string password = Sheet.Cells[2, 2].Value;
            this.CSV.Close();

            ShopQaHomePage home = new ShopQaHomePage(driver);
            home.GoToPage();
            ShopQaLoginPage login = home.ClickOnLoginLink();
            home = login.Login(username, password);
            if (home.WelcomeBack != null) // Successful login
            {
                Sheet = this.CSV.OpenCSV(@"D:\Kurs\test-pro-order.csv");
                rows = Sheet.UsedRange.Rows.Count;
                columns = Sheet.UsedRange.Columns.Count;

                if ((rows <= 1) || (columns < 3))
                {
                    this.LogLine("FAIL - Not enough data to proceed with tests");
                    Assert.Fail("Not enough data to proceed with tests");
                }

                ShopQaCartPage cart;
                ShopQaConfirmationPage confirmation;
                bool hasFail = false;
                string name, quantity, shipping;
                for (int i = 2; i <= rows; i++)
                {
                    name = Sheet.Cells[i, 1].Value;
                    quantity = Convert.ToString(Sheet.Cells[i, 2].Value);
                    shipping = Convert.ToString(Sheet.Cells[i, 3].Value);
                    if (shipping != "FREE")
                    {
                        shipping = $"${shipping}";
                    }

                    home.SelectQuantity(home.PackageProQuantity, quantity);
                    cart = home.ClickOnOrderPro();
                    string result = cart.ShippingColumn.Text;
                    string passFail = "PASS";
                    if (result != shipping)
                    {
                        hasFail = true;
                        passFail = "FAIL";
                    }

                    this.LogLine($"Test [{name}] - {passFail} - Conditions: Quantity={quantity}, Shipping={shipping}, ShippingWas={result}");
                    home = cart.ClickContinueShopping();
                }

                this.CSV.Close();
                this.CSV = null;

                // Let's clean the cart for next run, checkout ordered items
                cart = home.ClickOnViewCart();
                confirmation = cart.ClickCheckout();
                home = confirmation.ClickGoBack();

                if (hasFail)
                {
                    string message = "Test failed on one of the runs, check logs for more details.";
                    this.LogLine(message);
                    Assert.Fail(message);
                } else
                {
                    Assert.Pass();
                }
                
            }
            else
            {
                this.LogLine("FAIL - Failed to login");
                Assert.Fail("Failed to login");
            }
        }

        [SetUp]
        public void SetUp()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Window.Maximize();
            this.CSV = new CSVHandler();
        }

        [TearDown]
        public void TearDown()
        {
            if (this.driver != null)
            {
                this.driver.Close();
            }
            if (this.CSV != null)
            {
                this.CSV.Close();
            }
        }

        private void LogLine(string Message)
        {
            FileManagement.WriteLine(Message);
            TestContext.WriteLine(Message);
        }

        private void Log(string Message)
        {
            FileManagement.Write(Message);
            TestContext.Write(Message);
        }
    }
}

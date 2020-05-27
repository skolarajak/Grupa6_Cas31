using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Cas31.PageObjects.Shop.Qa.Rs
{
    class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public IWebElement UsernameInput
        {
            get
            {
                IWebElement element = null;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.Name("username")));
                    element = this.driver.FindElement(By.Name("username"));
                }
                catch (Exception)
                {
                }
                return element;
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                IWebElement element = null;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.Name("password")));
                    element = this.driver.FindElement(By.Name("password"));
                }
                catch (Exception)
                {
                }
                return element;
            }
        }

        public IWebElement LoginButton
        {
            get
            {
                IWebElement element = null;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.Name("login")));
                    element = this.driver.FindElement(By.Name("login"));
                }
                catch (Exception)
                {
                }
                return element;
            }
        }

        public HomePage Login(string username, string password)
        {
            UsernameInput.SendKeys(username);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
            //wait.Until(EC.ElementIsVisible(By.XPath("//h2[contains(text(), 'Welcome back,')]")));
            System.Threading.Thread.Sleep(2000);
            return new HomePage(this.driver);
        }

    }
}

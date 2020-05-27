using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Cas31.PageObjects.Shop.Qa.Rs
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void GoToPage()
        {
            this.driver.Navigate().GoToUrl("http://shop.qa.rs/");
        }

        public IWebElement LinkLogin
        {
            get
            {
                IWebElement element = null;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.XPath("//a[@href='/login']")));
                    element = this.driver.FindElement(By.XPath("//a[@href='/login']"));
                } catch (Exception)
                {
                }
                return element;
            }
        }

        public IWebElement WelcomeBack
        {
            get
            {
                IWebElement element = null;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.XPath("//h2[contains(text(), 'Welcome back,')]")));
                    element = this.driver.FindElement(By.XPath("//h2[contains(text(), 'Welcome back,')]"));
                } catch (Exception)
                {
                }
                return element;
            }
        }


        public LoginPage ClickOnLoginLink()
        {
            this.LinkLogin?.Click();
            wait.Until(EC.ElementIsVisible(By.ClassName("form-signin-heading")));
            return new LoginPage(this.driver);
        }

    }
}

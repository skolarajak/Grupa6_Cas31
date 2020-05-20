using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Cas31.PageObjects
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
            this.driver.Navigate().GoToUrl("https://www.google.com/");
        }

        public IWebElement SearchField
        {
            get
            {
                IWebElement element;
                try
                {
                    element = this.driver.FindElement(By.Name("q"));
                } catch (Exception)
                {
                    element = null;
                }

                return element;
            }
        }

        public ResultsPage SearchFor(string search)
        {
            this.SearchField?.SendKeys(search);
            this.SearchField?.Submit();
            wait.Until(EC.ElementIsVisible(By.Id("result-stats")));
            return new ResultsPage(this.driver);
        }

        public IWebElement PrivacyLink
        {
            get
            {
                IWebElement element;
                try
                {
                    element = this.driver.FindElement(By.XPath("//a[contains(@href, 'https://policies.google.com/privacy')]"));
                }
                catch (Exception)
                {
                    element = null;
                }

                return element;
            }
        }

        public void ClickOnPrivacy()
        {
            this.PrivacyLink?.Click();
        }


    }
}

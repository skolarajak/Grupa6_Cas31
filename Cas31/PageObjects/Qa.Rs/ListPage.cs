using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cas31.PageObjects.Qa.Rs
{
    class ListPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ListPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public UInt64 FemaleUsers
        {
            get
            {
                ReadOnlyCollection<IWebElement> rows = null;
                try
                {
                    rows = this.driver.FindElements(By.XPath("//td[@class='gender'][text()='Z']"));
                } catch (Exception)
                {

                }
                return Convert.ToUInt64(rows.Count);
            }
        }

        public UInt64 MaleUsers
        {
            get
            {
                ReadOnlyCollection<IWebElement> rows = null;
                try
                {
                    rows = this.driver.FindElements(By.XPath("//td[@class='gender'][text()='M']"));
                }
                catch (Exception)
                {

                }
                return Convert.ToUInt64(rows.Count);
            }
        }

    }
}

using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Cas31.PageObjects
{
    class ResultsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public UInt64 NumberOfResults
        {
            get
            {
                UInt64 num = 0;
                try
                {
                    IWebElement results = this.driver.FindElement(By.Id("result-stats"));
                    string numResults = Regex.Replace(results.Text, "[^0-9]", "");
                    num = Convert.ToUInt64(numResults);
                } catch(Exception)
                {

                }
                return num;
            }
        }
    }
}

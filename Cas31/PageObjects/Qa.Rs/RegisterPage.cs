using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace Cas31.PageObjects.Qa.Rs
{
    class RegisterPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        private IWebElement GetElement(By by)
        {
            IWebElement element;
            try
            {
                element = this.driver.FindElement(by);
            }
            catch (Exception)
            {
                element = null;
            }
            return element;
        }

        private SelectElement GetSelect(By by)
        {
            IWebElement element;
            SelectElement select;
            try
            {
                wait.Until(EC.ElementIsVisible(by));
                element = this.driver.FindElement(by);
                select = new SelectElement(element);
            }
            catch (Exception)
            {
                select = null;
            }
            return select;
        }

        public IWebElement FirstName
        {
            get
            {
                return this.GetElement(By.Name("ime"));
            }
        }

        public IWebElement LastName
        {
            get
            {
                return this.GetElement(By.Name("prezime"));
            }
        }

        public IWebElement UserName
        {
            get
            {
                return this.GetElement(By.Name("korisnicko"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return this.GetElement(By.Name("email"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return this.GetElement(By.Name("telefon"));
            }
        }

        public SelectElement Country
        {
            get
            {
                return this.GetSelect(By.Name("zemlja"));
            }
        }

        public SelectElement City
        {
            get
            {
                return this.GetSelect(By.Name("grad"));
            }
        }

        public IWebElement Address
        {
            get
            {
                return this.GetElement(By.XPath("//div[@id='address']//input"));
            }
        }

        public IWebElement GenderM
        {
            get
            {
                return this.GetElement(By.Id("pol_m"));
            }
        }

        public IWebElement GenderF
        {
            get
            {
                return this.GetElement(By.Id("pol_z"));
            }
        }

        public IWebElement Newsletter
        {
            get
            {
                return this.GetElement(By.Name("obavestenja"));
            }
        }

        public IWebElement Promotions
        {
            get
            {
                return this.GetElement(By.Name("promocije"));
            }
        }

        public IWebElement RegisterButton
        {
            get
            {
                return this.GetElement(By.Name("register"));
            }
        }

        public void FillFirstName(string text)
        {
            this.FirstName.SendKeys(text);
        }


    }
}

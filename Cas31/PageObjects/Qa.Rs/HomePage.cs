﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Cas31.PageObjects.Qa.Rs
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
            this.driver.Navigate().GoToUrl("http://test.qa.rs/");
        }

        public IWebElement LinkListUsers
        {
            get
            {
                IWebElement element;
                try
                {
                    element = this.driver.FindElement(By.PartialLinkText("Izlistaj"));
                }
                catch (Exception)
                {
                    element = null;
                }

                return element;
            }
        }

        public IWebElement LinkRegister
        {
            get
            {
                IWebElement element;
                try
                {
                    wait.Until(EC.ElementIsVisible(By.XPath("//div[@id='registerLinkPlaceholder']/a")));
                    element = this.driver.FindElement(By.XPath("//div[@id='registerLinkPlaceholder']/a"));
                } catch (Exception)
                {
                    element = null;
                }
                return element;
            }
        }

        public ListPage ClickOnListLink()
        {
            this.LinkListUsers?.Click();
            wait.Until(EC.ElementIsVisible(By.TagName("table")));
            return new ListPage(this.driver);
        }

        public RegisterPage ClickOnRegisterLink()
        {
            this.LinkRegister?.Click();
            wait.Until(EC.ElementIsVisible(By.Name("ime")));
            return new RegisterPage(this.driver);
        }
    }
}

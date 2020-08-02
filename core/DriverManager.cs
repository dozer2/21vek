using OpenQA.Selenium;
using Selenium.pages;
using Selenium.pages.elements;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;

namespace Selenium.core
{
    public class DriverManager
    {
        protected IWebDriver WebDriver;
        public static DriverManager instance;

        public static DriverManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DriverManager();
            }

            return instance;
        }
        private DriverManager()
        {
            var dirName = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin", StringComparison.Ordinal));
            var fileInfo = new System.IO.FileInfo(dirName);
            var parentDirName = fileInfo?.FullName;
            WebDriver = new OpenQA.Selenium.Chrome.ChromeDriver(parentDirName + @"libs");
            WebDriver.Manage().Window.Maximize();
        }

        public IWebDriver getWebDriver() {
            return WebDriver;
        }
        public DriverManager initPage<T>( T page) where T: MainPage
        {
            PageFactory.InitElements(WebDriver, page, new CustomFieldDecorator());
            return this;
        }

        public DriverManager initElement<T>(T element) where T : Element
        {
            PageFactory.InitElements(WebDriver, element, new CustomFieldDecorator());
            return this;
        }
        private OpenQA.Selenium.Support.UI.DefaultWait<IWebDriver> checkUntil( int wait)
        {
            OpenQA.Selenium.Support.UI.DefaultWait<IWebDriver> fluentWait = new OpenQA.Selenium.Support.UI.DefaultWait<IWebDriver>(WebDriver);
            fluentWait.Timeout = TimeSpan.FromSeconds(wait);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
           return fluentWait;
            
        }

        public DriverManager waitLoadPage() {
            checkUntil(300).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
            return this;
        }

        public void waitExist(Element element)
        {
            checkUntil(30).Until(d => element.isExist());
        }
         
    }

}

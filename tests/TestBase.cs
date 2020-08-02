using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Selenium.core
{
    [SetUpFixture]
    public abstract class TestBase
    {

        private IWebDriver webDriver = DriverManager.GetInstance().getWebDriver();


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            webDriver.Quit();
        }
    }
}
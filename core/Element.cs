using OpenQA.Selenium;
using Selenium.core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace Selenium.pages.elements
{
   public class Element
    {
        protected IWebElement webElement;

        DriverManager driverManager = DriverManager.GetInstance();

        public Element(IWebElement webElement) {
            this.webElement = webElement;
        }

        public string TagName => webElement.TagName;

        public string Text => webElement.Text;

        public bool Enabled => webElement.Enabled;

        public bool Selected => webElement.Selected;

        public Point Location => webElement.Location;

        public Size Size => webElement.Size;

        public bool Displayed => webElement.Displayed;

        public void Clear()
        {
            webElement.Clear();
        }

        public void Click()
        {
            ScrollIntoTopView();
            webElement.Click();
        }

        public IWebElement FindElement(By by)
        {
            return webElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return webElement.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return webElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return webElement.GetCssValue(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return webElement.GetProperty(propertyName);
        }

        public IWebElement getWebElement()
        {
            return webElement;
        }

        public void SendKeys(string text)
        {
            webElement.SendKeys(text);
        }

        public void Submit()
        {
            webElement.Submit();
        }

        public void ScrollIntoTopView() {
            ((IJavaScriptExecutor)driverManager.getWebDriver()).ExecuteScript("arguments[0].scrollIntoView(false);", this.getWebElement());
            ((IJavaScriptExecutor)driverManager.getWebDriver()).ExecuteScript("window.scroll(" + this.getWebElement().Location.X + "," + (this.getWebElement().Location.Y - 50) + ");", this.getWebElement());
        }

        public Boolean isExist() {
            try
            {
               return getWebElement().Displayed;
            }
            catch (NoSuchElementException )
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            
        }
    }

}

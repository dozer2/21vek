using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.elements
{
    public class Search : Element
    {

        [FindsBy(How = How.Id, Using = @"j-search")]
        public Element input;
        [FindsBy(How = How.ClassName, Using = @"search__submit")]
        public Element button;

        public Search(IWebElement webElement) : base(webElement)
        {
        }
        public void setSearchText(String text) {
            input.Clear();
            input.SendKeys(text);
        }
        public void clickSearch()
        {
            button.Click();
                }

    }
}

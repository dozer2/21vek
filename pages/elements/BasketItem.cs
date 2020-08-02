using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.elements
{
    public class BasketItem : Element
    {
        [FindsBy(How = How.XPath, Using = @".//td[contains(@class,'cr-basket__name')]//a")]
        public Element name { get; set; }

        [FindsBy(How = How.ClassName, Using = @"j-basket__cost")]
        public Element price { get; set; }



        public BasketItem(IWebElement webElement) : base(webElement)
        {
        }
    }
}

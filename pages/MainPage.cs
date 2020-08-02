using Selenium.pages.elements;
using Selenium.pages.popup;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium.pages
{
    public class MainPage
    {
        [FindsBy(How = How.ClassName, Using = @"b-popup")]
        public PopupNotification popup;

        [FindsBy(How = How.ClassName, Using = @"b-nav_container")]
        public CategoryBar NavigationBar { get; set; }

        [FindsBy(How = How.ClassName, Using = @"b-cart")]
        public Element basket;
        
        [FindsBy(How = How.ClassName, Using = @"search__form")]
        public Search search;

        public Boolean isEmptyBasket()
        {
            return basket.getWebElement().FindElement(OpenQA.Selenium.By.Id("j-basket_counter")).GetAttribute("data-count") == "0";
        }
    }
}

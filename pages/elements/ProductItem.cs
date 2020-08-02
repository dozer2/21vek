using OpenQA.Selenium;
using Selenium.core;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.elements
{
   public class ProductItem : Element
    {

        [FindsBy(How = How.XPath, Using = @".//dt[contains(@class,'result__root')]//span[contains(@class,'result__name')]")]
        public Element name;

        [FindsBy(How = How.ClassName, Using = @"j-to_basket")]
        public Element toBasket;

        [FindsBy(How = How.ClassName, Using = @"g-basketbtn")]
        public Element inBasket;

        [FindsBy(How = How.ClassName, Using = @"result__price")]
        public Element resultPrice;
        
        [FindsBy(How = How.ClassName, Using = @"item__notification")]
        public Element notification;

        public ProductItem(IWebElement webElement) : base(webElement)
        {
        }


        public String getPrice() {
            return resultPrice.FindElement(By.ClassName("g-item-data")).Text;
        }

        public void putProductToBasket() {
            toBasket.Click();
            DriverManager.GetInstance().waitExist(inBasket);
        }
    }
}

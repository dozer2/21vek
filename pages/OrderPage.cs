using OpenQA.Selenium;
using Selenium.core;
using Selenium.pages.elements;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages
{
   public class OrderPage: MainPage
    {
        [FindsBy(How = How.ClassName, Using = @"j-basket-item")]
        public List<BasketItem> orders;


        [FindsBy(How = How.ClassName, Using = @"cr-button__order")]
        public Element orderButton;
        
        [FindsBy(How = How.Id, Using = @"j-basket__confirm")]
        public Element confirmButton;

        public BasketItem getItem(String nameProduct)
        {

            foreach (BasketItem p in orders)
            {
                if (p.name.Text.Equals(nameProduct))
                {
                    return p;
                }
            }
            throw new ArgumentException("Order with name " + nameProduct + " cant found");

        }

        public Boolean isExistsEmptyRequiredFields() {
            return DriverManager.GetInstance().getWebDriver().FindElements(By.XPath(".//div[contains(@id,'j-order_form')]//span[contains(@class,'g-form__message')]")).Count > 0;
        }
    }
}

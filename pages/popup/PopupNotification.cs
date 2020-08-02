using OpenQA.Selenium;
using Selenium.pages.elements;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.popup
{
  public class PopupNotification: Element
    {
        [FindsBy(How = How.XPath, Using = @"(.//fieldset[contains(@class,'g-form__controls')]//label[contains(@class,'g-form__label')])[1]//input")]
        public Element name;
        [FindsBy(How = How.XPath, Using = @"(.//fieldset[contains(@class,'g-form__controls')]//label[contains(@class,'g-form__label')])[2]//input")]
        public Element email;
        [FindsBy(How = How.CssSelector, Using = @"[class='g-form__label g-form__set']")]
        public Element message;
        [FindsBy(How = How.XPath, Using = @".//*[starts-with(@class,'g-form__tools')]//button[starts-with(@class,'g-button')]")]
        public Element button;

        public PopupNotification(IWebElement webElement) : base(webElement)
        {
        }

        public Boolean isExistsEmptyRequiredFields()
        {
            return FindElements(By.XPath(".//div[contains(@class,'g-form__inputwrap')]//span[contains(@class,'g-form__message')]")).Count > 0;
        }

    }
}

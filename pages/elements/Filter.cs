using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.elements
{
    public class Filter : Element
    {

        [FindsBy(How = How.Name, Using = @"filter[price][from]")]
        public Element PriceFrom { get; set; }

        [FindsBy(How = How.Name, Using = @"filter[price][to]")]
        public Element PriceTo { get; set; }

        [FindsBy(How = How.XPath, Using = @".//dd[starts-with(@class,'filter-attr__value')]//label[starts-with(@title,'В наличии')]")]
        public CheckBox InStock { get; set; }

        [FindsBy(How = How.XPath, Using = @".//dl[contains(@class,'j-producers')]//dd[contains(@class,'filter-attr__value')]")]
        public List<CheckBox> producers;

        [FindsBy(How = How.XPath, Using = @".//div[contains(@class,'g-box_lseparator')]//dl[contains(@class,'b-filter-attr')]")]
        public List<Category> categories;

        [FindsBy(How = How.Name, Using = @"filter[sa]")]
        public Element showResult { get; set; }


        public Filter(IWebElement webElement) : base(webElement)
        {
        }

        public CheckBox GetProducer(String name)
        {
            foreach (CheckBox p in producers)
            {
                if (p.Text.Equals(name))
                {
                    return p;
                }
            }
            throw new ArgumentException("Producer with name " + name + " cant found");

        }

        public Category GetCategory(String name)
        {
            foreach (Category p in categories)
            {
                if (p.Text.Equals(name))
                {
                    return p;
                }
            }
            throw new ArgumentException("Category with name " + name + " cant found");

        }

        public class CheckBox : Element
        {

            public new string Text => webElement.Text==""? GetAttribute("title"): webElement.Text;
            public CheckBox(IWebElement webElement) : base(webElement)
            {
            }

            public Boolean isChecked()
            {
                return GetAttribute("class").Contains("g-form__checked");

            }

            public void Checked()
            {
                if (!isChecked())
                {
                    Click();
                }

            }

            public void UnChecked()
            {
                if (isChecked())
                {
                    Click();
                }
            }


        }

        public class Category : Element
        {

            [FindsBy(How = How.ClassName, Using = @"g-form__checklabel")]
            public List<CheckBox> items;

            
            [FindsBy(How = How.ClassName, Using = @"g-pseudo_href")]
            public Element name;

            [FindsBy(How = How.ClassName, Using = @"j-plus__toggle")]
            public Element showAll;

            public Category(IWebElement webElement) : base(webElement)
            {
            }

            private Boolean isOpen()
            {
                return GetAttribute("class").Contains("cr-filter__unfold");

            }

            public void Open() {
                if (!isOpen())
                {
                    name.Click();
                }
            }

            public CheckBox GetItem(String name)
            {
                foreach (CheckBox p in items)
                {
                    if (p.Text.Equals(name))
                    {
                        return p;
                    }
                }
                throw new ArgumentException("Item with name " + name + " cant found");

            }

            public void showAllItem() {
                if (showAll.isExist())
                {
                    showAll.Click();
                }
            }
        }

    }
}

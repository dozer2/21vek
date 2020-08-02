using Selenium.pages.elements;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages
{
    public class ProductPage : MainPage
    {
        [FindsBy(How = How.ClassName, Using = @"b-filter")]
        public Filter Filter { get; set; }

        [FindsBy(How = How.XPath, Using = @".//*[contains(@id,'j-result-page-1')]//li[contains(@class,'result__item')]")]
        public List<ProductItem> Products;


        public ProductItem getProduct(String nameProduct) {

            foreach (ProductItem p in Products)
            {
                if (p.name.Text.Equals(nameProduct))
                {
                    return p;
                }
            }
            throw new ArgumentException("Product with name " + nameProduct + " cant found");

        }
    }
}

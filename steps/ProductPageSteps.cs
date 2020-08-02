using NUnit.Framework;
using Selenium.core;
using Selenium.pages;
using Selenium.pages.elements;
using Selenium.pages.popup;
using System;
using System.Collections.Generic;
using System.Text;
using static Selenium.pages.elements.Filter;

namespace Selenium.steps
{
    public class ProductPageSteps : BaseSteps<ProductPageSteps>
    {
        ProductPage productPage;

        public ProductPageSteps(ProductPage productPage)
        {
            this.productPage = productPage;
        }

     
        public FilterSteps getFilter()
        {
            return new FilterSteps(productPage.Filter, this);
        }

        public ProductsSteps getProduct(String name) {
            return new ProductsSteps(productPage.getProduct(name), this);
        }

        public override ProductPageSteps refreshPage()
        {
            driverManager.waitLoadPage().initPage(productPage);
            return this;
        }
        public OrderPageSteps openBasket()
        {
            productPage.basket.Click();
            OrderPage orderPage = new OrderPage();
            DriverManager.GetInstance().waitLoadPage().initPage(orderPage);
            return new OrderPageSteps(orderPage);
        }
        public PopupNotification getPopUp() {
            return productPage.popup;
        }

       

    }


    public class FilterSteps {
        Filter filter;
        ProductPageSteps productPageSteps;

        public FilterSteps(Filter filter,ProductPageSteps productPageSteps)
        {
            this.filter = filter;
            this.productPageSteps = productPageSteps;
        }

        public FilterSteps setPriceFrom(int value)
        {
            filter.PriceFrom.SendKeys(value.ToString());
            return this;
        }

        public FilterSteps setPriceTo(int value)
        {
            filter.PriceTo.SendKeys(value.ToString());
            return this;
        }

        public FilterSteps setInStock(Boolean check)
        {
            if (check) filter.InStock.Checked(); else filter.InStock.UnChecked();
            return this;
        }

        public FilterSteps setProducer(String producer, Boolean check)
        {
            if (check) filter.GetProducer(producer).Checked(); else filter.GetProducer(producer).UnChecked();
            return this;
        }


        public CategorySteps openCategory(String category)
        {
            Category cat = filter.GetCategory(category);
            cat.Open();
            cat.showAllItem();
            return new CategorySteps(cat, this);

        }
        public ProductPageSteps showFilterResult()
        {
            filter.showResult.Click();
            productPageSteps.refreshPage();
            return productPageSteps;
        }
       
    }
    public class CategorySteps
    {

        Category category;
        FilterSteps filterSteps;


        public CategorySteps(Category category, FilterSteps filterSteps)
        {
            this.category = category;
            this.filterSteps = filterSteps;
        }

        public CategorySteps setItem(String item, Boolean check)
        {
            if (check) category.GetItem(item).Checked(); else category.GetItem(item).Checked();
            return this;
        }

        public FilterSteps getFilter() {
            return filterSteps;
        }
    }

    public class ProductsSteps
    {

        ProductItem product;
        ProductPageSteps productPageStep;

        public ProductsSteps(ProductItem product, ProductPageSteps productPageSteps)
        {
            this.product = product;
            this.productPageStep = productPageSteps;
        }

        public ProductsSteps getPriceToValue(ref String price) {
            price = product.getPrice();
            return this;
        }

        public ProductPageSteps toBasket()
        {
            product.putProductToBasket();
            return productPageStep;
        }

        public PopupStep setWiteList()
        {
            if (product.notification.isExist())
            {
                product.notification.Click();
                productPageStep.refreshPage();
                return new PopupStep(this, productPageStep.getPopUp());
            }
                throw new ArgumentException("Product with name " + product.name.Text + " cant notification");
        }

        public ProductPageSteps checkWiteList()
        {
            Assert.IsTrue(product.notification.isExist(), "Отсутствует текст уведомления о товаре в листе ожидания");
            Assert.IsTrue(product.notification.Text.Equals("В листе ожидания"), "Уведомление не содержит текст 'В листе ожидания'");
            return productPageStep;
        }

    }
    public class PopupStep
    {
        ProductsSteps productSteps;
        PopupNotification popupNotification;
        public PopupStep(ProductsSteps productSteps, PopupNotification popupNotification)
        {
            this.popupNotification = popupNotification;
            this.productSteps = productSteps;
        }
        public PopupStep send() {
            popupNotification.button.Click();
            return this;
        }

        public ProductsSteps close()
        {
             send();
            return productSteps;
        }

        public PopupStep checkExistsEmptyRequiredFields()
        {
            Assert.IsTrue(popupNotification.isExistsEmptyRequiredFields(), "Обязательные незаполненные поля отсутствуют");
            return this;
        }

        public PopupStep setEmail(String email)
        {
            popupNotification.email.Clear();
            popupNotification.email.SendKeys(email);
            return this;
        }

        public PopupStep setName(String name)
        {
            popupNotification.name.Clear();
            popupNotification.name.SendKeys(name);
            return this;
        }

        public PopupStep checkNotificationMessage(String text)
        {
            DriverManager.GetInstance().waitExist(popupNotification.message);
            Assert.IsTrue(popupNotification.message.Text.Equals(text), "Уведомление не содержит текст " + text);
            return this;
        }


    }
}

using NUnit.Framework;
using Selenium.pages;
using Selenium.pages.elements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Selenium.steps
{
    public class OrderPageSteps : BaseSteps<OrderPageSteps>
    {
        OrderPage orderPage;

        public OrderPageSteps(OrderPage orderPage)
        {
            this.orderPage = orderPage;
        }

        public override OrderPageSteps refreshPage()
        {
            driverManager.waitLoadPage().initPage(orderPage);
            return this;
        }

        public BasketItemStep getItem(String name) {
            return new BasketItemStep(orderPage.getItem(name), this);
        }

        public OrderPageSteps buyOrders()
        {
            orderPage.orderButton.Click();
            refreshPage();
            return  this;
        }

        public OrderPageSteps confirmOrder()
        {
            orderPage.confirmButton.Click();
            return this;
        }

        public OrderPageSteps checkExistsEmptyRequiredFields()
        {
            Assert.IsTrue(orderPage.isExistsEmptyRequiredFields(),"Обязательные незаполненные поля отсутствуют");
            return this;
        }
    }

    public class BasketItemStep {
        BasketItem order;
        OrderPageSteps orderSteps;

        public BasketItemStep(BasketItem order,OrderPageSteps orderSteps)
        {
            this.order = order;
            this.orderSteps = orderSteps;
        }

        public BasketItemStep checkPrice(String expectedPrice) {
            String actualResult=Regex.Replace(order.price.Text, @"\s+", "");
            expectedPrice= Regex.Replace(expectedPrice, @"\s+", "");
            actualResult = actualResult.Substring(0, actualResult.IndexOf("р"));
            Assert.IsTrue(expectedPrice.Equals(actualResult), "Сотимость в корзине: "+expectedPrice+" не совпадает с стоимостью из каталога: "+actualResult);
            return this;
        }

        public OrderPageSteps returnToBasket() {
            return orderSteps;
         }



    }
}

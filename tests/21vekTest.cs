using NUnit.Framework;
using Selenium.core;
using Selenium.pages;
using Selenium.steps;
using System;

namespace Selenium.tests
{
    [TestFixture]
    public class _21vekTest : TestBase
    {
        private static Boolean CHECKED = true;
        
        [Test]
        public void BuyOrder()
        {
            String price="";
            String productName = "Ноутбук Lenovo ThinkPad X1 Carbon Gen 8 (20U90006RT)";

            new MainPageStep().openMainPage()
                .openCategory("Компьютеры")
                .getSubGroup("Компьютерная техника")
                .openSubItem("Ноутбуки")
                .getFilter()
                .setPriceFrom(1200)
                .setPriceTo(6480)
                .setInStock(CHECKED)
                .setProducer("Lenovo", CHECKED)
                .openCategory("Линейка")
                .setItem("IdeaPad L (Lenovo)", CHECKED)
                .setItem("Legion (Lenovo)", CHECKED)
                .setItem("Lenovo ThinkPad X", CHECKED).getFilter()
                .openCategory("Тип")
                .setItem("ультрабук", CHECKED).getFilter()
                .showFilterResult()
                .getProduct(productName)
                .getPriceToValue(ref price)
                .toBasket()
                .openBasket()
                .getItem(productName)
                .checkPrice(price)
                .returnToBasket()
                .buyOrders()
                .confirmOrder()
                .checkExistsEmptyRequiredFields();
           
        }

        [Test]
        public void WaitList()
        {
            String productName = "Пылесос National NH-VC2075";
            new MainPageStep().openMainPage()
                .search("National")
                .getProduct(productName)
                .setWiteList()
                .send()
                .checkExistsEmptyRequiredFields()
                .setName("вася")
                .setEmail("good@job.su")
                .send()
                .checkNotificationMessage("Если товар появится на складе, вам придет сообщение на почту.")
                .close()
                .checkWiteList();
      }
    }
}

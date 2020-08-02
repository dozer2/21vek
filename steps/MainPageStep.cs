using Selenium.core;
using Selenium.pages;
using Selenium.pages.elements;
using Selenium.pages.popup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.steps
{
    public class MainPageStep : BaseSteps<MainPageStep>
    {
        MainPage mainPage = new MainPage();

        public MainPageStep openMainPage()
        {
            driverManager.getWebDriver().Url = @"http://21vek.by";
            return refreshPage();
        }

        public override MainPageStep refreshPage()
        {
            driverManager.waitLoadPage().initPage(mainPage);
            return this;
        }

        public CategoryBarSteps openCategory(String categoryName) {
            CategoryBarSteps categoryBarSteps= new CategoryBarSteps();
            categoryBarSteps.setCategoryPopUp(mainPage.NavigationBar.OpenCategory(categoryName));
            return categoryBarSteps;
        }

        public ProductPageSteps search(String text) {
            mainPage.search.setSearchText(text);
            mainPage.search.clickSearch();
            ProductPage productPage = new ProductPage();
            driverManager.waitLoadPage().initPage(productPage);
            return new ProductPageSteps(productPage);
        }
    }

    public class CategoryBarSteps : BaseSteps<CategoryBarSteps>
    {
        MainPage mainPage = new MainPage();

        CategoryPopUp categoryPopUp;

        public CategoryBarSteps setCategoryPopUp(CategoryPopUp categoryPop)
        {
            categoryPopUp = categoryPop;
            return this;
        }
        public ProductPageSteps openSubGroup(String subGroupName)
        {
            if (categoryPopUp == null)
            {
                throw new ArgumentException("CategoryPopUp is not init");
            }
            categoryPopUp.ClickToHeader(subGroupName);
            ProductPage productPage = new ProductPage();
             driverManager.waitLoadPage().initPage(productPage);
            return new ProductPageSteps(productPage);

        }
        public CategorySubGroupSteps getSubGroup(String subGroupName)
        {
            if (categoryPopUp == null)
            {
                throw new ArgumentException("CategoryPopUp is not init");
            }
            return new CategorySubGroupSteps(categoryPopUp.GetSubGroup(subGroupName),mainPage);

        }
        public override CategoryBarSteps refreshPage()
        {
            driverManager.initPage(mainPage);
            return this;
        }
    }

    public class CategorySubGroupSteps : BaseSteps<CategorySubGroupSteps>
    {
        MainPage mainPage;

        CategoryPopUpSubGroup categoryPopUpSubGroup;

        public CategorySubGroupSteps(CategoryPopUpSubGroup categoryPopUpSubGroup,MainPage mainPage)
        {
            this.categoryPopUpSubGroup = categoryPopUpSubGroup;
            this.mainPage = mainPage;
        }

        public CategorySubGroupSteps setCategoryPopUpSubGroup(CategoryPopUpSubGroup categoryPop)
        {
            categoryPopUpSubGroup = categoryPop;
            return this;
        }
        public ProductPageSteps openSubItem(String subItemName)
        {
            if (categoryPopUpSubGroup == null)
            {
                throw new ArgumentException("CategoryPopUpSubGroup is not init");
            }
            categoryPopUpSubGroup.ClickToSubItem(subItemName);
            ProductPage productPage = new ProductPage();
            driverManager.waitLoadPage().initPage(productPage);
            return new ProductPageSteps(productPage);

        }
      
        public override CategorySubGroupSteps refreshPage()
        {
            driverManager.waitLoadPage().initPage(mainPage);
            return this;
        }
    }

    
}

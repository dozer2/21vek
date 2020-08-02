
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.pages.elements
{

    public class CategoryBar : Element
    {
        [FindsBy(How = How.ClassName, Using = @"nav__item")]
        public List<CategoryItem> NavigationItems { get; set; }
        [FindsBy(How = How.ClassName, Using = @"l-nav-subgroup")]
        public CategoryPopUp PopUp { get; set; }
        public CategoryBar(IWebElement webElement) : base(webElement)
        {
        }

        public CategoryPopUp OpenCategory(String categoryName)
        {
            foreach (CategoryItem i in NavigationItems)
            {
                if (i.Text.Equals(categoryName))
                {
                    i.Click();
                    return PopUp;
                }
            }

            throw new ArgumentException("Category with name "+ categoryName+" cant found");
        }

    }

    public class CategoryItem : Element
    {
        [FindsBy(How = How.ClassName, Using = @"nav__link")]
        public Element item { get; set; }
        public CategoryItem(IWebElement webElement) : base(webElement)
        {

        }

    }

    public class CategoryPopUp : Element
    {
        [FindsBy(How = How.ClassName, Using = @"nav-subgroup__item")]
        public List<CategoryPopUpSubGroup> SubGroups { get; set; }

        public CategoryPopUp(IWebElement webElement) : base(webElement)
        {
        }


        public CategoryPopUpSubGroup GetSubGroup(String subgroupName) {
            foreach (CategoryPopUpSubGroup i in SubGroups)
            {
                if (i.HeaderGroup.Text.Equals(subgroupName))
                {
                    return i;
                }
            }
            throw new ArgumentException("SubGroup with name " + subgroupName + " cant found");
        }

        public void ClickToHeader(String subgroupName) {
            foreach (CategoryPopUpSubGroup i in SubGroups)
            {
                if (i.HeaderGroup.Text.Equals(subgroupName))
                {
                    i.HeaderGroup.FindElement(By.TagName("A")).Click();
                    return;
                   
                }
            }
            throw new ArgumentException("SubGroup with name " + subgroupName + " cant found");
        }
    }

   

    public class CategoryPopUpSubGroup : Element
    {
        [FindsBy(How = How.ClassName, Using = @"nav-sub__header")]
        public Element HeaderGroup { get; set; }
        [FindsBy(How = How.ClassName, Using = @"nav-sub__item")]
        public List<Element> SubItem { get; set; }


        public CategoryPopUpSubGroup(IWebElement webElement) : base(webElement)
        {
        }
        public void ClickToSubItem(String subItemName)
        {
            foreach (Element i in SubItem)
            {
                if (i.Text.Equals(subItemName))
                {
                    i.FindElement(By.TagName("A")).Click();
                    return;
                  
                }
            }
            throw new ArgumentException("SubItem with name " + subItemName + " cant found");
        }

    }

    

}

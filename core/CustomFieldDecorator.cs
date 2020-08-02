using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium;
using Selenium.pages.elements;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections;

namespace Selenium.core
{
    public class CustomFieldDecorator : DefaultPageObjectMemberDecorator, IPageObjectMemberDecorator
    {

        public new object Decorate(MemberInfo member, SeleniumExtras.PageObjects.IElementLocator locator)
        {

            FieldInfo field = member as FieldInfo;
            PropertyInfo property = member as PropertyInfo;
            Type targetType = null;

            if (field != null)
            {
                targetType = field.FieldType;
            }

            bool hasPropertySet = false;
            if (property != null)
            {
                hasPropertySet = property.CanWrite;
                targetType = property.PropertyType;
            }

            if (field == null & (property == null || !hasPropertySet))
            {
                return null;
            }

            if (targetType.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>)) != null
                && typeof(Element).IsAssignableFrom(targetType.GetGenericArguments()[0]))
            {
                if (!Attribute.IsDefined(member, typeof(FindsByAttribute)) && !Attribute.IsDefined(member, typeof(FindsByAllAttribute)))
                {
                    return null;
                }

            }
            else if (!typeof(Element).IsAssignableFrom(targetType)) {
                return null;
            }
            
            IList<By> bys = CreateLocatorList(member);
            if (bys.Count > 0)
            {
                bool cache = ShouldCacheLookup(member);
                object proxyObject = CreateProxyObject(targetType, locator, bys, cache);
                return proxyObject;
            }

            return null;
        }


        private static IList createListElements(Type returnType,Type memberType, ReadOnlyCollection<IWebElement> elements) {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(returnType);
            var instance = Activator.CreateInstance(constructedListType);
            IList customs = (IList)instance;
            foreach (var element in elements)
            {
                Element elem = (Element)memberType.GetGenericArguments()[0].GetConstructor(new[] { typeof(IWebElement) }).Invoke(new object[] { element });
                PageFactory.InitElements(element, elem, new CustomFieldDecorator());
                customs.Add(elem);

            }
            return customs;
        }
        private static object CreateProxyObject(Type memberType, IElementLocator locator, IEnumerable<By> bys, bool cache)
        {
            object proxyObject = null;
             if (memberType.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>)) != null && typeof(Element).IsAssignableFrom(memberType.GetGenericArguments()[0]))
            {
                ReadOnlyCollection<IWebElement> elements = locator.LocateElements(bys);
                proxyObject = createListElements (memberType.GetGenericArguments()[0],memberType,elements);
            }
            else if (typeof(Element).IsAssignableFrom(memberType))
            {
                IWebElement webElement = (IWebElement)WebElementProxy.CreateProxy(locator, bys, cache);
                Element element = (Element)memberType.GetConstructor(new[] { typeof(IWebElement) }).Invoke(new object[] { webElement });
                PageFactory.InitElements(locator.SearchContext,element,new CustomFieldDecorator());
                proxyObject = element;

            }
            else
            {
                throw new ArgumentException("Type of member '" + memberType.Name + "'Element or IList<Element>");
            }
            return proxyObject;
        }
    }
}

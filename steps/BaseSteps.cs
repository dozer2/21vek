using Selenium.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.steps
{
   public abstract class BaseSteps<T>
    {
       protected DriverManager driverManager = DriverManager.GetInstance();

        public abstract T refreshPage();
    }

    
}

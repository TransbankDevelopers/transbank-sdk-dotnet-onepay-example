using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Transbank.Onepay;

namespace OnepayMVCTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Onepay.ApiKey = "mUc0GxYGor6X8u-_oB3e-HWJulRG01WoC96-_tUA3Bg";
            Onepay.SharedSecret = "P4DCPS55QB2QLT56SQH6#W#LV76IAPYX";
            Onepay.IntegrationType = Transbank.Onepay.Enums.OnepayIntegrationType.TEST;
        }
    }
}

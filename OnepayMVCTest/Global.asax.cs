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
            
            // Onepay.ApiKey = "dKVhq1WGt_XapIYirTXNyUKoWTDFfxaEV63-O5jcsdw";
            // Onepay.SharedSecret = "?XW#WOLG##FBAGEAYSNQ5APD#JF@$AYZ";
            // Onepay.IntegrationType = Transbank.Onepay.Enums.OnepayIntegrationType.Test;
        }
    }
}

﻿using Models;
using Ninject;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestWebApp
{
    
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IKernel AppKernel;
        public static DBManager dbmanager;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            AppKernel = new StandardKernel(new DBProviderNinjectModule());
            dbmanager = AppKernel.Get<DBManager>();

            

        }
    }
   
}

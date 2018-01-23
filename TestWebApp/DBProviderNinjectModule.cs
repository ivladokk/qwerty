using Ninject.Modules;
using Repository;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TestWebApp
{
    public class DBProviderNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var constr = ConfigurationManager.AppSettings["DBConnectionString"];

            //Bind<IDBRepository>().To<ADORepository>().WithConstructorArgument("constr", constr);
            Bind<IDBRepository>().To<EFRepository>().WithConstructorArgument("constr", constr);
        }
    }
}
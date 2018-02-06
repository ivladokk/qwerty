using Ninject.Modules;
using Repository;
using Repository.ADORepository;
using Repository.EFRepository;
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
            var provider = ConfigurationManager.AppSettings["DBProvider"];
            switch (provider)
            {
                case "ADO.NET":
                    {
                        Bind<IDBRepository>().To<ADORepository>().WithConstructorArgument("constr", constr);
                        break;
                    }
                case "EF":
                    {
                        Bind<IDBRepository>().To<EFRepository>().WithConstructorArgument("constr", constr);
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown DBProvider");
                    }
            }

            //Bind<IDBRepository>().To<ADORepository>().WithConstructorArgument("constr", constr);
            //Bind<IDBRepository>().To<EFRepository>().WithConstructorArgument("constr", constr);
        }
    }
}
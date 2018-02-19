using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Repository;
using Repository.ADORepository;
using Repository.EFRepository;
using TestWebApp.Auth;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TestWebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TestWebApp.App_Start.NinjectWebCommon), "Stop")]
namespace TestWebApp.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var constr = ConfigurationManager.AppSettings["DBConnectionString"];
            switch (ConfigurationManager.AppSettings["DBProvider"])
            {
                case "ADO.NET":
                    {
                        kernel.Bind<IDBRepository>().To<ADORepository>().InRequestScope().WithConstructorArgument("constr", constr);
                        break;
                    }
                case "EF":
                    {
                        kernel.Bind<IDBRepository>().To<EFRepository>().InRequestScope().WithConstructorArgument("constr", constr);
                        break;
                    }
                default:
                    {
                        throw new Exception("Unknown DBProvider");
                    }
            }

            kernel.Bind<IAuthentication>().To<MyAuthentication>().InRequestScope();
        }
    }


}
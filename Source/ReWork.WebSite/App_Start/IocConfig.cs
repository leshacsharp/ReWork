using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin.Security.DataProtection;
using ReWork.Logic.Ioc;
using ReWork.WebSite.Ioc;
using System.Web.Mvc;

namespace ReWork.WebSite.App_Start
{
    public class IocConfig
    {
        public static void ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<DbModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServicesModule>();

            builder.Register<IDataProtectionProvider>(p => Startup.DataProtectionProvider).InstancePerLifetimeScope();


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
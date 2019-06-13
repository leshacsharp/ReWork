using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin.Security.DataProtection;
using Quartz;
using Quartz.Impl;
using ReWork.Logic.Ioc;
using ReWork.WebSite.Ioc;
using ReWork.WebSite.Jobs;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ReWork.WebSite.App_Start
{
    public class IocConfig
    {
        public static async void ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<DbModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServicesModule>();

            builder.Register<IDataProtectionProvider>(p => Startup.DataProtectionProvider).InstancePerLifetimeScope();

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            builder.Register(x => scheduler).As<IScheduler>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => typeof(IJob).IsAssignableFrom(x));

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            await ConfigureSchelder(container);
        }

        private static async Task ConfigureSchelder(IContainer container)
        {
            IScheduler sched = container.Resolve<IScheduler>();
            sched.JobFactory = new AutofacJobFactory(container);
            await sched.Start();

            IJobDetail job = JobBuilder.Create<EmailSender>().WithIdentity("emailJob", "group1").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("emailTrigger", "group1")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(15)
                    .RepeatForever())
                .StartNow()
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.DataProvider.Repositories.Implementation;
using ReWork.Model.Entities;

namespace ReWork.Logic.Ioc
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository>().As<IBaseRepository>().InstancePerRequest();

            builder.RegisterType<CustomerProfileRepository>().As<ICustomerProfileRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<EmployeeProfileRepository>().As<IEmployeeProfileRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<JobRepository>().As<IJobRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<FeedBackRepository>().As<IFeedBackRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().PropertiesAutowired().InstancePerRequest();

            builder.RegisterType<AppUserManager>().As<UserManager<User>>().InstancePerRequest();
            builder.RegisterType<AppRoleManager>().As<RoleManager<IdentityRole>>().InstancePerRequest();
        }
    }
}

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
            builder.RegisterType<CustomerProfileRepository>().As<ICustomerProfileRepository>().InstancePerRequest();
            builder.RegisterType<EmployeeProfileRepository>().As<IEmployeeProfileRepository>().InstancePerRequest();
            builder.RegisterType<JobRepository>().As<IJobRepository>().InstancePerRequest();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>().InstancePerRequest();
            builder.RegisterType<FeedBackRepository>().As<IFeedBackRepository>().InstancePerRequest();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().InstancePerRequest();

            builder.RegisterType<AppUserManager>().As<UserManager<User>>().InstancePerRequest();
            builder.RegisterType<AppRoleManager>().As<RoleManager<IdentityRole>>().InstancePerRequest();
        }
    }
}

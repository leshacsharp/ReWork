using Autofac;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Implementation;

namespace ReWork.WebSite.Ioc
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerRequest();
            builder.RegisterType<CustomerProfileService>().As<ICustomerProfileService>().InstancePerRequest();
            builder.RegisterType<EmployeeProfileService>().As<IEmployeeProfileService>().InstancePerRequest();

            builder.RegisterType<JobService>().As<IJobService>().InstancePerRequest();

            builder.RegisterType<SectionService>().As<ISectionService>().InstancePerRequest();
            builder.RegisterType<SkillService>().As<ISkillService>().InstancePerRequest();
        }
    }
}
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReWork.DataProvider.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.DataProvider.Repositories.Implementation;
using ReWork.Logic.Hubs.Abstraction;
using ReWork.Logic.Hubs.Implementation;
using ReWork.Model.Entities;

namespace ReWork.Logic.Ioc
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository>().As<IBaseRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerProfileRepository>().As<ICustomerProfileRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<EmployeeProfileRepository>().As<IEmployeeProfileRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<JobRepository>().As<IJobRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<OfferRepository>().As<IOfferRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<FeedBackRepository>().As<IFeedBackRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SectionRepository>().As<ISectionRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().PropertiesAutowired().InstancePerRequest();

            builder.RegisterType<NotificationRepository>().As<INotificationRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<NotificationHub>().As<INotificationHub>().InstancePerRequest();

            builder.RegisterType<ChatRoomRepository>().As<IChatRoomRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>().PropertiesAutowired().InstancePerRequest();
            builder.RegisterType<ChatHub>().As<IChatHub>().InstancePerRequest();

            builder.RegisterType<AppUserManager>().As<UserManager<User>>().InstancePerLifetimeScope();
            builder.RegisterType<AppRoleManager>().As<RoleManager<IdentityRole>>().InstancePerRequest();
        }
    }
}

using Autofac;
using ReWork.Logic.Infustructure;
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

            builder.RegisterType<FeedBackService>().As<IFeedBackService>().InstancePerRequest();
            builder.RegisterType<JobService>().As<IJobService>().InstancePerRequest();
            builder.RegisterType<OfferService>().As<IOfferService>().InstancePerRequest();

            builder.RegisterType<ChatRoomService>().As<IChatRoomService>().InstancePerRequest();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerRequest();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerRequest();

            builder.RegisterType<EmailService>().As<ISendMessageService<EmailMessage>>().InstancePerLifetimeScope();

            builder.RegisterType<SectionService>().As<ISectionService>().InstancePerRequest();
            builder.RegisterType<SkillService>().As<ISkillService>().InstancePerRequest();
        }
    }
}
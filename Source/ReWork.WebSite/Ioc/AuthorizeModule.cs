using Autofac;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Implementation;

namespace ReWork.WebSite.Ioc
{
    public class AuthorizeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
        }
    }
}
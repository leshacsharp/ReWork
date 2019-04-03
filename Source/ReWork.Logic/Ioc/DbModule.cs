using Autofac;
using ReWork.DataProvider.UnitOfWork;

namespace ReWork.Logic.Ioc
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReWorkUnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}

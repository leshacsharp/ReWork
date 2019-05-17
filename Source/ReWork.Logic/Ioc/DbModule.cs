using Autofac;
using ReWork.Model.Context;

namespace ReWork.Logic.Ioc
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReWorkContext>().As<IDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<CommitProvider>().As<ICommitProvider>().InstancePerRequest();
        }
    }
}

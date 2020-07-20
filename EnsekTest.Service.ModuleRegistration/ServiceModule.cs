using Autofac;
using EnsekTest.Service.Abstractions;

namespace EnsekTest.Service.ModuleRegistration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MeterReadingService>()
                .As<IMeterReadingService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();
        }
    }
}
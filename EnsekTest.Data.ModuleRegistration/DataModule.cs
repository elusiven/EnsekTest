using Autofac;

namespace EnsekTest.Data.ModuleRegistration
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder
            //    .RegisterType<EmailProcessingService>()
            //    .As<IEmailProcessingService>()
            //    .InstancePerLifetimeScope();
        }
    }
}
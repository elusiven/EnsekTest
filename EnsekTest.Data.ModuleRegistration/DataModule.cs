using Autofac;
using EnsekTest.Data.Abstractions;

namespace EnsekTest.Data.ModuleRegistration
{
    public class DataModule : Module
    {
        private readonly string _connectionString;

        public DataModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MeterReadingRepository>()
                .As<IMeterReadingRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterInstance(new DatabaseConnection(_connectionString))
                .SingleInstance();
        }
    }
}
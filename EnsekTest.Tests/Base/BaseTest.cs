using System.IO;
using EnsekTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnsekTest.Tests.Base
{
    [TestClass]
    public abstract class BaseTest
    {
        protected IConfiguration Configuration;
        protected DatabaseConnection DatabaseConnection;

        protected BaseTest()
        {
            Init();
        }

        [TestInitialize]
        public void Init()
        {
            Configuration = ConfigureAppSettings();
            DatabaseConnection = InitDatabaseConnection();
        }

        private IConfiguration ConfigureAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        private DatabaseConnection InitDatabaseConnection()
        {
            return new DatabaseConnection(Configuration.GetConnectionString("SqlServer"));
        }
    }
}
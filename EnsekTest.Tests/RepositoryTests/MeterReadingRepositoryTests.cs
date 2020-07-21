using System;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Data;
using EnsekTest.Data.Primitives.Entities;
using EnsekTest.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnsekTest.Tests.RepositoryTests
{
    [TestClass]
    public class MeterReadingRepositoryTests : BaseTest
    {
        [TestMethod]
        public async Task Get_All_MeterReadings()
        {
            // Arrange
            var meterReadingRepository = new MeterReadingRepository(DatabaseConnection);

            // Act
            var results = await meterReadingRepository.GetAllMeterReadingsAsync(new CancellationToken(false));

            // Assert
            Assert.IsNotNull(meterReadingRepository);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task Get_MeterReading_By_Id()
        {
            // Arrange
            var meterReadingRepository = new MeterReadingRepository(DatabaseConnection);

            // Act
            var result = await meterReadingRepository.GetMeterReadingAsync(1, new CancellationToken(false));

            // Assert
            Assert.IsNotNull(meterReadingRepository);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Create_New_MeterReading()
        {
            // Arrange
            var meterReadingRepository = new MeterReadingRepository(DatabaseConnection);
            var meterReading = new MeterReading()
            {
                MeterReadValue = "3124",
                MeterReadingDateTime = new DateTime(2020, 06, 19, 5, 20, 45),
                AccountId = 1
            };

            // Act
            var result = await meterReadingRepository.CreateMeterReadingAsync(meterReading, new CancellationToken(false));

            // Assert
            Assert.IsNotNull(meterReadingRepository);
            Assert.IsNotNull(meterReading);
            Assert.IsTrue(result);
        }
    }
}
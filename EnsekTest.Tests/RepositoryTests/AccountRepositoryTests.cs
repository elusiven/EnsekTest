using EnsekTest.Data;
using EnsekTest.Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Data.Primitives.Entities;

namespace EnsekTest.Tests.RepositoryTests
{
    [TestClass]
    public class AccountRepositoryTests : BaseTest
    {
        [TestMethod]
        public async Task Get_All_Accounts()
        {
            // Arrange
            var accountRepository = new AccountRepository(DatabaseConnection);

            // Act
            var results = await accountRepository.GetAllAccountsAsync(new CancellationToken(false));

            // Assert
            Assert.IsNotNull(accountRepository);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task Get_Account_By_Id()
        {
            // Arrange
            var accountRepository = new AccountRepository(DatabaseConnection);

            // Act
            var result = await accountRepository.GetAccountAsync(1, new CancellationToken(false));

            // Assert
            Assert.IsNotNull(accountRepository);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Create_New_Account()
        {
            // Arrange
            var accountRepository = new AccountRepository(DatabaseConnection);
            var account = new Account
            {
                FirstName = "TestName",
                LastName = "LastName"
            };

            // Act
            var result = await accountRepository.CreateAccountAsync(account, new CancellationToken(false));

            // Assert
            Assert.IsNotNull(accountRepository);
            Assert.IsNotNull(account);
            Assert.IsTrue(result);
        }
    }
}
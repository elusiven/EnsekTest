using Dapper;
using EnsekTest.Data.Abstractions;
using EnsekTest.Data.Primitives.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EnsekTest.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public AccountRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            List<Account> entities = new List<Account>();

            const string query = @"SELECT AccountId, FirstName, LastName FROM dbo.Account";

            using (var connection = new SqlConnection(_databaseConnection.Value))
            {
                var results = await connection.QueryAsync<Account>(query);
                entities.AddRange(results);
            }

            return entities;
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            Account entity = new Account();

            const string query = @"SELECT AccountId, FirstName, LastName FROM dbo.Account WHERE AccountId = @AccountId";

            using (var connection = new SqlConnection(_databaseConnection.Value))
            {
                entity = await connection.QueryFirstOrDefaultAsync<Account>(query, new { AccountId = id });
            }

            return entity;
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            bool isSuccess = false;

            const string query = @"INSERT INTO dbo.Account ([FirstName], [LastName]) VALUES(@FirstName, @LastName)";

            using (var conn = new SqlConnection(_databaseConnection.Value))
            {
                var result = await conn.ExecuteAsync(
                    query,
                    new { FirstName = account.FirstName, LastName = account.LastName });

                isSuccess = result > 0;
            }

            return isSuccess;
        }

        public Task<bool> UpdateAccountAsync(Account account)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAccountAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
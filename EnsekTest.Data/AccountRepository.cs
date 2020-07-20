using Dapper;
using EnsekTest.Data.Abstractions;
using EnsekTest.Data.Primitives.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
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

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken)
        {
            HashSet<Account> entities = new HashSet<Account>();

            await Task.Run(async () =>
            {
                const string query = @"SELECT AccountId, FirstName, LastName FROM dbo.Account";

                using (var connection = new SqlConnection(_databaseConnection.Value))
                {
                    var result = await connection.QueryAsync<Account>(query);

                    using (var enumerator = result.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            entities.Add(enumerator.Current);
                        }
                    }
                }
            }, cancellationToken);

            return entities;
        }

        public async Task<Account> GetAccountAsync(int id, CancellationToken cancellationToken)
        {
            Account entity = new Account();

            await Task.Run(async () =>
            {
                const string query = @"SELECT AccountId, FirstName, LastName FROM dbo.Account WHERE AccountId = @AccountId";

                using (var connection = new SqlConnection(_databaseConnection.Value))
                {
                    entity = await connection.QueryFirstOrDefaultAsync<Account>(query, new { AccountId = id });
                }
            }, cancellationToken);

            return entity;
        }

        public async Task<bool> CreateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            await Task.Run(async () =>
            {
                const string query = @"INSERT INTO dbo.Account ([FirstName], [LastName]) VALUES(@FirstName, @LastName)";

                using (var conn = new SqlConnection(_databaseConnection.Value))
                {
                    var result = await conn.ExecuteAsync(
                        query,
                        new { FirstName = account.FirstName, LastName = account.LastName });

                    isSuccess = result > 0;
                }
            }, cancellationToken);

            return isSuccess;
        }

        public Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
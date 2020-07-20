using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Service.Abstractions;
using EnsekTest.Service.Primitives.Models;

namespace EnsekTest.Service
{
    public class AccountService : IAccountService
    {
        public Task<HashSet<AccountResource>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<AccountResource> GetAccountAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAccountAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAccountAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAccountAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
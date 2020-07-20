using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Data.Primitives.Entities;

namespace EnsekTest.Data.Abstractions
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync(CancellationToken cancellationToken);

        Task<Account> GetAccountAsync(int id, CancellationToken cancellationToken);

        Task<bool> CreateAccountAsync(Account account, CancellationToken cancellationToken);

        Task<bool> UpdateAccountAsync(Account account, CancellationToken cancellationToken);

        Task<bool> DeleteAccountAsync(int id, CancellationToken cancellationToken);
    }
}
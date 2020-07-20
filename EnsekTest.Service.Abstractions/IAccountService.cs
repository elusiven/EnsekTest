using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Service.Primitives.Models;

namespace EnsekTest.Service.Abstractions
{
    public interface IAccountService
    {
        Task<HashSet<AccountResource>> GetAllAccountsAsync(CancellationToken cancellationToken = default);

        Task<AccountResource> GetAccountAsync(CancellationToken cancellationToken = default);

        Task CreateAccountAsync(CancellationToken cancellationToken = default);

        Task UpdateAccountAsync(CancellationToken cancellationToken = default);

        Task DeleteAccountAsync(CancellationToken cancellationToken = default);
    }
}
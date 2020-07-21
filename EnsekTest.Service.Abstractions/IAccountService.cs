using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Service.Primitives.Models;
using Microsoft.AspNetCore.Http;

namespace EnsekTest.Service.Abstractions
{
    public interface IAccountService
    {
        Task<HashSet<AccountResource>> GetAllAccountsAsync(CancellationToken cancellationToken = default);

        Task<AccountResource> GetAccountAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ImportAccountsFromCSV(IFormFile file, CancellationToken cancellationToken = default);

        Task CreateAccountAsync(AccountResource modelResource, CancellationToken cancellationToken = default);

        Task UpdateAccountAsync(AccountResource modelResource, CancellationToken cancellationToken = default);

        Task DeleteAccountAsync(int id, CancellationToken cancellationToken = default);
    }
}
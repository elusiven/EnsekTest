using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Service.Primitives.Models;
using Microsoft.AspNetCore.Http;

namespace EnsekTest.Service.Abstractions
{
    public interface IAccountService
    {
        Task<HashSet<AccountResource>> GetAllAccountsAsync();

        Task<AccountResource> GetAccountAsync(int id);

        Task<bool> ImportAccountsFromCSV(IFormFile file);

        Task CreateAccountAsync(AccountResource modelResource);

        Task UpdateAccountAsync(AccountResource modelResource);

        Task DeleteAccountAsync(int id);
    }
}
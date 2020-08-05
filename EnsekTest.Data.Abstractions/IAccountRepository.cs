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
        Task<IEnumerable<Account>> GetAllAccountsAsync();

        Task<Account> GetAccountAsync(int id);

        Task<bool> CreateAccountAsync(Account account);

        Task<bool> UpdateAccountAsync(Account account);

        Task<bool> DeleteAccountAsync(int id);
    }
}
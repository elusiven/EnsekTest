using CsvHelper.Configuration;
using EnsekTest.Service.Primitives.Models;

namespace EnsekTest.Service.Common.CSVMaps
{
    public class AccountMap : ClassMap<AccountResource>
    {
        public AccountMap()
        {
            Map(m => m.AccountId);
            Map(m => m.FirstName);
            Map(m => m.LastName);
        }
    }
}
using EnsekTest.Data.Primitives.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsekTest.Data.Abstractions
{
    public interface IMeterReadingRepository
    {
        Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync();

        Task<MeterReading> GetMeterReadingAsync(int id);

        Task<MeterReading> GetMeterReadingByAccountIdAsync(int accountId);

        Task<bool> CreateMeterReadingAsync(MeterReading account);

        Task<bool> UpdateMeterReadingAsync(MeterReading account);

        Task<bool> DeleteMeterReadingAsync(int id);
    }
}
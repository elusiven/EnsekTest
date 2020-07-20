using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Data.Primitives.Entities;

namespace EnsekTest.Data.Abstractions
{
    public interface IMeterReadingRepository
    {
        Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync(CancellationToken cancellationToken);

        Task<MeterReading> GetMeterReadingAsync(int id, CancellationToken cancellationToken);

        Task<bool> CreateMeterReadingAsync(MeterReading account, CancellationToken cancellationToken);

        Task<bool> UpdateMeterReadingAsync(MeterReading account, CancellationToken cancellationToken);

        Task<bool> DeleteMeterReadingAsync(int id, CancellationToken cancellationToken);
    }
}
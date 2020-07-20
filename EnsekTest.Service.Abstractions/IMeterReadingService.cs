using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnsekTest.Service.Primitives.Models;
using Microsoft.AspNetCore.Http;

namespace EnsekTest.Service.Abstractions
{
    public interface IMeterReadingService
    {
        Task<HashSet<MeterReadingResource>> GetAllMeterReadingsAsync(CancellationToken cancellationToken = default);

        Task<MeterReadingResource> GetMeterReadingAsync(int id, CancellationToken cancellationToken = default);

        Task<MeterReadingResource> GetMeterReadingByAccountIdAsync(int accountId,
            CancellationToken cancellationToken = default);

        Task<bool> ImportMeterReadingsFromCSV(IFormFile file, CancellationToken cancellationToken = default);

        Task CreateMeterReadingAsync(MeterReadingResource model, CancellationToken cancellationToken = default);

        Task UpdateMeterReadingAsync(MeterReadingResource model, CancellationToken cancellationToken = default);

        Task DeleteMeterReadingAsync(int id, CancellationToken cancellationToken = default);
    }
}
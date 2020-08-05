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
        Task<HashSet<MeterReadingResource>> GetAllMeterReadingsAsync();

        Task<MeterReadingResource> GetMeterReadingAsync(int id);

        Task<MeterReadingResource> GetMeterReadingByAccountIdAsync(int accountId);

        Task<bool> ImportMeterReadingsFromCSV(IFormFile file);

        Task CreateMeterReadingAsync(MeterReadingResource model);

        Task UpdateMeterReadingAsync(MeterReadingResource model);

        Task DeleteMeterReadingAsync(int id);
    }
}
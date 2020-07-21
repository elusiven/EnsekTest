using AutoMapper;
using CsvHelper;
using EnsekTest.Data.Abstractions;
using EnsekTest.Data.Primitives.Entities;
using EnsekTest.Service.Abstractions;
using EnsekTest.Service.Common.CSVMaps;
using EnsekTest.Service.Common.Exceptions;
using EnsekTest.Service.Primitives.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnsekTest.Service
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly ILogger<MeterReadingService> _logger;
        private readonly IMapper _mapper;
        private readonly IMeterReadingRepository _meterReadingRepository;
        private readonly IAccountService _accountService;

        public MeterReadingService(
            ILogger<MeterReadingService> logger,
            IMapper mapper,
            IMeterReadingRepository meterReadingRepository,
            IAccountService accountService)
        {
            _logger = logger;
            _mapper = mapper;
            _meterReadingRepository = meterReadingRepository;
            _accountService = accountService;
        }

        public async Task<HashSet<MeterReadingResource>> GetAllMeterReadingsAsync(CancellationToken cancellationToken = default)
        {
            HashSet<MeterReadingResource> resources = new HashSet<MeterReadingResource>();

            var results = await _meterReadingRepository.GetAllMeterReadingsAsync(cancellationToken);
            resources = _mapper.Map<HashSet<MeterReadingResource>>(results);

            return resources;
        }

        public async Task<MeterReadingResource> GetMeterReadingAsync(int id, CancellationToken cancellationToken = default)
        {
            MeterReadingResource resource = new MeterReadingResource();

            var result = await _meterReadingRepository.GetMeterReadingAsync(id, cancellationToken);
            resource = _mapper.Map<MeterReadingResource>(result);

            return resource;
        }

        public async Task<MeterReadingResource> GetMeterReadingByAccountIdAsync(int accountId, CancellationToken cancellationToken = default)
        {
            MeterReadingResource resource = new MeterReadingResource();

            var result = await _meterReadingRepository.GetMeterReadingByAccountIdAsync(accountId, cancellationToken);
            resource = _mapper.Map<MeterReadingResource>(result);

            return resource;
        }

        public async Task<bool> ImportMeterReadingsFromCSV(IFormFile file, CancellationToken cancellationToken = default)
        {
            bool succeeded = true;

            await Task.Run(async () =>
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        List<MeterReadingResource> records = new List<MeterReadingResource>();

                        csv.Configuration.RegisterClassMap<MeterReadingMap>();
                        csv.Configuration.BadDataFound = context =>
                            _logger.LogWarning("Validation failed for one of the fields");

                        while (csv.Read())
                        {
                            try
                            {
                                var record = csv.GetRecord<MeterReadingResource>();
                                records.Add(record);
                            }
                            catch (Exception e)
                            {
                                _logger.LogWarning(e.Message);
                            }
                        }

                        if (!records.Any())
                        {
                            succeeded = false;
                            throw new MeterReadingServiceException(
                                "Could not extract any data from this file.");
                        }

                        var duplicateEntries = records.GroupBy(x => x.AccountId)
                            .Where(g => g.Count() > 1)
                            .Select(y => y.Key)
                            .ToList();

                        foreach (MeterReadingResource meterReadingResource in records)
                        {
                            var existingMeterReadingResource =
                                await GetMeterReadingByAccountIdAsync(meterReadingResource.AccountId,
                                    cancellationToken);

                            var associatedAccount =
                                await _accountService.GetAccountAsync(meterReadingResource.AccountId);

                            if (associatedAccount == null)
                            {
                                _logger.LogWarning($"Meter reading with account ID {meterReadingResource.AccountId} does not have an associated account in the database.");
                                continue;
                            }

                            if (existingMeterReadingResource == null && !duplicateEntries.Contains(meterReadingResource.AccountId))
                            {
                                await CreateMeterReadingAsync(meterReadingResource, cancellationToken);
                            }
                        }
                    }
                }
            }, cancellationToken);

            return succeeded;
        }

        public async Task CreateMeterReadingAsync(MeterReadingResource model, CancellationToken cancellationToken = default)
        {
            if (model == null) throw new MeterReadingServiceException("Resource model can not be null");
            _logger.LogInformation("Creating new meter reading");

            var entity = _mapper.Map<MeterReading>(model);
            var succeeded = await _meterReadingRepository.CreateMeterReadingAsync(entity, cancellationToken);

            if (!succeeded)
            {
                string errorMessage = "Failed to create new meter reading.";
                _logger.LogError(errorMessage);
                throw new MeterReadingServiceException(errorMessage);
            }
        }

        public async Task UpdateMeterReadingAsync(MeterReadingResource model, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Updating existing meter reading");

            var entity = _mapper.Map<MeterReading>(model);
            var succeeded = await _meterReadingRepository.UpdateMeterReadingAsync(entity, cancellationToken);

            if (!succeeded)
            {
                string errorMessage = "Failed to update new meter reading";
                _logger.LogError(errorMessage);
                throw new MeterReadingServiceException(errorMessage);
            }
        }

        public async Task DeleteMeterReadingAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Deleting existing meter reading with ID {id}");

            var succeeded = await _meterReadingRepository.DeleteMeterReadingAsync(id, cancellationToken);

            if (!succeeded)
            {
                string errorMessage = "Failed to delete existing meter reading";
                _logger.LogError(errorMessage);
                throw new MeterReadingServiceException(errorMessage);
            }
        }
    }
}
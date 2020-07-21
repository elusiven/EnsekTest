using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

namespace EnsekTest.Service
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(
            ILogger<AccountService> logger,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<HashSet<AccountResource>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching all accounts from the database");
            HashSet<AccountResource> accountResources = new HashSet<AccountResource>();

            var results = await _accountRepository.GetAllAccountsAsync(cancellationToken);
            accountResources = _mapper.Map<HashSet<AccountResource>>(results);

            return accountResources;
        }

        public async Task<AccountResource> GetAccountAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching a single account from the database");
            AccountResource accountResource = new AccountResource();

            var result = await _accountRepository.GetAccountAsync(id, cancellationToken);
            accountResource = _mapper.Map<AccountResource>(result);

            return accountResource;
        }

        public async Task<bool> ImportAccountsFromCSV(IFormFile file, CancellationToken cancellationToken = default)
        {
            bool succeeded = true;

            await Task.Run(async () =>
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        List<AccountResource> records = new List<AccountResource>();

                        try
                        {
                            csv.Configuration.RegisterClassMap<AccountMap>();
                            while (csv.Read())
                            {
                                var record = csv.GetRecord<AccountResource>();
                                records.Add(record);
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.Message);
                        }

                        if (!records.Any())
                        {
                            succeeded = false;
                            throw new AccountServiceException(
                                "Could not extract any data from this file.");
                        }

                        var duplicateEntries = records.GroupBy(x => x.AccountId)
                            .Where(g => g.Count() > 1)
                            .Select(y => y.Key)
                            .ToList();

                        foreach (AccountResource accountResource in records)
                        {
                            var existingAccountResource =
                                await GetAccountAsync(accountResource.AccountId,
                                    cancellationToken);

                            if (existingAccountResource == null && !duplicateEntries.Contains(accountResource.AccountId))
                            {
                                await CreateAccountAsync(accountResource, cancellationToken);
                            }
                        }
                    }
                }
            }, cancellationToken);

            return succeeded;
        }

        public async Task CreateAccountAsync(AccountResource model, CancellationToken cancellationToken = default)
        {
            if (model == null) throw new AccountServiceException("Resource model cannot be null");
            _logger.LogInformation("Creating a new account record in the database");

            var entity = _mapper.Map<Account>(model);
            var succeeded = await _accountRepository.CreateAccountAsync(entity, cancellationToken);

            if (!succeeded)
            {
                string errorMessage = "Failed to create new account.";
                _logger.LogError(errorMessage);
                throw new AccountServiceException(errorMessage);
            }
        }

        public Task UpdateAccountAsync(AccountResource model, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAccountAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
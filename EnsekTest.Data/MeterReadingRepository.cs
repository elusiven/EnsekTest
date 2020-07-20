using Dapper;
using EnsekTest.Data.Abstractions;
using EnsekTest.Data.Primitives.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace EnsekTest.Data
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly DatabaseConnection _databaseConnection;

        public MeterReadingRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync(CancellationToken cancellationToken)
        {
            HashSet<MeterReading> entities = new HashSet<MeterReading>();

            await Task.Run(async () =>
            {
                const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading";

                using (var connection = new SqlConnection(_databaseConnection.Value))
                {
                    var result = await connection.QueryAsync<MeterReading>(query);

                    using (var enumerator = result.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            entities.Add(enumerator.Current);
                        }
                    }
                }
            }, cancellationToken);

            return entities;
        }

        public async Task<MeterReading> GetMeterReadingAsync(int id, CancellationToken cancellationToken)
        {
            MeterReading entity = new MeterReading();

            await Task.Run(async () =>
            {
                const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading WHERE MeterReadingId = @MeterReadingId";

                using (var connection = new SqlConnection(_databaseConnection.Value))
                {
                    entity = await connection.QueryFirstOrDefaultAsync<MeterReading>(query, new { MeterReadingId = id });
                }
            }, cancellationToken);

            return entity;
        }

        public async Task<MeterReading> GetMeterReadingByAccountIdAsync(int accountId, CancellationToken cancellationToken)
        {
            MeterReading entity = new MeterReading();

            await Task.Run(async () =>
            {
                const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading WHERE AccountId = @AccountId";

                using (var connection = new SqlConnection(_databaseConnection.Value))
                {
                    entity = await connection.QueryFirstOrDefaultAsync<MeterReading>(query, new { AccountId = accountId });
                }
            }, cancellationToken);

            return entity;
        }

        public async Task<bool> CreateMeterReadingAsync(MeterReading meterReading, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            await Task.Run(async () =>
            {
                const string query = @"INSERT INTO dbo.MeterReading ([MeterReadingDateTime], [MeterReadValue], [AccountId]) VALUES(@MeterReadingDateTime, @MeterReadValue, @AccountId)";

                using (var conn = new SqlConnection(_databaseConnection.Value))
                {
                    var result = await conn.ExecuteAsync(
                        query,
                        new { MeterReadingDateTime = meterReading.MeterReadingDateTime, MeterReadValue = meterReading.MeterReadValue, AccountId = meterReading.AccountId });

                    isSuccess = result > 0;
                }
            }, cancellationToken);

            return isSuccess;
        }

        public async Task<bool> UpdateMeterReadingAsync(MeterReading meterReading, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            await Task.Run(async () =>
            {
                const string query = @"UPDATE dbo.MeterReading SET MeterReadingDateTime = @MeterReadingDateTime,  MeterReadValue = @MeterReadValue WHERE AccountId = @AccountId; ";

                using (var conn = new SqlConnection(_databaseConnection.Value))
                {
                    var result = await conn.ExecuteAsync(
                        query,
                        new { MeterReadingDateTime = meterReading.MeterReadingDateTime, MeterReadValue = meterReading.MeterReadValue, AccountId = meterReading.AccountId });

                    isSuccess = result > 0;
                }
            }, cancellationToken);

            return isSuccess;
        }

        public Task<bool> DeleteMeterReadingAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
using Dapper;
using EnsekTest.Data.Abstractions;
using EnsekTest.Data.Primitives.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<IEnumerable<MeterReading>> GetAllMeterReadingsAsync()
        {
            List<MeterReading> entities = new List<MeterReading>();

            const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading";

            using (var connection = new SqlConnection(_databaseConnection.Value))
            {
                var result = await connection.QueryAsync<MeterReading>(query);
                entities.AddRange(result);
            }

            return entities;
        }

        public async Task<MeterReading> GetMeterReadingAsync(int id)
        {
            MeterReading entity = new MeterReading();

            const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading WHERE MeterReadingId = @MeterReadingId";

            using (var connection = new SqlConnection(_databaseConnection.Value))
            {
                entity = await connection.QueryFirstOrDefaultAsync<MeterReading>(query, new { MeterReadingId = id });
            }

            return entity;
        }

        public async Task<MeterReading> GetMeterReadingByAccountIdAsync(int accountId)
        {
            MeterReading entity = new MeterReading();

            const string query = @"SELECT MeterReadingId, AccountId, MeterReadingDateTime, MeterReadValue FROM dbo.MeterReading WHERE AccountId = @AccountId";

            using (var connection = new SqlConnection(_databaseConnection.Value))
            {
                entity = await connection.QueryFirstOrDefaultAsync<MeterReading>(query, new { AccountId = accountId });
            }

            return entity;
        }

        public async Task<bool> CreateMeterReadingAsync(MeterReading meterReading)
        {
            const string query = @"INSERT INTO dbo.MeterReading ([MeterReadingDateTime], [MeterReadValue], [AccountId]) VALUES(@MeterReadingDateTime, @MeterReadValue, @AccountId)";

            using (var conn = new SqlConnection(_databaseConnection.Value))
            {
                var result = await conn.ExecuteAsync(
                    query,
                    new { MeterReadingDateTime = meterReading.MeterReadingDateTime, MeterReadValue = meterReading.MeterReadValue, AccountId = meterReading.AccountId });

                return result > 0;
            }
        }

        public async Task<bool> UpdateMeterReadingAsync(MeterReading meterReading)
        {
            const string query = @"UPDATE dbo.MeterReading SET MeterReadingDateTime = @MeterReadingDateTime,  MeterReadValue = @MeterReadValue WHERE AccountId = @AccountId; ";

            using (var conn = new SqlConnection(_databaseConnection.Value))
            {
                var result = await conn.ExecuteAsync(
                    query,
                    new { MeterReadingDateTime = meterReading.MeterReadingDateTime, MeterReadValue = meterReading.MeterReadValue, AccountId = meterReading.AccountId });

                return result > 0;
            }
        }

        public async Task<bool> DeleteMeterReadingAsync(int id)
        {
            const string query = @"DELETE FROM dbo.MeterReading WHERE MeterReadingId = @MeterReadingId";

            using (var conn = new SqlConnection(_databaseConnection.Value))
            {
                var result = await conn.ExecuteAsync(
                    query,
                    new { MeterReadingId = id });

                return result > 0;
            }
        }
    }
}
using CsvHelper.Configuration.Attributes;
using System;

namespace EnsekTest.Service.Primitives.Models
{
    public class MeterReadingResource
    {
        [Ignore]
        public int MeterReadingId { get; set; }

        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
    }
}
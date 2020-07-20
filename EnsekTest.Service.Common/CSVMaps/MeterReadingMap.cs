using CsvHelper.Configuration;
using EnsekTest.Service.Primitives.Models;
using System.Text.RegularExpressions;

namespace EnsekTest.Service.Common.CSVMaps
{
    public class MeterReadingMap : ClassMap<MeterReadingResource>
    {
        public MeterReadingMap()
        {
            Map(m => m.AccountId);
            Map(m => m.MeterReadingDateTime);
            Map(m => m.MeterReadValue).Validate(ValidateMeterReadValue);
        }

        private bool ValidateMeterReadValue(string input)
        {
            return Regex.IsMatch(string.Format("{0:00000}", input), @"\b\d{5}\b");
        }
    }
}
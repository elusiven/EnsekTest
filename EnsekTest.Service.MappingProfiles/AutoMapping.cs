using AutoMapper;
using E = EnsekTest.Data.Primitives.Entities;
using M = EnsekTest.Service.Primitives.Models;

namespace EnsekTest.Service.MappingProfiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<E.Account, M.AccountResource>().ReverseMap();
            CreateMap<E.MeterReading, M.MeterReadingResource>().ReverseMap();
        }
    }
}
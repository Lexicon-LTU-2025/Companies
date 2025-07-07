using AutoMapper;
using Bogus.DataSets;
using Companis.Shared;

namespace Companies.API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Company, CompanyDto>();
    }
}

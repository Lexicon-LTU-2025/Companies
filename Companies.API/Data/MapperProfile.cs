using AutoMapper;
using Companies.API.Entities;
using Companis.Shared;

namespace Companies.API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<Employee, EmployeeDto>();
    }
}

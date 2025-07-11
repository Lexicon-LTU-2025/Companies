﻿using AutoMapper;
using Companis.Shared;
using Domain.Models.Entities;

namespace Companies.Infractructure.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Address, opt =>
              opt.MapFrom(src =>
              $"{src.Address}{(string.IsNullOrEmpty(src.Country) ? string.Empty : ", ")}{src.Country}"));

        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();


        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeCreateDto, Employee>();
        CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Controllers;
using CarService.Entities;
using CarService.Models;
using CarService.Services;

namespace CarService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>();
            //.ForMember(m => m.City, c => c.MapFrom(s => s.Address.City) helpful code
            CreateMap<User, UserDto>();
            CreateMap<CreateCarDto, Car>();
            CreateMap<CreateUserDto, User>();
            CreateMap<CarMarket, CarMarketDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));
            CreateMap<CarPart, CarPartDto>();
            CreateMap<CreateCarPartDto, CarPart>();
            CreateMap<CreateCarMarketDto, CarMarket>()
                .ForMember(cm => cm.Address, ccm => ccm.MapFrom(dto => new CarMarketAddress()
                {
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street
                }));
            CreateMap<Repair, RepairDto>();
            CreateMap<UserUpdate, User>();
            CreateMap<CarUpdateDto, CarDto>();
        }
    }
}

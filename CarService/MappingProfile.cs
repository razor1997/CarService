using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarService.Entities;
using CarService.Models;

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
        }
    }
}

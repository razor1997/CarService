using AutoMapper;
using CarService.Controllers;
using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services
{
    public class CarPartService : ICarPartService
    {
        private readonly CarServiceDbContext _context;
        private readonly IMapper _mapper;

        public CarPartService(CarServiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int carMarketId, CreateCarPartDto dto)
        {
            var carMarket = _context.CarMarkets.FirstOrDefault(c => c.Id == carMarketId);
            if (carMarket is null)
            {
                throw new NotFoundException.NotFoundException("Car Market not found");
            }

            var carPart = _mapper.Map<CarPart>(dto);
            _context.CarParts.Add(carPart);
            _context.SaveChanges();

            return carPart.Id;
        }
    }
}

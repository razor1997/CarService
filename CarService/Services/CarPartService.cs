using AutoMapper;
using CarService.Controllers;
using CarService.Entities;
using CarService.Models;
using Microsoft.EntityFrameworkCore;
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
            var carMarket = GetCarMarketById(carMarketId);

            var carPart = _mapper.Map<CarPart>(dto);

            carPart.CarMarketId = carMarketId;
            _context.CarParts.Add(carPart);
            _context.SaveChanges();

            return carPart.Id;
        }
        public CarPartDto GetById(int carMarketId, int carPartId)
        {
            var carMarket = GetCarMarketById(carMarketId);

            var carPart = _context.CarParts.FirstOrDefault(cp => cp.Id == carPartId);
            if(carPart is null || carPart.CarMarketId != carMarketId)
            {
                throw new NotFoundException.NotFoundException("Car Part not found");
            }

            var carPartDto = _mapper.Map<CarPartDto>(carPart);
            return carPartDto;
        }
        public List<CarPartDto> GetAll(int carMarketId)
        {
            var carMarket = GetCarMarketById(carMarketId);
            var carPartsDtos = _mapper.Map<List<CarPartDto>>(carMarket.Parts);
            return carPartsDtos;
        }
        public void RemoveAll( int carMarketId)
        {
            var carMarket = GetCarMarketById(carMarketId);

            _context.RemoveRange(carMarket.Parts);
            _context.SaveChanges();
        }

        private CarMarket GetCarMarketById(int carMarketId)
        {
            var carMarket = _context
                .CarMarkets
                .Include(cm => cm.Parts)
                .FirstOrDefault(cm => cm.Id == carMarketId);
            if (carMarket is null) throw new NotFoundException.NotFoundException("Car Market not found");
            return carMarket;
        }
    }
}

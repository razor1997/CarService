using AutoMapper;
using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services
{
    public class CarMarketService : Controller, ICarMarketService
    {
        private readonly IMapper _mapper;
        private readonly CarServiceDbContext _dbContext;

        public CarMarketService(IMapper mapper, CarServiceDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public CarMarketDto GetById(int id)
        {
            var carMarket = _dbContext
                .CarMarkets
                .Include(c => c.Address)
                .Include(c => c.Parts)
                .FirstOrDefault(c => c.Id == id);
            if (carMarket is null)
            {
                return null;
            }
            var result = _mapper.Map<CarMarketDto>(carMarket);
            return result;
        }
        public IEnumerable<CarMarketDto> GetAll()
        {
            var carMarkets = _dbContext
                .CarMarkets
                .Include(c => c.Address)
                .Include(c => c.Parts)
                .ToList();
            var carMarketsDtos = _mapper.Map<List<CarMarketDto>>(carMarkets);
            return carMarketsDtos;
        }
        public int Create(CreateCarMarketDto dto)
        {
            var carMarket = _mapper.Map<CarMarket>(dto);
            _dbContext.CarMarkets.Add(carMarket);
            _dbContext.SaveChanges();
            return carMarket.Id;
        }
        public bool Delete(int id)
        {
            var carMarket = _dbContext
            .CarMarkets
            .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) return false;
            _dbContext.CarMarkets.Remove(carMarket);
            _dbContext.SaveChanges();

            return true;
        }
        public bool Update(int id, UpdateCarMarketDto dto)
        {
            var carMarket = _dbContext
                .CarMarkets
                .FirstOrDefault(c => c.Id == id);
            if(carMarket is null)
            {
                return false;
            }
            carMarket.Name = dto.Name;
            carMarket.Description = dto.Description;
            carMarket.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();

            return true;
        }
    }
}

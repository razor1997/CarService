using AutoMapper;
using CarService.Entities;
using CarService.Models;
using CarService.NotFoundException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CarMarketService> _logger;

        public CarMarketService(IMapper mapper, CarServiceDbContext dbContext, ILogger<CarMarketService> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }
        public CarMarketDto GetById(int id)
        {
            var carMarket = _dbContext
                .CarMarkets
                .Include(c => c.Address)
                .Include(c => c.Parts)
                .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");

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
        public void Delete(int id)
        {
            _logger.LogError($"Car Market with id:{ id} DELETE action invoked");
            var carMarket = _dbContext
            .CarMarkets
            .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");
            _dbContext.CarMarkets.Remove(carMarket);
            _dbContext.SaveChanges();
        }
        public void Update(int id, UpdateCarMarketDto dto)
        {
            var carMarket = _dbContext
                .CarMarkets
                .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");

            carMarket.Name = dto.Name;
            carMarket.Description = dto.Description;
            carMarket.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}

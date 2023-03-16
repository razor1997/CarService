using AutoMapper;
using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services
{
    public class CarService : ICarService
    {
        private readonly CarServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarService(CarServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public CarDto GetById(int id)
        {
            var car = _dbContext
            .Cars
            //.Include(c => c.AddSomething) temporary
            .FirstOrDefault(c => c.Id == id);

            if (car is null) return null;
            var result = _mapper.Map<CarDto>(car);
            return result;
        }

        public IEnumerable<CarDto> GetAll()
        {
            var cars = _dbContext
                .Cars
                //.Include(c => c.AddSomething) temporary
                .ToList();
            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return carsDtos;
        }

        public int Create(CreateCarDto dto)
        {
            var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return car.Id;
        }
        public bool Delete(int id)
        {
            var car = _dbContext
            .Cars
            .FirstOrDefault(c => c.Id == id);

            if (car is null) return false;
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();

            return true;
        }

        public void Update(int id, CarUpdateDto dto)
        {
            var car = _dbContext
                .Cars
                .FirstOrDefault(c => c.Id == id);

            if (car is null) throw new NotFoundException.NotFoundException("Car not found");

            car.Mark = dto.Mark;
            car.Model = dto.Model; 
            car.EngineType = _dbContext.EngineTypes.
                                FirstOrDefault(
                                et => et.Id == dto.EngineType);
            car.BuyCost = dto.BuyCost;
            car.Capacity = dto.Capacity;
            car.YearProduction = dto.YearProduction;
            car.Body = dto.Body;
            car.Mileage = dto.Mileage;

            _dbContext.SaveChanges();
        }
    }
}

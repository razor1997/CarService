using AutoMapper;
using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly CarServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarController(CarServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {
            var cars = _dbContext
                .Cars
                //.Include(c => c.AddSomething) temporary
                .ToList();
            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return Ok(carsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult <CarDto> Get([FromRoute] int id)
        {
            var car = _dbContext
                .Cars
                //.Include(c => c.AddSomething) temporary
                .FirstOrDefault(c => c.Id == id);
            if(car is null)
            {
                return NotFound();
            }
            var carDto = _mapper.Map<CarDto>(car);
            return Ok(car); 
        }
        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {
            var car = _mapper.Map<Car>(dto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return Created($"/api/car/{car.Id}", null);
        }
    }

}

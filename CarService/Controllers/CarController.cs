using AutoMapper;
using CarService.Models;
using CarService.Services;
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
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {

            var cars = _carService.GetAll();
            return Ok(cars);
        }
        [HttpGet("{id}")]
        public ActionResult <CarDto> Get([FromRoute] int id)
        {
            var car = _carService.GetById(id);   

            if(car is null)
            {
                return NotFound();
            }

            return Ok(car); 
        }
        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _carService.Create(dto);
            return Created($"/api/car/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted =_carService.Delete(id);

            if(isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }

}

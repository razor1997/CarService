using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    public class CarMarketController : Controller
    {
        private readonly ICarMarketService _carMarketService;

        public CarMarketController(ICarMarketService carMarketService)
        {
            _carMarketService = carMarketService;
        }
        [HttpGet("{id}")]
        public ActionResult<CarMarketDto> Get([FromRoute] int id)
        {
            var car = _carMarketService.GetById(id);

            if (car is null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarMarketDto>> GetAll()
        {
            var carMarketsDtos = _carMarketService.GetAll();
            return Ok(carMarketsDtos);
        }
        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarMarketDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _carMarketService.Create(dto);
            return Created($"/api/car/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _carMarketService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateCarMarketDto dto, [FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = _carMarketService.Update(id, dto);

            if(!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

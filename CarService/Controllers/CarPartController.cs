using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("carservice/carmarket/{carMarketId}/carPart")]
    [ApiController]
    public class CarPartController : ControllerBase
    {
        private readonly ICarPartService _carPartService;

        public CarPartController(ICarPartService carPartService)
        {
            _carPartService = carPartService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int carMarketId,[FromBody] CreateCarPartDto dto)
        {
            var newCarPartId = _carPartService.Create(carMarketId, dto);

            return Created($"carservice/carservice/{carMarketId}/carpart/{newCarPartId}", null);
        }
        [HttpGet("{carPartId}")]
        public ActionResult<CarPartDto> Get([FromRoute] int carMarketId, [FromRoute] int carPartId)
        {
            CarPartDto carPart = _carPartService.GetById(carMarketId, carPartId);
            return carPart;
        }
        [HttpGet]
        public ActionResult<List<CarPartDto>> Get([FromRoute] int carMarketId)
        {
            var result = _carPartService.GetAll(carMarketId);
            return result;
        }
        [HttpDelete]
        public ActionResult Delete([FromRoute]int carMarketId)
        {
            _carPartService.RemoveAll(carMarketId);
            return NoContent();
        }

    }
}

using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("api/carmarket/{carMarketId}/carPart")]
    [ApiController]
    public class CarPartController : ControllerBase
    {
        private readonly ICarPartService _carPartService;

        public CarPartController(ICarPartService carPartService)
        {
            _carPartService = carPartService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute]int carPartId, CreateCarPartDto dto)
        {
            var newCarPartId = _carPartService.Create(carPartId, dto);

            return Created($"carservice/{carPartId}/carpart/{newCarPartId}", null);
        }

    }
}

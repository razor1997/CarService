using CarService.Entities;
using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("carservice/carmarket")]
    [ApiController]
    [Authorize]
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

            return Ok(car);
        }
        [HttpGet]
        [Authorize(Policy = "CreatedAtLeast2CarMarkets")]// [AllowAnonymous]////allow to get without token
        public ActionResult<IEnumerable<CarMarketDto>> GetAll()
        {
            var carMarketsDtos = _carMarketService.GetAll();
            return Ok(carMarketsDtos);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
      
        public ActionResult Create([FromBody] CreateCarMarketDto dto)
        {
            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var id = _carMarketService.Create(dto);
            return Created($"/CarService/CarMarket/{id}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _carMarketService.Delete(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateCarMarketDto dto, [FromRoute] int id)
        {
            _carMarketService.Update(id, dto);

            return Ok();
        }
        [HttpGet("{PhoneNumber}")]
        [Authorize(Policy = "HasPhoneNumber")]
        public ActionResult<CarMarketDto> GetByPhoneNumber([FromRoute] string phoneNumber)
        {
            var carMarket = _carMarketService.GetById(0); //to change

            return Ok(carMarket);
        }
    }
}

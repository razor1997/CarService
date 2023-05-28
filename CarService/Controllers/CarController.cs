using AutoMapper;
using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CarService.Entities;

namespace CarService.Controllers
{
    [Route("carservice/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CarController(ICarService carService, IHttpContextAccessor httpContextAccessor, IPhotoService photoService,
             IUserService userService, IMapper mapper)
        {
            _carService = carService;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAll()
        {

            var cars = _carService.GetAll();
            return Ok(cars);
        }
        [HttpGet("{id}")]
        public ActionResult<CarDto> Get([FromRoute] int id)
        {
            var car = _carService.GetById(id);

            if (car is null)
            {
                return NotFound();
            }

            return Ok(car);
        }
        [HttpPost]
        public ActionResult CreateCar([FromBody] CreateCarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Car car = _carService.Create(dto);
            return Created($"/carservice/cars/{car.Id}", car);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _carService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CarUpdateDto model)
        {
            _carService.Update(id, model);

            return Ok();
        }
        [HttpPost("add-photo/{carId}")]
        public async Task<ActionResult> AddPhoto([FromRoute] int carId, [FromForm] IFormFile[] file)
        {
            try
            {
                var files = Request.Form.Files;
                // Get the claims values
                var name = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = await _userService.GetUserByUsernameAsync(name);
                if (user == null) return BadRequest("User not found");
                foreach (var temp in files)
                {
                    var result = await _photoService.AddPhotoAsync(temp);
                    var photo = new Photo
                    {
                        Url = result.SecureUrl.AbsoluteUri,
                        PublicId = result.PublicId,
                        UserId = user.Id,
                        IsMain = true,
                        CarId = carId
                    };
                    user.Photos.Add(photo);
                    if (await _userService.SaveAllAsync())
                    {
                        return CreatedAtRoute("GetUser", new { username = user.Name }, _mapper.Map<PhotoDto>(photo));
                    }
                }
                return BadRequest("Problems");
            }
            catch
            {
                return BadRequest("Failed to upload photo");
            }

        }
    }

}

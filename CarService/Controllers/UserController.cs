using AutoMapper;
using CarService.Entities;
using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("carService/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly CarServiceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
       
        public UserController(CarServiceDbContext dbContext, IMapper mapper, IUserService userService, IPhotoService photoService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _photoService = photoService;
        }
        [HttpPost]
        public ActionResult CreateUser(CreateUserDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(dto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Created($"/api/user/{user.Id}", null);

        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        [HttpGet("{email}")]
        public ActionResult<User> GetByEmail([FromRoute] string email)
        {
            var user = _userService.GetByEmail(email);

            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserUpdate member)
        {
            _userService.Update(member.Id, member);
            if (await _userService.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update");
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult> AddPhoto([FromForm]IFormFile[] file)
        {
            var files = Request.Form.Files;
            //var user = await _userService.GetUserByUsernameAsync(User.GetUsername());
            //if(files. > 0)
            foreach(var temp in files)
            {
                var result = await _photoService.AddPhotoAsync(temp);
                var photo = new Photo
                {
                    Owner = OwnerType.otPerson,
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId
                };

                if (await _userService.SaveAllAsync())
                {
                    //return CreatedAtRoute("GetUser", _mapper.Map<PhotoDto>(photo)); //new { username = user.UserName },
                }
            }
            return BadRequest("Problems");
        }
    }
}

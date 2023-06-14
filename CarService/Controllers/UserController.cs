using AutoMapper;
using CarService.Entities;
using CarService.Extensions;
using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        public UserController(CarServiceDbContext dbContext, IMapper mapper, IUserService userService, IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
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
                        CarId = 0
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

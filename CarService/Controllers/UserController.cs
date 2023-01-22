using AutoMapper;
using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    public class UserController : Controller
    {
        private readonly CarServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(CarServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult CreateUser(CreateUserDto dto)
        {
            if(dto.Name.Length > 25)
            {
                return BadRequest("");
            }

            var user = _mapper.Map<User>(dto);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Created($"/api/user/{user.Id}", null);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

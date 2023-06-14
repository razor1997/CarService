using CarService.Models;
using CarService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Controllers
{
    [Route("carservice/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            if (dto.Email is null || dto.Password is null)
            {
                return Unauthorized("Invalid username or passwordDUPA");
            }
            if(!_accountService.ValidateLoginUser(dto.Email, dto.Password))
            {
                return Unauthorized("Invalid username or passwordDUPA");
            }
            dto.Token = _accountService.GenerateJwt(dto);

            return Ok(dto);
        }
    }
}

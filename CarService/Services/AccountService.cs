using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.NotFoundException;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Services
{
    public class AccountService : ControllerBase, IAccountService
    {
        private readonly CarServiceDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(CarServiceDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId,
                Name = dto.Name
            };
            var hashPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public string GenerateJwt(LoginDto dto) 
        {
            var user = _context.Users
                .Include(u => u.Role)
                .Include(p => p.Photos)            
                .FirstOrDefault(u => u.Email == dto.Email)
                ;
            
            if(user is null || dto.Password is null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.SurName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")

                )

            };

            if(!string.IsNullOrEmpty(user.ContactNumber))
            {
                claims.Add(
                     new Claim(ClaimTypes.MobilePhone, user.ContactNumber));
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
            }
            // Set current principal
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtKeyExpiredDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return tokenHandler.WriteToken(token);
        }
        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}

using AutoMapper;
using CarService.Entities;
using CarService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Services
{
    public class UserService : Controller, IUserService
    {
        private readonly CarServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(CarServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<User> GetAll()
        {
            var users = _dbContext.Users.ToList();

            return users;
        }
        public UserDto GetByEmail(string emailAddress)
        {
            var user = _dbContext.Users
                .Where(u => u.Email == emailAddress)
                .Include(p =>p.Photos)
                .FirstOrDefault();
            if (user is null) throw new NotFoundException.NotFoundException("User not found");
            var userDto = _mapper.Map<UserDto>(user);
            userDto.PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url;
            return userDto;
        }
        public void Update(int id, UserUpdate dto)
        {
            var user = _dbContext.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            if (user is null) throw new NotFoundException.NotFoundException("User not found");
            var result = _mapper.Map(dto, user);

            _dbContext.Users.Update(result);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task <User> GetUserByUsernameAsync(String username)
        {
            return await _dbContext.Users
                .Where(u => u.Name == username)
                .Include(p => p.Photos)
                .SingleOrDefaultAsync();
        }
    }
}
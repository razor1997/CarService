using CarService.Entities;
using CarService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarService.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetByEmail(string emailAddress);
        void Update(int id, UserUpdate dto);
        Task<bool> SaveAllAsync();
    }
}
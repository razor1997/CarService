using CarService.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarService.Services
{
    public interface ICarMarketService
    {
        int Create(CreateCarMarketDto dto);
        IEnumerable<CarMarketDto> GetAll();
        CarMarketDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateCarMarketDto dto);
    }
}
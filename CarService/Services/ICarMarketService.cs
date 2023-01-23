using CarService.Models;
using System.Collections.Generic;

namespace CarService.Services
{
    public interface ICarMarketService
    {
        int Create(CreateCarMarketDto dto);
        IEnumerable<CarMarketDto> GetAll();
        CarMarketDto GetById(int id);
        bool Delete(int id);
        bool Update(int id, UpdateCarMarketDto dto);
    }
}
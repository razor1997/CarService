using CarService.Entities;
using CarService.Models;
using System.Collections.Generic;

namespace CarService.Services
{
    public interface ICarService
    {
        Car Create(CreateCarDto dto);
        IEnumerable<CarDto> GetAll();
        CarDto GetById(int id);
        bool Delete(int id);
        void Update(int id, CarUpdateDto dto);

    }
}
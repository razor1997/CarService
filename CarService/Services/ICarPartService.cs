using CarService.Controllers;
using CarService.Models;
using System.Collections.Generic;

namespace CarService.Services
{
    public interface ICarPartService
    {
        int Create(int carMarketId, CreateCarPartDto dto);
        CarPartDto GetById(int carMarketId, int carPartId);
        List<CarPartDto> GetAll(int carMarketId);
        public void RemoveAll(int carMarketId);
    }
}
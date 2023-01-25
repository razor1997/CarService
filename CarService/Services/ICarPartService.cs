using CarService.Controllers;

namespace CarService.Services
{
    public interface ICarPartService
    {
        int Create(int carMarketId, CreateCarPartDto dto);
    }
}
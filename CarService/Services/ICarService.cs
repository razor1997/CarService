﻿using CarService.Models;
using System.Collections.Generic;

namespace CarService.Services
{
    public interface ICarService
    {
        int Create(CreateCarDto dto);
        IEnumerable<CarDto> GetAll();
        CarDto GetById(int id);
        bool Delete(int id);
    }
}
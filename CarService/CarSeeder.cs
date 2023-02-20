﻿using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService
{
    public class CarSeeder
    {
        private readonly CarServiceDbContext _dbContext;
        public CarSeeder(CarServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Cars.Any())
                {
                    var cars = GetCars();
                    _dbContext.Cars.AddRange(cars);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.EngineTypes.Any())
                {
                    var engineTypes = GetEngineTypes();
                    _dbContext.EngineTypes.AddRange(engineTypes);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Administrator"
                },
                new Role()
                {
                    Name = "Manager"
                }
            };
            return roles;
        }
        private IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>()
            {
                new Car
                {
                    Mark = "BMW",
                    Model = "Series 5",
                    Body = (int)CarBody.cbCombi
                },
                new Car
                {
                    Mark = "Mercedes-Benz",
                    Model = "C - Class",
                    Body = (int)CarBody.cbSedan,
                    Repairs = new List<Repair>
                    {
                        new Repair
                        {
                             Cost = 500,
                             Description = "Engine Broke"
                        },
                        new Repair
                        {
                             Cost = 200,
                             Description = "Wheels"
                        },
                    }
                }
            };
            return cars;
        }
        private IEnumerable<EngineType> GetEngineTypes()
        {
            var engineTypes = new List<EngineType>()
            {
                new EngineType
                {
                    Name = "Petrol"
                },
                new EngineType
                {
                    Name = "Diesel"
                },
                new EngineType
                {
                    Name = "LPG"
                },
                new EngineType
                {
                    Name = "Electric"
                }
            };
            return engineTypes;
        }
    }
}

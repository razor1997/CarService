using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class CarUpdateDto
    { 
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Body { get; set; }
        public int Capacity { get; set; }
        public int Mileage { get; set; }
        public int BuyCost { get; set; }
        public int YearProduction { get; set; }
        public int EngineTypeId { get; set; }
    }
}

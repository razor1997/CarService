using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }
        public int Type { get; set; }
        public int Mileage { get; set; }
        public int BuyCost { get; set; }
        public int YearProduction { get; set; }
        public int EngineCapacity { get; set; }
        public int EngineTypeId { get; set; }
        public EngineType EngineType { get; set; }
        public virtual List<RepairDto> Parts { get; set; }

    }
}

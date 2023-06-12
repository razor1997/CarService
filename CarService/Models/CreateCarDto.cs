using CarService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class CreateCarDto
    {
        public string Model { get; set; }
        public string Mark { get; set; }
        public int YearProduction { get; set; }
        public int Capacity { get; set; }
        public int EngineType { get; set; }
        public int Body { get; set; }
        public int Type { get; set; }
    }
}

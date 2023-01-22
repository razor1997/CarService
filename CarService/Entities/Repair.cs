using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class Repair
    {
        public int Id { get; set; }
        public int IdCar { get; set; }
        public string Description { get; set; } 
        public int Cost { get; set; }
    }
}

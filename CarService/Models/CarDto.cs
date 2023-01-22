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
    }
}

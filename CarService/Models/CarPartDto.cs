using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class CarPartDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}

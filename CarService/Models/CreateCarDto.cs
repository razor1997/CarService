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
        public int Type { get; set; }
    }
}

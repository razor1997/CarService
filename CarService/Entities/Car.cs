using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Mark { get; set; }    
        public int Type { get; set; }
        public virtual List<Repair> Repairs { get; set; }
    }
}

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
        public int Body { get; set; }
        public int Capacity { get; set; }
        public int Mileage { get; set; }
        public int BuyCost { get; set; }
        public int YearProduction { get; set; }
        public virtual User User { get; set; }
        public virtual EngineType EngineType { get; set; }
        public virtual List<Repair> Repairs { get; set; }
        public virtual List <Photo> Photos{ get; set; }

    }
}

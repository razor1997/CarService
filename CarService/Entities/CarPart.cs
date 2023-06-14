using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class CarPart
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int CarMarketId { get; set; } 
        public virtual CarMarket CarMarket { get; set; }
    }
}

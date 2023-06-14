using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class CarMarket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public int AddressId { get; set; }
        public int? OwnerId { get; set; }
        public User Owner { get; set; }
        public virtual CarMarketAddress Address{ get; set;}
        public virtual List<CarPart> Parts { get; set; } 

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        [MaxLength(50)]
        public string SurName { get; set; }
        public virtual List <Car> Cars { get; set; }
        public string About { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}

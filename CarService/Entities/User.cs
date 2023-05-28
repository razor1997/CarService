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
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set;     }
        public virtual List <Car> Cars { get; set; }
        public virtual List<Photo> Photos{ get; set; }
        public string? About { get; set; }
        public string ContactNumber { get; set; }   
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string SurName { get; set; }
        public string About { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}

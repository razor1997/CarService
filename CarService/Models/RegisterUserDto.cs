using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ConfirmPassword { get; set; } 
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;

    }
}

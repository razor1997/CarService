using System.ComponentModel.DataAnnotations;

namespace CarService.Controllers
{
    public class CreateCarPartDto
    {
        [Required]
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CarMarketId { get; set; }
    }
}       
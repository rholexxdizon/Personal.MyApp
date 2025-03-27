using System.ComponentModel.DataAnnotations;

namespace Personal.MyApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
    }
}

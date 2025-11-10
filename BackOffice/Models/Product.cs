using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("ProductName")]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Qty { get; set; }

    }
}

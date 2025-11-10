using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class CategorieProduit
    {
        
        [Key]
        public int IdCategorieProduit { get; set; }

        [Required]
        [StringLength(100)]
        public string? Categorie { get; set; }
    }
}

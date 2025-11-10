using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class Produit
    {
        [Key]
        public int IdProduit { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nom { get; set; }

        [ForeignKey(nameof(CategorieProduit))]
        public int? IdCategorieProduit { get; set; }
        public CategorieProduit? CategorieProduit { get; set; }

        [ForeignKey(nameof(Method))]
        public int? IdMethod { get; set; }
        public Method? Method { get; set; }
    }
}

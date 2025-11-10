using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class Mouvement
    {
        [Key]
        public int IdMouvement { get; set; }

        [ForeignKey(nameof(Produit))]
        public int IdProduit { get; set; }
        public Produit Produit { get; set; }

        [StringLength(90)]
        public string Libelle { get; set; }

        public DateTime Daty { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Action))]
        public int IdAction { get; set; }
        public Action Action { get; set; }

        public double Qt { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2} MGA")]
        public double Pu { get; set; }
        //public double Montant { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2} MGA")]
        //public double Montant => Qt * Pu ;
        public double Montant { get; set; }
        [NotMapped]
        public Queue<(double QTE, double PU)>? currentStock { get; set; }
    }
}

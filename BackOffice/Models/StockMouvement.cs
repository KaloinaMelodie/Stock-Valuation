namespace BackOffice.Models
{
    public class StockMovement
    {
        public DateTime Date { get; set; }
        public string Produit { get; set; }
        public string Libelle { get; set; }
        public string Action { get; set; } // "Entree" ou "Sortie"
        public int Quantite { get; set; }
        public double PrixUnitaire { get; set; }
        public double Montant => Quantite * PrixUnitaire;

        public Queue<(double QTE, double PU)>? currentStock {  get; set; }
    }

}

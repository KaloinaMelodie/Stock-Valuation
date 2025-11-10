namespace BackOffice.Models
{
    public class MouvementCsv
    {
        public int IdProduit { get; set; }
        public string Libelle { get; set; }
        public DateTime Daty { get; set; }
        public int IdAction { get; set; }
        public double Qt { get; set; }
        public double Pu { get; set; }
        public double Montant { get; set; }
    }

}

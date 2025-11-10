using BackOffice.Models;

namespace BackOffice.Managers
{
    public class SortieManager
    {
        public List<Mouvement> Movements { get; set; } = new();

        public List<(double QTE, double PU, double Montant)> SortieFIFO(double sortie)
        {
            var result = new List<(double QTE, double PU, double Montant)>();
            var lastMovement = Movements.LastOrDefault();
            if (Movements == null || !Movements.Any() || Movements.Count() == 0)
            {
                throw new Exception("La quantite restant du stock de ce produit est 0");
            }
            if (lastMovement != null)
            {
                var stock = new Queue<(double QTE, double PU)>(lastMovement.currentStock);
                double quantiteTotal = 0;
                double quantityToRemove = sortie;
                while (quantityToRemove > 0 && stock.Any())
                {
                    var (qte, pu) = stock.Peek();
                    quantiteTotal += qte;
                    if (qte <= quantityToRemove)
                    {
                        result.Add((qte, pu, qte * pu));
                        quantityToRemove = quantityToRemove - (int)qte;
                        stock.Dequeue();
                    }
                    else
                    {
                        result.Add((quantityToRemove, pu, quantityToRemove * pu));
                        stock.Dequeue();
                        stock.Enqueue((qte - quantityToRemove, pu));
                        quantityToRemove = 0;
                    }
                }
                if (quantityToRemove != 0 && !stock.Any())
                {
                    throw new Exception("La quantite restant du stock de ce produit est " + quantiteTotal);
                }

            }
            return result;
        }
        public List<(double QTE, double PU, double Montant)> SortieLIFO(double sortie)
        {
            var result = new List<(double QTE, double PU, double Montant)>();
            var lastMovement = Movements.LastOrDefault();
            if (Movements == null || !Movements.Any() || Movements.Count() == 0)
            {
                throw new Exception("La quantite restant du stock de ce produit est 0");
            }
            if (lastMovement != null)
            {
                var stock = new Queue<(double QTE, double PU)>(lastMovement.currentStock);
                double quantiteTotal = 0;
                double quantityToRemove = sortie;
                while (quantityToRemove > 0 && stock.Any())
                {
                    var (qte, pu) = stock.Peek();
                    quantiteTotal += qte;
                    if (qte <= quantityToRemove)
                    {
                        result.Add((qte, pu, qte * pu));
                        quantityToRemove = quantityToRemove - (int)qte;
                        stock.Dequeue();
                    }
                    else
                    {
                        result.Add((quantityToRemove, pu, quantityToRemove * pu));
                        stock.Dequeue();
                        stock.Enqueue((qte - quantityToRemove, pu));
                        quantityToRemove = 0;
                    }
                }
                if (quantityToRemove != 0 && !stock.Any())
                {
                    throw new Exception("La quantite restant du stock de ce produit est " + quantiteTotal);
                }

            }
            return result;
        }
        public List<(double QTE, double PU, double Montant)> SortieCMUP(double sortie)
        {
            var result = new List<(double QTE, double PU, double Montant)>();
            var lastMovement = Movements.LastOrDefault();
            if (Movements == null || !Movements.Any() || Movements.Count() == 0)
            {
                throw new Exception("La quantite restant du stock de ce produit est 0");
            }
            if (lastMovement != null)
            {
                var stock = new Queue<(double QTE, double PU)>(lastMovement.currentStock);
                double quantiteTotal = 0;
                double quantityToRemove = sortie;
                while (quantityToRemove > 0 && stock.Any())
                {
                    var (qte, pu) = stock.Peek();
                    quantiteTotal += qte;
                    if (qte <= quantityToRemove)
                    {
                        result.Add((qte, pu, qte * pu));
                        quantityToRemove = quantityToRemove - (int)qte;
                        stock.Dequeue();
                    }
                    else
                    {
                        result.Add((quantityToRemove, pu, quantityToRemove * pu));
                        stock.Dequeue();
                        stock.Enqueue((qte - quantityToRemove, pu));
                        quantityToRemove = 0;
                    }
                }
                if (quantityToRemove != 0 && !stock.Any())
                {
                    throw new Exception("La quantite restant du stock de ce produit est " + quantiteTotal);
                }

            }
            return result;
        }
    }
}

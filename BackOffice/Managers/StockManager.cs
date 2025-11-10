using BackOffice.Models;

namespace BackOffice.Managers
{
    public class StockManager
    {
        public List<Mouvement> Movements { get; set; } = new();

        public List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)> GetStockStateFIFO()
        {
            var stock = new Queue<(double QTE, double PU)>();
            var result = new List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)>();

            foreach (var movement in Movements)
            {
                if (movement.Action.ActionName == "Entree")
                {
                    stock.Enqueue((movement.Qt, movement.Pu));
                    movement.currentStock = new Queue<(double QTE, double PU)>(stock);

                }
                else if (movement.Action.ActionName == "Sortie")
                {
                    double quantityToRemove = movement.Qt;
                    while (quantityToRemove > 0 && stock.Any())
                    {
                        var (qte, pu) = stock.Peek();
                        if (qte <= quantityToRemove)
                        {

                            //result.Add((movement.Daty, movement.Libelle, qte, pu, qte * pu));
                            quantityToRemove = quantityToRemove - (int)qte;
                            stock.Dequeue();
                            movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                        }
                        else
                        {
                            //result.Add((movement.Daty, movement.Libelle, quantityToRemove, pu, quantityToRemove * pu));
                            stock.Dequeue();
                            stock.Enqueue((qte - quantityToRemove, pu));
                            quantityToRemove = 0;
                            movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                        }
                    }
                }
            }

            return result;
        }

        public List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)> GetStockStateLIFO()
        {
            var stock = new Stack<(double QTE, double PU)>();
            var result = new List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)>();

            foreach (var movement in Movements)
            {
                if (movement.Action.ActionName == "Entree")
                {
                    stock.Push((movement.Qt, movement.Pu));
                    movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                }
                else if (movement.Action.ActionName == "Sortie")
                {
                    double quantityToRemove = movement.Qt;
                    while (quantityToRemove > 0 && stock.Any())
                    {
                        var (qte, pu) = stock.Peek();
                        if (qte <= quantityToRemove)
                        {
                            //result.Add((movement.Daty, movement.Libelle, qte, pu, qte * pu));
                            quantityToRemove -= (int)qte;
                            stock.Pop();
                            movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                        }
                        else
                        {
                            //result.Add((movement.Daty, movement.Libelle, quantityToRemove, pu, quantityToRemove * pu));
                            stock.Pop();
                            stock.Push((qte - quantityToRemove, pu));
                            quantityToRemove = 0;
                            movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                        }
                    }
                }
            }

            return result;

        }

        public List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)> GetStockStateCMUP()
        {
            var stock = new Stack<(double QTE, double PU)>();
            var result = new List<(DateTime Date, string Libelle, double QTE, double PU, double Montant)>();

            foreach (var movement in Movements)
            {
                if (movement.Action.ActionName == "Entree")
                {
                    (double QTE, double PU) taloha = (0, 0);
                    if (stock.Any())
                    {

                        taloha = stock.Pop();
                    }
                    stock.Push((movement.Qt + taloha.QTE,
                       (movement.Pu * movement.Qt + taloha.QTE * taloha.PU) / (movement.Qt + taloha.QTE)));
                    movement.currentStock = new Queue<(double QTE, double PU)>(stock);
                }
                else if (movement.Action.ActionName == "Sortie")
                {
                    double quantityToRemove = movement.Qt;

                    //result.Add((movement.Date, movement.Libelle, quantityToRemove, pu, quantityToRemove * pu));                    
                    var taloha = stock.Pop();
                    if (taloha.QTE - quantityToRemove > 0)
                    {
                        stock.Push((taloha.QTE - quantityToRemove, movement.Pu));
                    }
                    quantityToRemove = 0;
                    movement.currentStock = new Queue<(double QTE, double PU)>(stock);

                }
            }

            return result;
        }
    }

}

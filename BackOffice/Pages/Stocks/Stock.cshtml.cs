using BackOffice.Migrations;
using System.Globalization;
using BackOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackOffice.Managers;
using Rotativa.AspNetCore;

namespace BackOffice.Pages.Stocks
{
    public class StockModel : PageModel
    {
        public List<StockMovement> Movements { get; set; }
        public List<Mouvement> Mouvements { get; set; }
        private readonly BackOffice.Data.AppDBContext _context;

        public StockModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BackOffice.Models.Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? idproduit)
        {
            var strproduit = "Cartouches jet d encre";

            if (idproduit == null)
            {
                return NotFound();
            }
            var produit = await _context.Produit.Include(m => m.Method).FirstOrDefaultAsync(m => m.IdProduit == idproduit);
            if (produit == null)
            {
                return NotFound();
            }
            Produit = produit;
            Mouvements =  _context.Mouvement.Include(m => m.Produit)
    .Include(m => m.Action).Where(m => m.IdProduit == idproduit).ToList();

            // Exemple de mouvements
            Movements = new List<StockMovement>
        {
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "Stock Initial", Action = "Entree", Quantite = 20, PrixUnitaire = 30 },
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "BS 265", Action = "Sortie", Quantite = 13, PrixUnitaire = 30 },
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "BE 35", Action = "Entree", Quantite = 20, PrixUnitaire = 32 },
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "BS 310", Action = "Sortie", Quantite = 7, PrixUnitaire = 30 },
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "BS 310", Action = "Sortie", Quantite = 8, PrixUnitaire = 32 },
            //new StockMovement { Date = DateTime.Now, Produit = produit, Libelle = "BE 44", Action = "Entree", Quantite = 10, PrixUnitaire = 31 },

            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "Stock Initial", Action = "Entree", Quantite = 20, PrixUnitaire = 30 },
            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "BS 265", Action = "Sortie", Quantite = 13, PrixUnitaire = 30 },
            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "BE 35", Action = "Entree", Quantite = 20, PrixUnitaire = 32 },
            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "BS 310", Action = "Sortie", Quantite = 15, PrixUnitaire = 31.48 },
            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "BE 31", Action = "Entree", Quantite = 10, PrixUnitaire = 31 },
            new StockMovement { Date = DateTime.Now, Produit = strproduit, Libelle = "BS 32", Action = "Sortie", Quantite = 8, PrixUnitaire = 31.26 },
        };

            var manager = new StockManager { Movements = Mouvements };
            if(produit.Method.MethodName == "FIFO")
            {
            manager.GetStockStateFIFO();            
            }else if (produit.Method.MethodName == "LIFO")
            {
             manager.GetStockStateLIFO();
            }
            else if (Produit.Method.MethodName == "CMUP")
            {
             manager.GetStockStateCMUP();
            }
            return Page();
        }
        public async Task<RazorPageAsPdf> OnGetExportPdf(int? idproduit)
        { 

            //if (idproduit == null)
            //{
            //    return NotFound();
            //}
            var produit = await _context.Produit.Include(m => m.Method).FirstOrDefaultAsync(m => m.IdProduit == idproduit);
            //if (produit == null)
            //{
            //    return NotFound();
            //}
            Produit = produit;
            Mouvements = _context.Mouvement.Include(m => m.Produit)
            .Include(m => m.Action).Where(m => m.IdProduit == idproduit).ToList();

          

            var manager = new StockManager { Movements = Mouvements };
            if (produit.Method.MethodName == "FIFO")
            {
                manager.GetStockStateFIFO();
            }
            else if (produit.Method.MethodName == "LIFO")
            {
                manager.GetStockStateLIFO();
            }
            else if (Produit.Method.MethodName == "CMUP")
            {
                manager.GetStockStateCMUP();
            }

            var pdf = new RazorPageAsPdf(this)
            {
                FileName = "FicheDeStock.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
            return pdf;
        }

    }

}

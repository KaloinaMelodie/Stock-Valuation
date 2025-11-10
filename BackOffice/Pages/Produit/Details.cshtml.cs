using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data; 
using BackOffice.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BackOffice.Managers;

namespace BackOffice.Pages.Produit 
{
    public class DetailsModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public DetailsModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        public BackOffice.Models.Produit Produit { get; set; } = default!;

        [BindProperty]
        public Mouvement Mouvement { get; set; } = default!;

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ErrorMessage = null;
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.Include(m => m.Method).Include(m => m.CategorieProduit).FirstOrDefaultAsync(m => m.IdProduit == id);

            if (produit is not null)
            {
                Produit = produit;
                ViewData["IdAction"] = new SelectList(_context.Action, "IdAction", "ActionName");
               
                return Page();
            }
           
            return NotFound();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var produit = await _context.Produit.Include(m => m.Method).Include(m => m.CategorieProduit).FirstOrDefaultAsync(m => m.IdProduit == Mouvement.IdProduit);

            if (produit is not null)
            {
                Produit = produit;
                ViewData["IdAction"] = new SelectList(_context.Action, "IdAction", "ActionName");
            }
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            if (Mouvement.IdAction == 1)
            {
                _context.Mouvement.Add(Mouvement);
                await _context.SaveChangesAsync();
            }
            if (Mouvement.IdAction == 2)
            {
                var mouvements = _context.Mouvement.Include(m => m.Produit)
                .Include(m => m.Action).Where(m => m.IdProduit == Produit.IdProduit).ToList();
                var stockmanager = new StockManager { Movements = mouvements };
                if (Produit.Method.MethodName == "FIFO")
                {
                    stockmanager.GetStockStateFIFO();
                }
                else if (Produit.Method.MethodName == "LIFO")
                {
                    stockmanager.GetStockStateLIFO();
                }
                else if (Produit.Method.MethodName == "CMUP")
                {
                    stockmanager.GetStockStateCMUP();
                }
                var manager = new SortieManager { Movements = mouvements };
                List<(double QTE, double PU, double Montant)> sorties = null;
                if (Produit.Method.MethodName == "FIFO")
                {
                    try
                    {
                        sorties = manager.SortieFIFO(Mouvement.Qt);
                    }
                    catch (Exception ex) {
                        ErrorMessage = ex.Message;
                        return Page();
                    }                 
                    
                }
                if (Produit.Method.MethodName == "LIFO")
                {
                    try
                    {
                        sorties = manager.SortieLIFO(Mouvement.Qt);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        return Page();
                    }
                }
                if (Produit.Method.MethodName == "CMUP")
                {
                    try
                    {
                        sorties = manager.SortieCMUP(Mouvement.Qt);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;
                        return Page();
                    }
                }
                if(sorties != null)
                {
                    foreach ((double QTE, double PU, double Montant)  mivoka in sorties)
                    {
                        var entity = new Mouvement
                        {
                            IdProduit = Mouvement.IdProduit,
                            Libelle = Mouvement.Libelle,
                            Daty = Mouvement.Daty,
                            IdAction = Mouvement.IdAction,
                            Qt = mivoka.QTE,
                            Pu = mivoka.PU,
                            Montant = mivoka.Montant
                        };
                        _context.Mouvement.Add(entity);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            //loop sortie 
            // create mouvement set libelle, insert

            //_context.Mouvement.Add(Mouvement);
            //await _context.SaveChangesAsync();
            //return Page();
            return RedirectToPage("/Mouvements/Index");
        }
    }
}

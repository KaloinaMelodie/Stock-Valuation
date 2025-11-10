using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data;
using BackOffice.Models;

namespace BackOffice.Pages.Produit
{
    public class DeleteModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public DeleteModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BackOffice.Models.Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.FirstOrDefaultAsync(m => m.IdProduit == id);

            if (produit is not null)
            {
                Produit = produit;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO
            // if the produit is already used in mouvement, cannot delete 
            var produit = await _context.Produit.FindAsync(id);
            if (produit != null)
            {
                Produit = produit;
                _context.Produit.Remove(Produit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

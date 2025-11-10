using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data;
using BackOffice.Models;

namespace BackOffice.Pages.Produit
{
    public class EditModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public EditModel(BackOffice.Data.AppDBContext context)
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

            var produit =  await _context.Produit.FirstOrDefaultAsync(m => m.IdProduit == id);
            if (produit == null)
            {
                return NotFound();
            }
            Produit = produit;
           ViewData["IdCategorieProduit"] = new SelectList(_context.Set<CategorieProduit>(), "IdCategorieProduit", "Categorie");
           ViewData["IdMethod"] = new SelectList(_context.Set<Method>(), "IdMethod", "MethodName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Produit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitExists(Produit.IdProduit))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProduitExists(int id)
        {
            return _context.Produit.Any(e => e.IdProduit == id);
        }
    }
}

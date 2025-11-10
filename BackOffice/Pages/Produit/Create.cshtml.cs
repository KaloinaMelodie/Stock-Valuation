using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BackOffice.Data;
using BackOffice.Models;

namespace BackOffice.Pages.Produit
{
    public class CreateModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public CreateModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdCategorieProduit"] = new SelectList(_context.Set<CategorieProduit>(), "IdCategorieProduit", "Categorie");
        ViewData["IdMethod"] = new SelectList(_context.Set<Method>(), "IdMethod", "MethodName");
            return Page();
        }

        [BindProperty]
        public BackOffice.Models.Produit Produit { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["IdCategorieProduit"] = new SelectList(_context.Set<CategorieProduit>(), "IdCategorieProduit", "Categorie");
            ViewData["IdMethod"] = new SelectList(_context.Set<Method>(), "IdMethod", "MethodName");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produit.Add(Produit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

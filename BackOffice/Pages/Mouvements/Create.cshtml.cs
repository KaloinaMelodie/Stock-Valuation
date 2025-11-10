using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BackOffice.Data;
using BackOffice.Models; 

namespace BackOffice.Pages.Mouvements
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
        ViewData["IdAction"] = new SelectList(_context.Action, "IdAction", "ActionName");
        ViewData["IdProduit"] = new SelectList(_context.Produit, "IdProduit", "Nom");
            //si sortie alors pu ne s'affiche pas 
            return Page();
        }

        [BindProperty]
        public Mouvement Mouvement { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // sortie check disponibilite, afficher etat de stock


            _context.Mouvement.Add(Mouvement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

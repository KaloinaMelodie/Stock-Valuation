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

namespace BackOffice.Pages.Mouvements
{
    public class EditModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public EditModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mouvement Mouvement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvement =  await _context.Mouvement.FirstOrDefaultAsync(m => m.IdMouvement == id);
            if (mouvement == null)
            {
                return NotFound();
            }
            Mouvement = mouvement;
           ViewData["IdAction"] = new SelectList(_context.Action, "IdAction", "ActionName");
           ViewData["IdProduit"] = new SelectList(_context.Produit, "IdProduit", "Nom");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            ViewData["IdAction"] = new SelectList(_context.Action, "IdAction", "ActionName");
            ViewData["IdProduit"] = new SelectList(_context.Produit, "IdProduit", "Nom");

            _context.Attach(Mouvement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MouvementExists(Mouvement.IdMouvement))
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

        private bool MouvementExists(int id)
        {
            return _context.Mouvement.Any(e => e.IdMouvement == id);
        }
    }
}

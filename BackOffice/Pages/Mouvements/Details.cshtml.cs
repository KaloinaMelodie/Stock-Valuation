using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data;
using BackOffice.Models;

namespace BackOffice.Pages.Mouvements
{
    public class DetailsModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public DetailsModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        public Mouvement Mouvement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvement = await _context.Mouvement.FirstOrDefaultAsync(m => m.IdMouvement == id);

            if (mouvement is not null)
            {
                Mouvement = mouvement;

                return Page();
            }

            return NotFound();
        }
    }
}

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
    public class IndexModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public IndexModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        public IList<BackOffice.Models.Produit> Produit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Produit = await _context.Produit
                .Include(p => p.CategorieProduit)
                .Include(p => p.Method).ToListAsync();
        }
    }
}

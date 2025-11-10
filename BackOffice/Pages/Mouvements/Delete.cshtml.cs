using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data;
using BackOffice.Models;
using Microsoft.AspNetCore.Identity;

namespace BackOffice.Pages.Mouvements
{
    public class DeleteModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public DeleteModel(BackOffice.Data.AppDBContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public Mouvement Mouvement { get; set; } = default!;


        [TempData]
        public string ErrorMessage { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentRoles = await _userManager.GetRolesAsync(currentUser);
            if (currentRoles.Count() !=0 && currentRoles[0] != "admin")
            {
                ErrorMessage = "Employe doesn't have the right to make the action.";
                return Page();
            }
            if (id == null)
            {
                return NotFound();
            }

            var mouvement = await _context.Mouvement.FindAsync(id);
            if (mouvement != null)
            {
                Mouvement = mouvement;
                _context.Mouvement.Remove(Mouvement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

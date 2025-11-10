using BackOffice.Data;
using BackOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackOffice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDBContext _context;


        [BindProperty]
        public List<MonthlyRevenue> Revenues { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

    

        public IndexModel(AppDBContext context, ILogger<IndexModel> logger,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (!_signInManager.IsSignedIn(User)|| currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }
            //if (currentUser == null)
            //{

            //}

            var query = _context.Mouvement.AsQueryable();

            // Appliquer les filtres
            if (StartDate.HasValue)
            {
                query = query.Where(m => m.Daty >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                query = query.Where(m => m.Daty <= EndDate.Value);
            }

            // Groupement par année et mois
            Revenues = query
                .GroupBy(m => new { m.Daty.Year, m.Daty.Month })
                .Select(g => new MonthlyRevenue
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    //Revenue = g.Sum(m => m.Montant)
                    Revenue = g.Sum(m => m.Qt * m.Pu)
                })
                .OrderBy(r => r.Year).ThenBy(r => r.Month)
                .ToList();
            return Page();
        }
    }
}

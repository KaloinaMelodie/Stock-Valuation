using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 
using BackOffice.Data;
using BackOffice.Models;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;

namespace BackOffice.Pages.Mouvements
{
    public class IndexModel : PageModel
    {
        private readonly BackOffice.Data.AppDBContext _context;

        public IndexModel(BackOffice.Data.AppDBContext context)
        {
            _context = context;
        }

        public IList<Mouvement> Mouvement { get;set; } = default!;

        [TempData]
        public string ErrorMessage { get; set; }


        public async Task OnGetAsync()
        {
            ErrorMessage = null;
            Mouvement = await _context.Mouvement
                .Include(m => m.Action)
                .Include(m => m.Produit).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile csvFile)
        {
            Mouvement = await _context.Mouvement
                .Include(m => m.Action)
                .Include(m => m.Produit).ToListAsync();

            if (csvFile == null || csvFile.Length == 0)
            {
                ErrorMessage = "Veuillez sélectionner un fichier CSV.";
                return Page();
            }

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", HeaderValidated = null, MissingFieldFound = null }))
            {
                try
                {
                    var mouvements = csv.GetRecords<MouvementCsv>().ToList();

                    foreach (var mouvement in mouvements)
                    {
                        var entity = new Mouvement
                        {
                            IdProduit = mouvement.IdProduit,
                            Libelle = mouvement.Libelle,
                            Daty = mouvement.Daty,
                            IdAction = mouvement.IdAction,
                            Qt = mouvement.Qt,
                            Pu = mouvement.Pu,
                            Montant = mouvement.Montant
                        };

                        _context.Mouvement.Add(entity);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Erreur lors de l'importation : "+ex.Message;
                    return Page();
                }
            }

            TempData["SuccessMessage"] = "Importation réussie !";
            Mouvement = await _context.Mouvement
                .Include(m => m.Action)
                .Include(m => m.Produit).ToListAsync();
            return Page();
        }
    }
}

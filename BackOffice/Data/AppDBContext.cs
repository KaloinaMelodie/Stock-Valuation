using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackOffice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackOffice.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext (DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<BackOffice.Models.Product> Product { get; set; }
        public DbSet<CategorieProduit> CategorieProduit{ get; set; }
        public DbSet<Method> Method { get; set; }

        public DbSet<Produit> Produit { get; set; }

        public DbSet<BackOffice.Models.Action> Action{ get; set; }

        public DbSet<Mouvement> Mouvement { get; set; }

        //public DbSet<Race> Races { get; set; }    
        //public DbSet<Club> Clubs { get; set; }
        //public DbSet<Address> Addresses { get; set; }
    }
}

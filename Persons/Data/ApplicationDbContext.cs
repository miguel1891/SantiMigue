using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persons.Models;

namespace Persons.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //esto se asegura que cuando iniciás la APP por primera vez, exista la DB y las tablas
            //si no existe, la crea
            Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<MonedasData>()
                .HasKey(m => new { m.IDMoneda });

            builder.Entity <Cotizaciones>()
                .HasKey(c => new { c.IDCotizacion });
        

    }

    public DbSet<Persons.Models.PersonsData> PersonsData { get; set; }

    public DbSet<Persons.Models.MonedasData> Monedas { get; set; } 

    public DbSet<Persons.Models.Cotizaciones> Cotizaciones { get; set; }
    }
}

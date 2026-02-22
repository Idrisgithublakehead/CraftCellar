using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using CraftCellar.Models;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftCellar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brewery> Breweries { get; set; }
        // ok so here now we can actually tell  Entity Framework our db tables we want
        // this helps it create a breweries table and our beverages table , and the model design it takes is what we specifdied in the other model files
        // its also basically just setting up the foreign relationship

        public DbSet<Beverage> Beverages { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OmerBank.Models.Entities;
using System.Diagnostics;

namespace OmerBank.Models.Context
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Kinds)
                .WithOne(k => k.Account)
                .HasForeignKey(k => k.AccountID);

            base.OnModelCreating(modelBuilder);
        }
        //entities
        public DbSet<Account> Accounts { get; set; }
  

        public DbSet<ApplicationUserProfile> ApplicationProfiles { get; set; }

        public DbSet<Kind> Kinds { get; set; }

        public DbSet<Nation> Nations { get; set; }
    }
}



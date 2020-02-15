using IrrigationServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Context
{
    public class IrrigationDbContext : DbContext
    {
        public IrrigationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Kapcsolat létrehozása Zonak és Szenzorok között
            modelBuilder.Entity<Zona>()
                .HasMany(c => c.Szenzorok)
                .WithOne(e => e.Zona)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Kapcsolat létrehozása Szenzorok és Meresek között
            modelBuilder.Entity<Szenzor>()
                .HasMany(c => c.Meresek)
                .WithOne(e => e.Szenzor)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Zona> Zonak { get; set; }
        public DbSet<Szenzor> Szenzorok { get; set; }
        public DbSet<Meres> Meresek { get; set; }
    }
}

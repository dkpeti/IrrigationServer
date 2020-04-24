using IrrigationServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Context
{
    public class IrrigationDbContext : IdentityDbContext<User>
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public IrrigationDbContext(DbContextOptions<IrrigationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Kapcsolat létrehozása Zona és Szenzorok között
            modelBuilder.Entity<Zona>()
                .HasMany(c => c.Szenzorok)
                .WithOne(e => e.Zona)
                .OnDelete(DeleteBehavior.NoAction);

            // Kapcsolat létrehozása Szenzor és Meresek között
            modelBuilder.Entity<Szenzor>()
                .HasMany(c => c.Meresek)
                .WithOne(e => e.Szenzor)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Kapcsolat létrehozása Pi és Zónak között
            modelBuilder.Entity<Pi>()
               .HasMany(c => c.Zonak)
               .WithOne(e => e.Pi)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            //Kapcsolat létrehozása Pi és Szenzorok között
            modelBuilder.Entity<Pi>()
                .HasMany(c => c.Szenzorok)
                .WithOne(e => e.Pi)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Kapcsolat létrehozása User és Pik között
            modelBuilder.Entity<User>()
                .HasMany(c => c.Pies)
                .WithOne(e => e.User)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pi> Pies { get; set; }
        public DbSet<Zona> Zonak { get; set; }
        public DbSet<Szenzor> Szenzorok { get; set; }
        public DbSet<Meres> Meresek { get; set; }
    }
}

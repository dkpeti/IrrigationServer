using IrrigationServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrrigationServer.Context
{
    public class IrrigationDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)  //tie-up DbContext with LoggerFactory object
            .EnableSensitiveDataLogging()
            .UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=IrrigationDB;Trusted_Connection=True;ConnectRetryCount=0");
        }

        public DbSet<Zona> Zonak { get; set; }
        public DbSet<Szenzor> Szenzorok { get; set; }
        public DbSet<Meres> Meresek { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using RLS.PortfolioGeneration.Persistence.Model.Clients;
using RLS.PortfolioGeneration.Persistence.Model.Pricing;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clients");

            modelBuilder.Entity<TenancyPeriod>();

            modelBuilder.Entity<TenancyPeriod>()
                .HasOne(t => t.Account)
                .WithMany(a => a.TenancyPeriods);

            modelBuilder.Entity<TenancyPeriod>()
                .HasOne(t => t.Site)
                .WithMany(s => s.TenancyPeriods);

            modelBuilder.Entity<Site>();
            
            modelBuilder.Entity<Mpan>()
                .HasOne(s => s.Site)
                .WithMany(s => s.Mpans);

            modelBuilder.Entity<Mprn>()
                .HasOne(s => s.Site)
                .WithMany(s => s.Mprns);

            modelBuilder.Entity<Portfolio>();

            modelBuilder.Entity<PortfolioMeter>()
                .HasKey(k => new { k.MeterNumber, k.EffectiveFrom, k.EffectiveTo });

            modelBuilder.Entity<PortfolioMeter>()
                .HasOne(pm=> pm.Portfolio)
                .WithMany(p => p.PortfolioMeters);
        }



        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {

        }
    }
}
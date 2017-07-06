using Microsoft.EntityFrameworkCore;

namespace RLS.PortfolioGeneration.Persistence.Model
{
    public partial class ModelDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clients");

            modelBuilder.Entity<Site>()
                .HasOne(s => s.Account)
                .WithMany(s => s.Sites);

            modelBuilder.Entity<Mpan>()
                .HasOne(s => s.Site)
                .WithMany(s => s.Mpans); 
        }
            
        
        
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {

        }
    }
}
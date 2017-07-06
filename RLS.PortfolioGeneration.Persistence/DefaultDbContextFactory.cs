using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.Persistence
{
    public class DefaultDbContextFactory : IDbContextFactory<ModelDbContext>
    {
        public ModelDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ModelDbContext>();
            builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Initial Catalog=portfoliopricing;MultipleActiveResultSets=True;");
            return new ModelDbContext(builder.Options);
        }
    }
}
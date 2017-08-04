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
            builder.UseSqlServer("Server=tcp:rlsportfoliodb.database.windows.net,1433;Initial Catalog=portfoliopricing;Persist Security Info=False;User ID=rlsDBadmin;Password=r1sDBadm1n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Initial Catalog=portfoliopricing;MultipleActiveResultSets=True;");
            return new ModelDbContext(builder.Options);
        }
    }
}
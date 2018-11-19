using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RLS.PortfolioGeneration.Persistence.Model;

namespace RLS.PortfolioGeneration.Persistence
{
    public class DefaultDbContextFactory : IDesignTimeDbContextFactory<ModelDbContext>
    {
        public ModelDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ModelDbContext>();
            
            // LOCAL:
            builder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Initial Catalog=portfoliopricing;MultipleActiveResultSets=True;");

            // DEMO:
            //builder.UseSqlServer("Server=tcp:rlsportfoliodb.database.windows.net,1433;Initial Catalog=portfoliopricing;Persist Security Info=False;User ID=rlsDBadmin;Password=r1sDBadm1n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            // TEST:
            //builder.UseSqlServer("Server=tcp:rlsportfoliodb.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=rlsDBadmin;Password=r1sDBadm1n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            // ECA:
            // builder.UseSqlServer("Server=tcp:rlsportfoliodb.database.windows.net,1433;Initial Catalog=portfolioeca;Persist Security Info=False;User ID=rlsDBadmin;Password=r1sDBadm1n;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new ModelDbContext(builder.Options);
        }
    }
}
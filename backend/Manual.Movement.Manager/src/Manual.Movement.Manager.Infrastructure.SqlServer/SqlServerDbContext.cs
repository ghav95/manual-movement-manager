using Manual.Movement.Manager.Domain.ManualHandling;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Domain.ProductCosif;
using System.Data.Entity;
using System.Reflection;

namespace Manual.Movement.Manager.Infrastructure.SqlServer
{
    public class SqlServerDbContext : DbContext
    {
        const string ConnectionString = "Server=localhost,1433;Database=FINANCIAL_TRANSACTIONS;User Id=SA;Password=BNPParibas;";

        public SqlServerDbContext() : base (ConnectionString){}

        public DbSet<Product> Products;
        public DbSet<ProductCosif> ProductCosifs;
        public DbSet<ManualHandling> ManualHandlings;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
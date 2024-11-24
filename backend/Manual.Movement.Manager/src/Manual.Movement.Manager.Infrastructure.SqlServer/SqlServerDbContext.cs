using Manual.Movement.Manager.Domain.ManualHandling;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Domain.ProductCosif;
using System.Data.Entity;
using System.Reflection;

namespace Manual.Movement.Manager.Infrastructure.SqlServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext() : base ("SqlServerDbContext") {}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCosif> ProductCosifs { get; set; }
        public DbSet<ManualHandling> ManualHandlings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
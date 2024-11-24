namespace Manual.Movement.Manager.Infrastructure.SqlServer.Migrations
{
    using Manual.Movement.Manager.Domain.ManualHandling;
    using Manual.Movement.Manager.Domain.Product;
    using Manual.Movement.Manager.Domain.ProductCosif;
    using System.Collections.Generic;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Manual.Movement.Manager.Infrastructure.SqlServer.SqlServerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; 
        }

        public void RunSeed(SqlServerDbContext context)
        {
            Seed(context);
        }

        protected override void Seed(Manual.Movement.Manager.Infrastructure.SqlServer.SqlServerDbContext context)
        {            
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>
                {
                    new Product("P001", "Laptop", "A"),
                    new Product("P002", "Mouse", "A"),
                    new Product("P003", "Keyboard", "I"),
                });
            }
                        
            if (!context.ProductCosifs.Any())
            {
                context.ProductCosifs.AddRange(new List<ProductCosif>
                {
                    new ProductCosif("P001", "C001", "CL001", "A"),
                    new ProductCosif("P002", "C002", "CL002", "A"),
                    new ProductCosif("P003", "C003", "CL003", "I"),
                });
            }

            
            if (!context.ManualHandlings.Any())
            {
                context.ManualHandlings.AddRange(new List<ManualHandling>
                {
                    new ManualHandling(1, 2024, 1, "P001", "C001", "First Movement", DateTime.Now, "admin", 1000.50m),
                    new ManualHandling(2, 2024, 2, "P002", "C002", "Second Movement", DateTime.Now, "admin", 500.75m),
                });
            }
                        
            context.SaveChanges();
        }
    }
}

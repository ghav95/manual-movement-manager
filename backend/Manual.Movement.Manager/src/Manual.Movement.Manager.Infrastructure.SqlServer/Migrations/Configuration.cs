namespace Manual.Movement.Manager.Infrastructure.SqlServer.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Manual.Movement.Manager.Infrastructure.SqlServer.SqlServerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; 
        }
    

        protected override void Seed(Manual.Movement.Manager.Infrastructure.SqlServer.SqlServerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

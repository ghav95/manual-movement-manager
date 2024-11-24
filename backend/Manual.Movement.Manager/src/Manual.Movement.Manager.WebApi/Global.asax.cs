using Manual.Movement.Manager.Infrastructure.SqlServer;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
                        
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerDbContext, Infrastructure.SqlServer.Migrations.Configuration>());
                        
            using (var context = new SqlServerDbContext())
            {
                context.Database.Initialize(force: true);
            }

            var configuration = new Infrastructure.SqlServer.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
                        
            using (var context = new SqlServerDbContext())
            {
                configuration.RunSeed(context);
            }
        }
    }
}

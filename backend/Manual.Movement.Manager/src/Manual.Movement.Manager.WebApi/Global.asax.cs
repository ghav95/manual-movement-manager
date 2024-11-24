using Manual.Movement.Manager.Infrastructure.SqlServer;
using System.Data.Entity;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
                        
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerDbContext, Manual.Movement.Manager.Infrastructure.SqlServer.Migrations.Configuration>());
                        
            using (var context = new SqlServerDbContext())
            {
                context.Database.Initialize(force: true);
            }
        }
    }
}

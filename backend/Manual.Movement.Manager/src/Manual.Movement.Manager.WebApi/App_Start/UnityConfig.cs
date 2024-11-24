using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Infrastructure.SqlServer;
using Manual.Movement.Manager.Infrastructure.SqlServer.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Manual.Movement.Manager.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            RegisterRepositories(container);
            RegisterDbContext(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<IProductRepository, ProductRespository>(new HierarchicalLifetimeManager());
        }

        private static void RegisterDbContext(IUnityContainer container)
        {
            container.RegisterType<SqlServerDbContext>(new HierarchicalLifetimeManager());
        }
    }
}
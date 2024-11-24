using FluentValidation;
using Manual.Movement.Manager.Domain.ManualHandling;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Infrastructure.SqlServer;
using Manual.Movement.Manager.Infrastructure.SqlServer.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using Unity.WebApi;

namespace Manual.Movement.Manager.WebApi
{
    public static class UnityConfig
    {
        private static readonly Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
        private static ITypeLifetimeManager DefaultLifetimeManager => new TransientLifetimeManager();
        private static ITypeLifetimeManager ScopedLifetimeManager => new HierarchicalLifetimeManager();

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            RegisterRepositories(container);
            RegisterDbContext(container);
            RegisterMediatR(container);
            RegisterFluentValidationValidators(container);
            RegisterPipelineBehaviors(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<IProductRepository, ProductRespository>(ScopedLifetimeManager);
            container.RegisterType<IManualHandlingRepository, ManualHandlingRepository>(ScopedLifetimeManager);
        }

        private static void RegisterDbContext(IUnityContainer container)
        {
            container.RegisterType<SqlServerDbContext>(ScopedLifetimeManager);
        }

        private static void RegisterMediatR(IUnityContainer container)
        {
            container.RegisterType<IMediator, Mediator>(ScopedLifetimeManager);
            container.RegisterInstance<ServiceFactory>(type => container.Resolve(type));

            foreach (var type in AllClasses.FromAssemblies(Assemblies)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))))
            {
                var @interface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                container.RegisterType(@interface, type, DefaultLifetimeManager);
            }
        }

        private static void RegisterFluentValidationValidators(IUnityContainer container)
        {
            //foreach (var type in Assemblies.SelectMany(a => a.GetTypes())
            //    .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)))
            //{
            //    var validatorInterface = type.BaseType;
            //    container.RegisterType(validatorInterface, type, DefaultLifetimeManager);
            //}
        }

        private static void RegisterPipelineBehaviors(IUnityContainer container)
        {
            foreach (var type in AllClasses.FromAssemblies(Assemblies)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))))
            {
                var behaviorInterface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));
                container.RegisterType(behaviorInterface, type, DefaultLifetimeManager);
            }
        }
    }
}
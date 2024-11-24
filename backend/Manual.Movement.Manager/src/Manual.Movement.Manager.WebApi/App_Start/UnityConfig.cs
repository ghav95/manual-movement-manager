using FluentValidation;
using Manual.Movement.Manager.Application.Pipeline.Behaviors;
using Manual.Movement.Manager.Domain.ManualHandling;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Infrastructure.SqlServer;
using Manual.Movement.Manager.Infrastructure.SqlServer.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
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
        private static ITypeLifetimeManager ScopedLifetime => new HierarchicalLifetimeManager();
        private static ITypeLifetimeManager TransientLifetime => new TransientLifetimeManager();

        private static readonly Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .ToArray();

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            RegisterRepositories(container);
            RegisterDbContext(container);
            RegisterPipelineBehaviors(container);
            RegisterFluentValidationValidators(container);
            RegisterMediatR(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType<IProductRepository, ProductRespository>(ScopedLifetime);
            container.RegisterType<IManualHandlingRepository, ManualHandlingRepository>(ScopedLifetime);
        }

        private static void RegisterDbContext(IUnityContainer container)
        {
            container.RegisterType<SqlServerDbContext>(ScopedLifetime);
        }

        private static void RegisterMediatR(IUnityContainer container)
        {
            container.RegisterType<IMediator, Mediator>(ScopedLifetime);
            container.RegisterInstance<ServiceFactory>(type => container.Resolve(type));

            foreach (var type in AllClasses.FromAssemblies(Assemblies)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))))
            {
                var @interface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                container.RegisterType(@interface, type, TransientLifetime);
            }
        }

        private static void RegisterFluentValidationValidators(IUnityContainer container)
        {            
            foreach (var type in Assemblies.SelectMany(SafeGetTypes)
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)))
            {
                var requestType = type.BaseType.GetGenericArguments()[0]; // Get TRequest
                var validatorInterface = typeof(IValidator<>).MakeGenericType(requestType);

                container.RegisterType(validatorInterface, type, TransientLifetime);
            }
        }

        private static void RegisterPipelineBehaviors(IUnityContainer container)
        {
            container.RegisterType(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), TransientLifetime);
        }

        private static IEnumerable<Type> SafeGetTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }
    }

}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Faketory.Common
{

    public interface IActivable
    {
        public string ConfigurationKey { get; }
    }

    public static class ActiveDependencyApplyingExtensions
    {
        public static IServiceCollection DecorateIfActive<TInterface, TIActivable>(this IServiceCollection services, IConfiguration configuration) where TIActivable : IActivable, TInterface, new()
        {
            var x = new TIActivable();
            var key = x.ConfigurationKey;

            var shouldBeActivated = configuration.GetValue<bool>(key);
            if (shouldBeActivated)
            {
                var success = services.TryDecorate<TInterface, TIActivable>();
                if (!success)
                    throw new Exception($"{typeof(TInterface).Name} cannot be decorated with {typeof(TIActivable).Name}. Check if base class is added to dependency injection container.");
            }

            return services;
        }
        public static IServiceCollection OverrideIfActive<TInterface, TIActivable>(this IServiceCollection services, IConfiguration configuration) where TIActivable : IActivable, TInterface, new()
        {
            var x = new TIActivable();
            var key = x.ConfigurationKey;

            var shouldBeActivated = configuration.GetValue<bool>(key);
            if (shouldBeActivated)
            {
                var service = services.FirstOrDefault(x => x.ServiceType == typeof(TInterface));
                var serviceLifetime = service.Lifetime;
                var isRemoved = services.Remove(service);

                if (!isRemoved)
                    throw new Exception($"{typeof(TInterface).Name} cannot be overrided with {typeof(TIActivable).Name}. Check if base class is added to dependency injection container.");

                switch (serviceLifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(typeof(TInterface),typeof(TIActivable));
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(typeof(TInterface), typeof(TIActivable));
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(typeof(TInterface), typeof(TIActivable));
                        break;
                    default:
                        services.AddScoped(typeof(TInterface), typeof(TIActivable));
                        break;
                }
            }
            return services;
        }
    }
}

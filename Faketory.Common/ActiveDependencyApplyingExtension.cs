using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Faketory.Common
{

    public abstract class IActivableClass
    {
        public abstract string ConfigurationKey { get; }
    }

    public static class ActiveDependencyApplyingExtension
    {
        public static IServiceCollection DecorateIfActive<TInterface, TIActivable>(this IServiceCollection services, IConfiguration configuration) where TIActivable : IActivableClass, TInterface, new()
        {
            var x = new TIActivable();
            var key = x.ConfigurationKey;

            var shouldMeasurePlcRepoTime = configuration.GetValue<bool>(key);
            if (shouldMeasurePlcRepoTime)
            {
                var success = services.TryDecorate<TInterface, TIActivable>();
                if (!success)
                    throw new Exception($"{typeof(TInterface).Name} cannot be decorated with {typeof(TIActivable).Name}. Check if base class is added to dependency injection container.");
            }

            return services;
        }
    }
}

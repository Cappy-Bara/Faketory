using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.IPolicies;
using Faketory.Application.Policies;
using Faketory.Application.Services.Implementations;
using Faketory.Common;
using Microsoft.Extensions.Configuration;
using Faketory.Common.Configurations;

namespace Faketory.Application.Installation
{
    public static class ApplicatinInstallation
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var storageType = configuration.GetValue<StorageType>("StorageType");
            switch (storageType)
            {
                case StorageType.InDatabase:
                    services.AddScoped<ITimestampService, TimestampService>()
                    .OverrideIfActive<ITimestampService, TimeMeasuringTimestampService>(configuration);
                    break;
                case StorageType.InMemory:
                    services.AddScoped<ITimestampService,InMemoryTimestampService>();
                    break;
            }

            services.AddScoped<IInputOccupiedPolicy, InputOccupiedPolicy>();

            return services;
        }
    }
}

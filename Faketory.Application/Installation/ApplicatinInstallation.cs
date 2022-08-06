using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.IPolicies;
using Faketory.Application.Policies;
using Faketory.Application.Services.Implementations;
using Faketory.Common;
using Microsoft.Extensions.Configuration;

namespace Faketory.Application.Installation
{
    public static class ApplicatinInstallation
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ITimestampService, TimestampService>()
                .OverrideIfActive<ITimestampService, TimeMeasuringTimestampService>(configuration);
            
            services.AddScoped<IInputOccupiedPolicy, InputOccupiedPolicy>();

            return services;
        }
    }
}

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.IPolicies;
using Faketory.Application.Policies;
using Faketory.Application.Services.Implementations;

namespace Faketory.Application.Installation
{
    public static class ApplicatinInstallation
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ITimestampService, TimestampService>();
            services.AddScoped<IInputOccupiedPolicy, InputOccupiedPolicy>();

            return services;
        }
    }
}

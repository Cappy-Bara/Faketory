using Faketory.Common;
using Faketory.Domain.IRepositories;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories;
using Faketory.Infrastructure.Repositories.TimeMeasuring;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Faketory.Infrastructure.Installation
{
    public static class InfrastructureInstallation
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FaketoryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
            });

            services.AddSingleton<IPlcRepository, PlcRepository>()
                    .DecorateIfActive<IPlcRepository, PlcTimeMeasuringRepository>(configuration);

            services.AddScoped<IPlcEntityRepository, PlcEntityRepository>();
            services.AddScoped<IPlcModelRepository, PlcModelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<IIORepository, IORepository>();
            services.AddScoped<IConveyorRepository, ConveyorRepository>();
            services.AddScoped<IPalletRepository, PalletRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IMachineRepository, MachineRepository>();

            return services;
        }
    }
}

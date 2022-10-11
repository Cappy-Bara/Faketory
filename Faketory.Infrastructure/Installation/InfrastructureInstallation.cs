using Faketory.Common;
using Faketory.Common.Configurations;
using Faketory.Domain.IRepositories;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories.Database;
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
            var storageType = configuration.GetValue<StorageType>("StorageType");
            if (storageType == StorageType.InDatabase)
            {
                services.AddDbContext<FaketoryDbContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("Default"));
                });
            }
            else if (storageType == StorageType.InMemory)
            {
                services.AddSingleton<FaketoryInMemoryDbContext>();
            }

            services.AddSingleton<IPlcRepository, Repositories.InMemory.PlcRepository>()
                    .DecorateIfActive<IPlcRepository, PlcTimeMeasuringRepository>(configuration);

            services.AddRepository<IPlcEntityRepository, PlcEntityRepository, Repositories.InMemory.PlcEntityRepository>(configuration);
            services.AddRepository<IPlcModelRepository, PlcModelRepository, Repositories.InMemory.PlcModelRepository>(configuration);
            services.AddRepository<IUserRepository, UserRepository, Repositories.InMemory.UserRepository>(configuration);
            services.AddRepository<ISlotRepository, SlotRepository, Repositories.InMemory.SlotRepository>(configuration);
            services.AddRepository<IIORepository, IORepository, Repositories.InMemory.IORepository>(configuration);
            services.AddRepository<IConveyorRepository, ConveyorRepository, Repositories.InMemory.ConveyorRepository>(configuration);
            services.AddRepository<IPalletRepository, PalletRepository, Repositories.InMemory.PalletRepository>(configuration);
            services.AddRepository<ISensorRepository, SensorRepository, Repositories.InMemory.SensorRepository>(configuration);
            services.AddRepository<IMachineRepository, MachineRepository, Repositories.InMemory.MachineRepository>(configuration);

            return services;
        }

        public static IServiceCollection AddRepository<TInterface, TInDb, TInMemory>(this IServiceCollection services, IConfiguration configuration) where TInDb : TInterface where TInMemory : TInterface
        {
            var repositoryType = configuration.GetValue<StorageType>("StorageType");

            switch (repositoryType)
            {
                case StorageType.InDatabase:
                    services.AddScoped(typeof(TInterface), typeof(TInDb));
                    break;
                case StorageType.InMemory:
                    services.AddScoped(typeof(TInterface), typeof(TInMemory));
                    break;
                default:
                    break;
            }

            return services;
        }
    }
}

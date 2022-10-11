using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class ConveyorRepository : IConveyorRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public ConveyorRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public Task<List<Conveyor>> GetAllUserConveyors()
        {
            return Task.FromResult(context.Conveyors.Values.ToList());
        }
        public async Task UpdateConveyors(List<Conveyor> conveyors)
        {
            foreach (var conveyor in conveyors)
            {
                context.Conveyors[conveyor.Id] = conveyor;
            }
            await context.Persist();
        }
        public async Task UpdateConveyor(Conveyor conveyor)
        {
            context.Conveyors[conveyor.Id] = conveyor;
            await context.Persist();
        }
        public async Task RemoveConveyor(Guid id)
        {
            context.Conveyors.Remove(id);
            await context.Persist();
        }
        public Task<Conveyor> GetConveyor(Guid id)
        {
            context.Conveyors.TryGetValue(id, out var output);

            return Task.FromResult(output);
        }
        public Task<bool> ConveyorExists(Guid id)
        {
            return Task.FromResult(context.Conveyors.TryGetValue(id,out var _));
        }
        public async Task<Guid> AddConveyor(Conveyor conveyor)
        {
            conveyor.Id = Guid.NewGuid();

            context.Conveyors.Add(conveyor.Id, conveyor);

            await context.Persist();
            return conveyor.Id;
        }
    }
}

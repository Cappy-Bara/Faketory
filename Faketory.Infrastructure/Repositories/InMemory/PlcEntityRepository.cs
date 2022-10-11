using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PlcEntityRepository : IPlcEntityRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public PlcEntityRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<PlcEntity> CreatePlc(PlcEntity plc)
        {
            plc.Id = Guid.NewGuid();

            context.Plcs.Add(plc.Id, plc);
            
            await context.Persist();

            return plc;
        }
        public async Task<bool> DeletePlc(Guid id)
        {
            var success = context.Plcs.Remove(id);
            await context.Persist();
            return success;
        }
        public Task<PlcEntity> GetPlcById(Guid id)
        {
            _ = context.Plcs.TryGetValue(id, out var output);

            return Task.FromResult(output);
        }
        public Task<IEnumerable<PlcEntity>> GetUserPlcs()
        {
            return Task.FromResult(context.Plcs.Values.AsEnumerable());
        }
        public Task<bool> PlcExists(Guid id)
        {
            return Task.FromResult(context.Plcs.Values.Any(x => x.Id == id));
        }
    }
}

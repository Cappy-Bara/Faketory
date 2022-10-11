using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class IORepository : IIORepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public IORepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<Guid> CreateIO(IO io)
        {
            io.Id = Guid.NewGuid();

            context.InputsOutputs.Add(io.Id, io);

            await context.Persist();

            return io.Id;
        }
        public Task<IO> GetIO(Guid slotId, int @byte, int bit, IOType type)
        {
            return Task.FromResult(context.InputsOutputs.Values
                .FirstOrDefault(x => x.SlotId == slotId && x.Byte == @byte
                    && x.Bit == bit && x.Type == type));
        }
        public Task<IEnumerable<IO>> GetIOs()
        {
            return Task.FromResult(context.InputsOutputs.Values.AsEnumerable());
        }
        public Task<IEnumerable<IO>> GetSlotInputs(Guid slotId)
        {
            return Task.FromResult(context.InputsOutputs.Values.Where(x => x.SlotId == slotId && x.Type == IOType.Input).AsEnumerable());
        }
        [Obsolete("Splited to Get slot inputs and get slot outputs")]
        public Task<IEnumerable<IO>> GetSlotIOs(Guid slotId)
        {
            return Task.FromResult(context.InputsOutputs.Values.Where(x => x.SlotId == slotId).AsEnumerable());
        }
        public Task<IEnumerable<IO>> GetSlotOutputs(Guid slotId)
        {
            return Task.FromResult(context.InputsOutputs.Values.Where(x => x.SlotId == slotId && x.Type == IOType.Output).AsEnumerable());
        }
        public Task<bool> IOExists(Guid slotId, int @byte, int @bit, IOType type)
        {
            var output = context.InputsOutputs.Values.Any(x =>
            x.SlotId == slotId && x.Byte == @byte && x.Bit == @bit && x.Type == type);
            return Task.FromResult(output);
        }
        public Task<bool> IOExists(Guid IOId)
        {
            return Task.FromResult(context.InputsOutputs.TryGetValue(IOId,out var _));
        }
        public async Task RemoveIO(Guid IOId)
        {
            context.InputsOutputs.Remove(IOId);
            await context.Persist();
        }
        public async Task UpdateIOs(IEnumerable<IO> ios)
        {
            foreach (var io in ios)
            {
                context.InputsOutputs[io.Id] = io;
            }
            await context.Persist();
        }
    }
}

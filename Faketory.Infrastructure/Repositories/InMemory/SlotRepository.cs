using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class SlotRepository : ISlotRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public SlotRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        private void UpdateSlotNumbers()
        {
            var slots = context.Slots
                .OrderBy(x => x.Value.Number);

            int i = 1;

            foreach (var slot in slots)
            {
                slot.Value.Number = i;
                i++;
            }
        }
        private Slot GetSlot(Guid slotId)
        {
            context.Slots.TryGetValue(slotId,out var output);
            return output;
        }
        public async Task CreateSlotForUser()
        {
            var value = new Slot()
            {
                Id = Guid.NewGuid(),
                Number = 99,
            };
            context.Slots.Add(value.Id,value);
            UpdateSlotNumbers();

            await context.Persist();
        }
        public Task<IEnumerable<Slot>> GetUserSlots()
        {
            return Task.FromResult(context.Slots.Values.AsEnumerable());
        }
        public async Task<bool> RemoveSlot(Guid slotId)
        {
            var slot = GetSlot(slotId);
            
            if (slot == null)
                return false;
            
            context.Slots.Remove(slot.Id);
            await context.Persist();
            return true;
        }
        public Task<bool> SlotExists(Guid slotId)
        {
            return Task.FromResult(context.Slots.TryGetValue(slotId, out var _));
        }
        public async Task BindPlcWithSlot(Guid slotId, Guid PlcId)
        {
            var slot = GetSlot(slotId);
            slot.PlcId = PlcId;

            await context.Persist();
        }
        public Task<Slot> GetSlotById(Guid slotId)
        {
            return Task.FromResult(GetSlot(slotId));
        }
        public async Task UnbindPlcFromSlot(Guid plcId)
        {
            var slot = context.Slots.Values.FirstOrDefault(x => x.PlcId == plcId);
            
            if (slot != null)
                slot.PlcId = null;

            await context.Persist();
        }
    }
}

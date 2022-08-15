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
        private readonly Dictionary<Guid,Slot> _slots;

        public SlotRepository()
        {
            _slots = new Dictionary<Guid, Slot>();
        }

        private void UpdateSlotNumbers()
        {
            var slots = _slots
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
            _slots.TryGetValue(slotId,out var output);
            return output;
        }
        public Task CreateSlotForUser()
        {
            var value = new Slot()
            {
                Id = Guid.NewGuid(),
                Number = 99,
            };
            _slots.Add(value.Id,value);
            UpdateSlotNumbers();

            return Task.CompletedTask;
        }
        public Task<IEnumerable<Slot>> GetUserSlots()
        {
            return Task.FromResult(_slots.Values.AsEnumerable());
        }
        public Task<bool> RemoveSlot(Guid slotId)
        {
            var slot = GetSlot(slotId);
            if (slot == null)
                return Task.FromResult(false);
            _slots.Remove(slot.Id);
            return Task.FromResult(true);
        }
        public Task<bool> SlotExists(Guid slotId)
        {
            return Task.FromResult(_slots.TryGetValue(slotId, out var _));
        }
        public Task BindPlcWithSlot(Guid slotId, Guid PlcId)
        {
            var slot = GetSlot(slotId);
            slot.PlcId = PlcId;

            return Task.CompletedTask;
        }
        public Task<Slot> GetSlotById(Guid slotId)
        {
            return Task.FromResult(GetSlot(slotId));
        }
        public Task UnbindPlcFromSlot(Guid plcId)
        {
            var slot = _slots.Values.FirstOrDefault(x => x.PlcId == plcId);
            
            if (slot != null)
                slot.PlcId = null;

            return Task.CompletedTask;
        }
    }
}

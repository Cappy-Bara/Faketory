using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public SlotRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task UpdateSlotNumbers(string userEmail)
        {
            var slots =  _dbContext.Slots.Where(x => x.UserEmail == userEmail)
                .OrderBy(x => x.Number);

            int i = 1;

            foreach(Slot slot in slots)
            {
                slot.Number = i;
                i++;
            }
            await _dbContext.SaveChangesAsync();
        }
        private async Task<Slot> GetSlot(Guid slotId)
        {
            return await _dbContext.Slots.FirstOrDefaultAsync(x => x.Id == slotId);
        }
        public async Task CreateSlotForUser(string userEmail)
        {
            var value = new Slot()
            {
                UserEmail = userEmail,
                Number = 99,
            };
            _dbContext.Slots.Add(value);
            _dbContext.SaveChanges();
            await UpdateSlotNumbers(userEmail);
        }
        public async Task<IEnumerable<Slot>> GetUserSlots(string userEmail)
        {
            return await _dbContext.Slots.Where(x => x.UserEmail == userEmail).ToListAsync();
        }
        public async Task<bool> RemoveSlot(Guid slotId)
        {
            var slot = await GetSlot(slotId);
            if (slot == null)
                return false;
            _dbContext.Slots.Remove(slot);
            _dbContext.SaveChanges();
            await UpdateSlotNumbers(slot.UserEmail);
            return true;
        }
        public async Task<bool> SlotExists(Guid slotId)
        {
            return await _dbContext.Slots.AnyAsync(x => x.Id == slotId);
        }
        public async Task BindPlcWithSlot(Guid slotId, Guid PlcId)
        {
            var slot = await GetSlot(slotId);
            slot.PlcId = PlcId;
            _dbContext.Update(slot);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Slot> GetSlotById(Guid slotId)
        {
            return await GetSlot(slotId);
        }
        public async Task UnbindPlcFromSlot(Guid plcId)
        {
            var slot = await _dbContext.Slots.FirstOrDefaultAsync(x => x.PlcId == plcId);
            if (slot == null)
                return;
            slot.PlcId = null;
            _dbContext.Slots.Update(slot);
            await _dbContext.SaveChangesAsync();
        }
    }
}

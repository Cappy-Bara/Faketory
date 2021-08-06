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

        private async Task UpdateSlotNumbers(string userEmail)
        {
            var slots = _dbContext.Slots.Where(x => x.UserEmail == userEmail)
                .OrderBy(x => x.Number)
                .ToArray();

            for (int i = 0; i < slots.Length; i++)
                slots[i].Number = i + 1;

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
            await UpdateSlotNumbers(slot.UserEmail);
            return true;
        }
    }
}

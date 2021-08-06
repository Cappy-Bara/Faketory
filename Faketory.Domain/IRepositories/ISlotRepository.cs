using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface ISlotRepository
    {
        public Task<IEnumerable<Slot>> GetUserSlots(string userEmail);
        public Task CreateSlotForUser(string userEmail);
        public Task<bool> RemoveSlot(Guid slotId);
    }
}

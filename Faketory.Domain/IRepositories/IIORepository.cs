using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface IIORepository
    {
        public Task CreateIO(IO io);
        public Task<IEnumerable<IO>> GetSlotsIOs(Guid slotId);
        public Task UpdateIOs(IEnumerable<IO> ios);
        public Task<bool> IOExists(Guid slotId, int @byte, int @bit);
    }
}

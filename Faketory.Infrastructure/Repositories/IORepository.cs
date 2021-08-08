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
    public class IORepository : IIORepository
    {
        private readonly FaketoryDbContext _dbContext;
        public IORepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateIO(IO io)
        {
            await _dbContext.InputsOutputs.AddAsync(io);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<IO>> GetSlotsIOs(Guid slotId)
        {
            return await _dbContext.InputsOutputs.Where(x => x.SlotId == slotId).ToListAsync();
        }
        public async Task<bool> IOExists(Guid slotId, int @byte, int @bit)
        {
            return await _dbContext.InputsOutputs.AnyAsync(x =>
            x.SlotId == slotId && x.Byte == @byte && x.Bit == @bit);
        }
        public async Task UpdateIOs(IEnumerable<IO> ios)
        {
            _dbContext.UpdateRange(ios);
            await _dbContext.SaveChangesAsync();
        }
    }
}

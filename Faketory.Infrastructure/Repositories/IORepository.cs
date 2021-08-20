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
        public async Task<IEnumerable<IO>> GetSlotIOs(Guid slotId)
        {
            return await _dbContext.InputsOutputs.Where(x => x.SlotId == slotId).ToListAsync();
        }
        public async Task<bool> IOExists(Guid slotId, int @byte, int @bit, IOType type)
        {
            var output = await _dbContext.InputsOutputs.AnyAsync(x =>
            x.SlotId == slotId && x.Byte == @byte && x.Bit == @bit && x.Type == type);
            return output;
        }
        public async Task<bool> IOExists(Guid IOId)
        {
            return await _dbContext.InputsOutputs.AnyAsync(x => x.Id == IOId);
        }
        public async Task RemoveIO(Guid IOId)
        {
            var value = await _dbContext.InputsOutputs.FirstOrDefaultAsync(x => x.Id == IOId);
            _dbContext.InputsOutputs.Remove(value);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateIOs(IEnumerable<IO> ios)
        {
            _dbContext.UpdateRange(ios);
            await _dbContext.SaveChangesAsync();
        }
    }
}

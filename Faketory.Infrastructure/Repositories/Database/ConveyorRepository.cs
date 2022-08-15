using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.Database
{
    public class ConveyorRepository : IConveyorRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public ConveyorRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Conveyor>> GetAllUserConveyors()
        {
            return _dbContext.Conveyors
                .Include(x => x.IO)
                .ToListAsync();
        }

        public async Task UpdateConveyors(List<Conveyor> conveyors)
        {
            _dbContext.Conveyors.UpdateRange(conveyors);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateConveyor(Conveyor conveyor)
        {
            _dbContext.Conveyors.Update(conveyor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveConveyor(Guid id)
        {
            var conv = await _dbContext.Conveyors.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Conveyors.Remove(conv);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Conveyor> GetConveyor(Guid id)
        {
            return await _dbContext.Conveyors.Include(x => x.IO)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> ConveyorExists(Guid id)
        {
            return await _dbContext.Conveyors.AnyAsync(x => x.Id == id);
        }
        public async Task<Guid> AddConveyor(Conveyor conveyor)
        {
            var id = (await _dbContext.Conveyors.AddAsync(conveyor)).Entity.Id;
            await _dbContext.SaveChangesAsync();
            return id;
        }
    }
}

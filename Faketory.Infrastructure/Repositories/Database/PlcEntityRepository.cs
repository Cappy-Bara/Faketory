using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.Database
{
    public class PlcEntityRepository : IPlcEntityRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public PlcEntityRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PlcEntity> CreatePlc(PlcEntity plc)
        {
            var id = (await _dbContext.Plcs.AddAsync(plc)).Entity.Id;
            await _dbContext.SaveChangesAsync();
            var output = await GetPlcById(id);
            return output;
        }
        public async Task<bool> DeletePlc(Guid id)
        {
            var deleteObject = await _dbContext.Plcs.FirstOrDefaultAsync(x => x.Id == id);
            if (deleteObject == null)
                return false;
            _dbContext.Remove(deleteObject);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<PlcEntity> GetPlcById(Guid id)
        {
            var output = await _dbContext.Plcs
                .Include(x => x.Model)
                .FirstOrDefaultAsync(x => x.Id == id);
            return output;
        }
        public async Task<IEnumerable<PlcEntity>> GetUserPlcs()
        {
            return await _dbContext.Plcs.ToListAsync();
        }
        public async Task<bool> PlcExists(Guid id)
        {
            return await _dbContext.Plcs.AnyAsync(x => x.Id == id);
        }
    }
}

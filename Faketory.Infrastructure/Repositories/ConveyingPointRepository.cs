using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories
{
    public class ConveyingPointRepository : IConveyingPointRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public ConveyingPointRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddConveyingPoints(List<ConveyingPoint> conveyingPoints)
        {
            await _dbContext.AddRangeAsync(conveyingPoints);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<ConveyingPoint>> GetAllUserConveyingPoints(string email)
        {
            return await _dbContext.ConveyingPoints.Include(x => x.Conveyor).Where(x => x.Conveyor.UserEmail == email).ToListAsync();
        }
        public async Task RemoveConveyingPoints(List<ConveyingPoint> conveyingPoints)
        {
            _dbContext.ConveyingPoints.RemoveRange(conveyingPoints);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateConveyingPoints(List<ConveyingPoint> conveyingPoints)
        {
            _dbContext.UpdateRange(conveyingPoints);
            await _dbContext.SaveChangesAsync();
        }
    }
}

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
    public class ConveyorRepository : IConveyorRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public ConveyorRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Conveyor>> GetAllUserConveyors(string email)
        {
            return _dbContext.Conveyors.Where(x => x.UserEmail == email)
                .Include(x => x.IO).Include(x => x.ConveyingPoints)
                .ToListAsync();
        }

        public async Task UpdateConveyors(List<Conveyor> conveyors)
        {
            _dbContext.Conveyors.UpdateRange(conveyors);
            await _dbContext.SaveChangesAsync();
        }
    }
}

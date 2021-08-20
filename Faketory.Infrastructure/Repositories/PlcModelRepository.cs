using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories
{
    public class PlcModelRepository : IPlcModelRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public PlcModelRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ModelExists(int modelId)
        {
            return await _dbContext.PlcModels.AnyAsync(x => x.CpuModel == modelId);
        }
    }
}

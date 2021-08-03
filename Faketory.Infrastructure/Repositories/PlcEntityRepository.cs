﻿using System;
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
    public class PlcEntityRepository : IPlcEntityRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public PlcEntityRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PlcEntity> CreatePlc(PlcEntity plc)
        {
            var output = (await _dbContext.Plcs.AddAsync(plc)).Entity;
            await _dbContext.SaveChangesAsync();
            return output;
        }
        public async Task DeletePlc(Guid id)
        {
            var deleteObject = await _dbContext.Plcs.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Remove(deleteObject);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<PlcEntity> GetPlcById(Guid id)
        {
            var output = await _dbContext.Plcs
                .FirstOrDefaultAsync(x => x.Id == id);
            return output;
        }
        public async Task<IEnumerable<PlcEntity>> GetUserPlcs(string email)
        {
            return await _dbContext.Plcs.Where(x => x.UserEmail == email).ToListAsync();
        }
    }
}

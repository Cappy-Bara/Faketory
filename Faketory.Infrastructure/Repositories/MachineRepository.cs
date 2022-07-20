using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faketory.Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly FaketoryDbContext _dbContext;
        public MachineRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Machine>> GetAllUserMachines(string userEmail)
        {
            return await _dbContext.Machines.Where(x => x.UserEmail == userEmail)
                    .ToListAsync();
        }

        public async Task UpdateMachines(IEnumerable<Machine> machines)
        {
            _dbContext.Machines.UpdateRange(machines);
            await _dbContext.SaveChangesAsync();
        }
    }
}

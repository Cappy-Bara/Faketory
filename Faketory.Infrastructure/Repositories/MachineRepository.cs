using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Guid> AddMachine(Machine machine)
        {
            var id = (await _dbContext.Machines.AddAsync(machine)).Entity.Id;
            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task DeleteMachine(Machine machine)
        {
            _dbContext.Machines.Remove(machine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Machine>> GetAllUserMachines(string userEmail)
        {
            return await _dbContext.Machines.Where(x => x.UserEmail == userEmail)
                    .ToListAsync();
        }

        public async Task<Machine> GetMachine(Guid id)
        {
            return await _dbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateMachine(Machine machine)
        {
            _dbContext.Machines.Update(machine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMachines(IEnumerable<Machine> machines)
        {
            _dbContext.Machines.UpdateRange(machines);
            await _dbContext.SaveChangesAsync();
        }
    }
}

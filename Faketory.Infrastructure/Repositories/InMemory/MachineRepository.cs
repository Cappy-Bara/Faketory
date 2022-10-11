using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class MachineRepository : IMachineRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public MachineRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<Guid> AddMachine(Machine machine)
        {
            machine.Id = Guid.NewGuid();
            context.Machines.Add(machine.Id,machine);

            await context.Persist();

            return machine.Id;
        }

        public async Task DeleteMachine(Machine machine)
        {
            context.Machines.Remove(machine.Id);
            await context.Persist();
        }

        public Task<List<Machine>> GetAllUserMachines()
        {
            return Task.FromResult(context.Machines.Values.ToList());
        }

        public Task<Machine> GetMachine(Guid id)
        {
            _ = context.Machines.TryGetValue(id,out var output);

            return Task.FromResult(output);
        }

        public async Task UpdateMachine(Machine machine)
        {
            context.Machines[machine.Id] = machine;
            await context.Persist();
        }

        public async Task UpdateMachines(IEnumerable<Machine> machines)
        {
            foreach (var machine in machines)
            {
                context.Machines[machine.Id] = machine;
            }

            await context.Persist();
        }
    }
}

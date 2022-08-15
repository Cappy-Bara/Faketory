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
        private readonly Dictionary<Guid, Machine> _machines;

        public MachineRepository()
        {
            _machines = new();
        }

        public Task<Guid> AddMachine(Machine machine)
        {
            machine.Id = Guid.NewGuid();
            _machines.Add(machine.Id,machine);

            return Task.FromResult(machine.Id);
        }

        public Task DeleteMachine(Machine machine)
        {
            _machines.Remove(machine.Id);
            return Task.CompletedTask;
        }

        public Task<List<Machine>> GetAllUserMachines()
        {
            return Task.FromResult(_machines.Values.ToList());
        }

        public Task<Machine> GetMachine(Guid id)
        {
            _ = _machines.TryGetValue(id,out var output);

            return Task.FromResult(output);
        }

        public Task UpdateMachine(Machine machine)
        {
            _machines[machine.Id] = machine;
            return Task.CompletedTask;
        }

        public Task UpdateMachines(IEnumerable<Machine> machines)
        {
            foreach (var machine in machines)
            {
                _machines[machine.Id] = machine;
            }

            return Task.CompletedTask;
        }
    }
}

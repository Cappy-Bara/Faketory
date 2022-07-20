using Faketory.Domain.Resources.IndustrialParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.IRepositories
{
    public interface IMachineRepository
    {
        public Task<List<Machine>> GetAllUserMachines(string userEmail);
        public Task UpdateMachines(IEnumerable<Machine> machines);
        public Task<Guid> AddMachine(Machine machine);
        public Task UpdateMachine(Machine machine);
        public Task DeleteMachine(Machine machine);
        public Task<Machine> GetMachine(Guid id);
    }
}

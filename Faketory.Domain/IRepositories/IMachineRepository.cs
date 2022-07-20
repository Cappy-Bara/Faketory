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
    }
}

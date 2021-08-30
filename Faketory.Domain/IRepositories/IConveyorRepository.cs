using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.IRepositories
{
    public interface IConveyorRepository
    {
        public Task<List<Conveyor>> GetAllUserConveyors(string email);
        public Task UpdateConveyors(List<Conveyor> conveyors);
    }
}

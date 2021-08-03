using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface IPlcRepository
    {
        public Task CreatePlc(PlcEntity entity);
        public Task ConnectToPlc(Guid id);
        public Task WriteToPlc(Guid id, int @byte, int @bit, bool value);
        public Task<bool> ReadFromPlc(Guid id, int @byte, int @bit);
        public Task DeletePlc(Guid id);
    }
}

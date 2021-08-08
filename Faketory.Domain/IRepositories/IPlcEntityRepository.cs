using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface IPlcEntityRepository
    {
        public Task<PlcEntity> CreatePlc(PlcEntity plc);
        public Task<bool> DeletePlc(Guid id);
        public Task<IEnumerable<PlcEntity>> GetUserPlcs(string email);
        public Task<PlcEntity> GetPlcById(Guid id);
        public Task<bool> PlcExists(Guid id);
    }
}

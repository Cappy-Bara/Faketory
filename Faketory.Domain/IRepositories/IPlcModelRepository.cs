using Faketory.Domain.Resources.PLCRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.IRepositories
{
    public interface IPlcModelRepository
    {
        public Task<bool> ModelExists(int modelId);
        public Task<PlcModel> GetModel(int modelName);
    }
}

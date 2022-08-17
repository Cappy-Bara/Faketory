using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PlcModelRepository : IPlcModelRepository
    {
        private readonly List<PlcModel> _plcModels;

        public PlcModelRepository()
        {
            _plcModels = PlcModelsSeeder.GetData().ToList();
        }

        public Task<PlcModel> GetModel(int modelName)
        {
            return Task.FromResult(_plcModels.FirstOrDefault(x => modelName == x.CpuModel));
        }

        public Task<bool> ModelExists(int modelId)
        {
            return Task.FromResult(_plcModels.Any(x => x.CpuModel == modelId));
        }
    }
}

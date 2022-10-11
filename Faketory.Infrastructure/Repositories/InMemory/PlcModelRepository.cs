using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PlcModelRepository : IPlcModelRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public PlcModelRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public Task<PlcModel> GetModel(int modelName)
        {
            return Task.FromResult(context.PlcModels.FirstOrDefault(x => modelName == x.CpuModel));
        }

        public Task<bool> ModelExists(int modelId)
        {
            return Task.FromResult(context.PlcModels.Any(x => x.CpuModel == modelId));
        }
    }
}

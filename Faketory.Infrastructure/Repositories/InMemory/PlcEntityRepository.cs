using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PlcEntityRepository : IPlcEntityRepository
    {
        private readonly Dictionary<Guid, PlcEntity> _plcEntities;

        public PlcEntityRepository()
        {
            _plcEntities = new();
        }

        public Task<PlcEntity> CreatePlc(PlcEntity plc)
        {
            plc.Id = Guid.NewGuid();

            _plcEntities.Add(plc.Id, plc);
            return Task.FromResult(plc);
        }
        public Task<bool> DeletePlc(Guid id)
        {
            return Task.FromResult(_plcEntities.Remove(id));
        }
        public Task<PlcEntity> GetPlcById(Guid id)
        {
            _ = _plcEntities.TryGetValue(id, out var output);

            return Task.FromResult(output);
        }
        public Task<IEnumerable<PlcEntity>> GetUserPlcs()
        {
            return Task.FromResult(_plcEntities.Values.AsEnumerable());
        }
        public Task<bool> PlcExists(Guid id)
        {
            return Task.FromResult(_plcEntities.Values.Any(x => x.Id == id));
        }
    }
}

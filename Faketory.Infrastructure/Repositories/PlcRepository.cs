using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using S7.Net;

namespace Faketory.Infrastructure.Repositories
{
    public class PlcRepository : IPlcRepository
    {
        private Dictionary<Guid, Plc> Plcs { get; set; } = new Dictionary<Guid, Plc>();
        private Plc GetPlc(Guid id)
        {
            var plc = Plcs.FirstOrDefault(x => x.Key == id).Value;
            return plc;
        }
        public async Task<bool> PlcExists(Guid plcId)
        {
            var plc = GetPlc(plcId);
            return !(plc == null);
        }
        public async Task CreatePlc(PlcEntity entity)
        {
            var plc = entity.CreatePlc();
            Plcs.Add(entity.Id, plc);
            await Task.CompletedTask;
        }
        public async Task<bool> ConnectToPlc(Guid id)
        {
            var plc = GetPlc(id);
            await plc.OpenAsync();
            return plc.IsConnected;
        }
        public async Task<bool> IsConnected(Guid id)
        {
            return GetPlc(id).IsConnected;
        }
        public async Task DeletePlc(Guid id)
        {
            Plcs.Remove(id);
            await Task.CompletedTask;
        }
        public async Task<bool> ReadFromPlc(Guid id, int @byte, int @bit)
        {
            var plc = GetPlc(id);
            return (bool)(await plc.ReadAsync($"O{@byte}.{@bit}"));            //TODO - READ MULTIPLE VALUES!
        }
        public async Task WriteToPlc(Guid id, int @byte, int @bit, bool value)
        {
            var plc = GetPlc(id);
            await plc.WriteAsync($"I{@byte}.{@bit}", value);
        }
    }
}

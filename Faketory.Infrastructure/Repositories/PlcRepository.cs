using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
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
        public async Task CreatePlc(PlcEntity entity)
        {
            var plc = entity.CreatePlc();
            Plcs.Add(entity.Id, plc);
            await Task.CompletedTask;
        }
        public async Task ConnectToPlc(Guid id)
        {
            var plc = GetPlc(id);
            await plc.OpenAsync();
            if (!plc.IsConnected)
                throw new CommunicationException("Communication error! PLC not connected.");
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

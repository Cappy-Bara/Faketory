using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Aggregates
{
    public class FaketoryDataset
    {
        public Dictionary<string, User> Users { get; set; } = new();
        public List<PlcModel> PlcModels { get; set; }
        public Dictionary<Guid, PlcEntity> Plcs { get; set; } = new();
        public Dictionary<Guid, Slot> Slots { get; set; } = new();
        public Dictionary<Guid, IO> InputsOutputs { get; set; } = new();
        public Dictionary<Guid, Pallet> Pallets { get; set; } = new();
        public Dictionary<Guid, Conveyor> Conveyors { get; set; } = new();
        public Dictionary<Guid, Sensor> Sensors { get; set; } = new();
        public Dictionary<Guid, Machine> Machines { get; set; } = new();
    }
}

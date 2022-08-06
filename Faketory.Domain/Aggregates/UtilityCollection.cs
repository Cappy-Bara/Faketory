using Faketory.Domain.Resources.IndustrialParts;
using System.Collections.Generic;

namespace Faketory.Domain.Aggregates
{
    public class UtilityCollection
    {
        public List<Conveyor> Conveyors { get; set; }
        public List<Pallet> Pallets { get; set; }
        public List<Sensor> Sensors { get; set; }
        public List<Machine> Machines { get; set; }
    }
}

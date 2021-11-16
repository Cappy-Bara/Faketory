using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class ModifiedUtils
    {
        public IEnumerable<Pallet> Pallets { get; set; }
        public IEnumerable<SensorState> Sensors { get; set; }
        public IEnumerable<ConveyorState> Conveyors { get; set; }
    }
}

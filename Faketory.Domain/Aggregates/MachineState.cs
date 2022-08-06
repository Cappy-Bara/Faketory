using Faketory.Domain.Resources.IndustrialParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Aggregates
{
    public class MachineState
    {
        public Guid Id { get; set; }
        public bool IsProcessing { get; private set; }

        public MachineState(Machine machine)
        {
            Id = machine.Id;
            IsProcessing = machine.IsProcessing;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class ConveyorState
    {
        public Guid Id { get; set; }
        public bool IsRunning { get; set; }

        public ConveyorState(Conveyor conveyor)
        {
            Id = conveyor.Id;
            IsRunning = conveyor.IsRunning;
        }
    }
}

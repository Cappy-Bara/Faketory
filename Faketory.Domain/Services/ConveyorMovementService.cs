using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Services
{
    public class ConveyorMovementService
    {
        private List<Conveyor> _conveyors { get; set; }
        private List<Pallet> _pallets { get; set; }

        public ConveyorMovementService(List<Conveyor> conveyors, List<Pallet> pallets)
        {
            _conveyors = conveyors;
            _pallets = pallets;
        }








    }
}

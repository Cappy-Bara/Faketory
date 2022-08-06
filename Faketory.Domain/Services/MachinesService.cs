using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.IndustrialParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Services
{
    public class MachinesService
    {
        private List<Machine> _machines { get; set; }
        private List<Pallet> _pallets { get; set; }

        public List<MachineState> ModifiedMachines { get; private set; } = new List<MachineState>();

        public MachinesService(List<Machine> machines, List<Pallet> pallets)
        {
            _machines = machines;
            _pallets = pallets;
        }


        public void HandleProcessing()
        {
            foreach (var machine in _machines)
            {
                bool processingStatusBefore = machine.IsProcessing;

                machine.Process(_pallets);

                if (processingStatusBefore != machine.IsProcessing)
                    UpdateModifiedMachines(machine);
            }
        }

        public void TurnOnOrOff()
        {
            foreach (var machine in _machines)
            {
                bool processingStatusBefore = machine.IsProcessing;

                machine.TurnOffIfNoPallets(_pallets);

                if (processingStatusBefore != machine.IsProcessing)
                    UpdateModifiedMachines(machine);
            }
        }

        private void UpdateModifiedMachines(Machine machine)
        {
            var machineToModify = ModifiedMachines.FirstOrDefault(x => x.Id == machine.Id);
            if (machineToModify != null)
            {
                ModifiedMachines.Remove(machineToModify);
            }
            ModifiedMachines.Add(new MachineState(machine));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Services;

namespace Faketory.Domain.Aggregates
{
    public static class Scene
    {
        public static ModifiedUtils HandleTimestamp(UtilityCollection utils)
        {
            var machinesService = new MachinesService(utils.Machines, utils.Pallets);
            machinesService.HandleProcessing();

            var conveyingService = new ConveyingService(utils.Conveyors, utils.Pallets);
            conveyingService.HandleConveyorMovement();

            machinesService.TurnOnOrOff();

            var sensingService = new SensingService(utils.Pallets, utils.Sensors);
            sensingService.HandleSensing();

            return new ModifiedUtils()
            {
                Sensors = sensingService.ModifiedSensors,
                Pallets = conveyingService.ModifiedPallets,
                Conveyors = conveyingService.ModifiedConveyors,
                Machines = machinesService.ModifiedMachines,
            };
        }
    }
}

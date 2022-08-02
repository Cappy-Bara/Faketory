using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Application.Resources.IOs.Commands.ReadOutputsFromPlc;
using Faketory.Application.Resources.IOs.Commands.WriteInputsToPlc;
using Faketory.Application.Resources.Slots.Queries.GetAllUserSlots;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampService : ITimestampService
    {
        private readonly IMediator _mediator;
        private readonly IPalletRepository _palletRepo;
        private readonly ISensorRepository _sensorRepo;
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IMachineRepository _machinesRepo;

        public TimestampService(IMediator mediator, 
                                IPalletRepository palletRepo, 
                                ISensorRepository sensorRepo, 
                                IConveyorRepository conveyorRepo, 
                                IMachineRepository machinesRepo)
        {
            _mediator = mediator;
            _palletRepo = palletRepo;
            _sensorRepo = sensorRepo;
            _conveyorRepo = conveyorRepo;
            _machinesRepo = machinesRepo;
        }


        public async Task<ModifiedUtils> Timestamp(string userEmail)
        {
            var slotsQuery = new GetAllUserSlotsQuery()
            {
                Id = userEmail,
            };
            var slots = await _mediator.Send(slotsQuery);

            await ReadFromOutputs(slots);

            var userUtils = await GetUserUtils(userEmail);

            var modifiedUtils = Scene.HandleTimestamp(userUtils);
            
            await UpdateInDatabase(userUtils);

            await WriteToInputs(slots);

            return modifiedUtils;
        }

        private async Task WriteToInputs(IEnumerable<Slot> slots)
        {
            var writeCommand = new WriteInputsToPlcCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(writeCommand);
        }
        private async Task ReadFromOutputs(IEnumerable<Slot> slots)
        {
            var readOutputs = new ReadOutputsFromPlcCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(readOutputs);
        }
        private async Task<UtilityCollection> GetUserUtils(string userEmail)
        {
            return new UtilityCollection()
            {
                Conveyors = await _conveyorRepo.GetAllUserConveyors(userEmail),
                Pallets = await _palletRepo.GetAllUserPallets(userEmail),
                Machines = await _machinesRepo.GetAllUserMachines(userEmail),
                Sensors = await _sensorRepo.GetUserSensors(userEmail)
            };
        }
        private async Task UpdateInDatabase(UtilityCollection utils)
        {
            await _conveyorRepo.UpdateConveyors(utils.Conveyors);
            await _palletRepo.UpdatePallets(utils.Pallets);
            await _sensorRepo.UpdateSensors(utils.Sensors);
            await _machinesRepo.UpdateMachines(utils.Machines);
        }
    }
}

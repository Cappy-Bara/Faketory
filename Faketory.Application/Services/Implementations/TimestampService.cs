using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Application.Resources.IOs.Commands.ReadOutputsFromPlc;
using Faketory.Application.Resources.IOs.Commands.WriteInputsToPlc;
using Faketory.Application.Resources.Slots.Queries.GetAllUserSlots;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampService : ITimestampService
    {
        protected readonly IMediator _mediator;
        protected readonly IPalletRepository _palletRepo;
        protected readonly ISensorRepository _sensorRepo;
        protected readonly IConveyorRepository _conveyorRepo;
        protected readonly IMachineRepository _machinesRepo;

        private IEnumerable<Slot> _slots;
        private UtilityCollection _userUtils;

        public TimestampService()
        {

        }

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

        public async Task<ModifiedUtils> Timestamp()
        {
            await DatabaseReading();

            await PlcReading();

            var modifiedUtils = SceneHandling();

            await DatabaseWriting();

            await PlcWriting();

            return modifiedUtils;
        }

        protected virtual async Task DatabaseReading()
        {
            var slotsQuery = new GetAllUserSlotsQuery(){};

            _slots = await _mediator.Send(slotsQuery);
            _userUtils = await GetUserUtils();
        }
        protected virtual async Task PlcReading()
        {
            await ReadFromOutputs(_slots);
        }
        protected virtual ModifiedUtils SceneHandling()
        {
            return Scene.HandleTimestamp(_userUtils);
        }
        protected virtual async Task DatabaseWriting()
        {
            await UpdateInDatabase(_userUtils);
        }
        protected virtual async Task PlcWriting()
        {
            await WriteToInputs(_slots);
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
        private async Task<UtilityCollection> GetUserUtils()
        {
            return new UtilityCollection()
            {
                Conveyors = await _conveyorRepo.GetAllUserConveyors(),
                Pallets = await _palletRepo.GetAllUserPallets(),
                Machines = await _machinesRepo.GetAllUserMachines(),
                Sensors = await _sensorRepo.GetUserSensors()
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

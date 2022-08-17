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

        protected IEnumerable<Slot> _slots;
        protected UtilityCollection _userUtils;

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
            await DataReading();

            await PlcReading();

            var modifiedUtils = SceneHandling();

            await DataWriting();

            await PlcWriting();

            return modifiedUtils;
        }

        protected virtual async Task DataReading()
        {
            var slotsQuery = new GetAllUserSlotsQuery(){};

            _slots = await _mediator.Send(slotsQuery);
            _userUtils = await GetUserUtils();
        }
        
        protected virtual async Task PlcReading()
        {
            var readOutputs = new ReadOutputsFromPlcCommand()
            {
                SlotIds = _slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(readOutputs);
        }
        protected virtual ModifiedUtils SceneHandling()
        {
            return Scene.HandleTimestamp(_userUtils);
        }
        protected virtual async Task DataWriting()
        {
            await _conveyorRepo.UpdateConveyors(_userUtils.Conveyors);
            await _palletRepo.UpdatePallets(_userUtils.Pallets);
            await _sensorRepo.UpdateSensors(_userUtils.Sensors);
            await _machinesRepo.UpdateMachines(_userUtils.Machines);
        }
        protected virtual async Task PlcWriting()
        {
            var writeCommand = new WriteInputsToPlcCommand()
            {
                SlotIds = _slots.Select(x => x.Id.ToString()).ToArray()
            };

            await _mediator.Send(writeCommand);
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
    }
}

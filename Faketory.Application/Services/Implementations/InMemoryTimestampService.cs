using Faketory.Application.Services.Interfaces;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Faketory.Application.Services.Implementations
{
    public class InMemoryTimestampService : TimestampService, ITimestampService
    {
        private readonly IIORepository _ioRepo;
        private readonly IPlcRepository _plcRepo;

        public InMemoryTimestampService(
            IMediator mediator,
            IPalletRepository palletRepo,
            ISensorRepository sensorRepo,
            IConveyorRepository conveyorRepo,
            IMachineRepository machinesRepo,
            IIORepository ioRepo,
            IPlcRepository plcRepo) : base(mediator, palletRepo, sensorRepo, conveyorRepo, machinesRepo)
        {
            _ioRepo = ioRepo;
            _plcRepo = plcRepo;
        }

        public override async Task DataReading()
        {
            await base.DataReading();

            var ios = await _ioRepo.GetIOs();

            _userUtils.Sensors = _userUtils.Sensors.Join(ios, sensor => sensor.IOId, io => io.Id, (x, y) => { x.IO = y; return x; }).ToList();
            _userUtils.Conveyors = _userUtils.Conveyors.Join(ios, conveyor => conveyor.IOId, io => io.Id, (x, y) => { x.IO = y; return x; }).ToList();
        }
        public override Task DataWriting()
        {
            return Task.CompletedTask;
        }

        public override async Task PlcReading()
        {
            foreach(var slot in _slots)
            {
                var outputs = await _ioRepo.GetSlotOutputs(slot.Id);        //TODO Test if can be taken from slots
                if (!outputs.Any())
                {
                    continue;
                }

                var plcId = slot.PlcId ?? Guid.Empty;

                if (plcId == Guid.Empty || !_plcRepo.PlcExists(plcId) || !_plcRepo.IsConnected(plcId))
                {
                    foreach (IO io in outputs)
                    {
                        io.Value = false;
                    }
                }
                else
                {
                    foreach (IO io in outputs)
                    {
                        io.Value = await _plcRepo.ReadFromPlc(plcId, io.Byte, io.Bit);      //read multiple values
                    }
                }
            }
        }

        public override async Task PlcWriting()
        {
            foreach (var slot in _slots)
            {
                var inputs = await _ioRepo.GetSlotInputs(slot.Id);          //TODO Test if can be taken from slots
                if (!inputs.Any())
                {
                    continue;
                }

                var plcId = slot.PlcId ?? Guid.Empty;
                if (plcId != Guid.Empty && _plcRepo.PlcExists(plcId) && _plcRepo.IsConnected(plcId))
                {
                    foreach (IO io in inputs)
                    {
                        await _plcRepo.WriteToPlc(plcId, io.Byte, io.Bit, io.Value);    //write multiple values
                    }
                }
            }
        }
    }
}

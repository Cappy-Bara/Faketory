using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Exceptions;
using Faketory.Domain.Factories;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorHandler : IRequestHandler<UpdateSensorCommand, Unit>
    {
        private readonly IIORepository _ioRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;
        private readonly ISensorRepository _sensorRepo;

        public UpdateSensorHandler(IUserRepository userRepo, IIORepository ioRepo, ISlotRepository slotRepo, ISensorRepository sensorRepo)
        {
            _userRepo = userRepo;
            _ioRepo = ioRepo;
            _slotRepo = slotRepo;
            _sensorRepo = sensorRepo;
        }

        public async Task<Unit> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
        {
            var sensor = await _sensorRepo.GetSensorById(request.SensorId);
            if (sensor == null)
                throw new NotFoundException("This sensor does not exist");

            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User does not exist");

            if (!await _slotRepo.SlotExists(request.SlotId))
            {
                throw new NotFoundException("Slot does not exist");
            }//sprawdzić czy slot jest tego usera

            var ioFactory = new IOFactory(_ioRepo);
            var IO = await ioFactory.GetOrCreateIO(request.Bit, request.Byte, request.SlotId, IOType.Input);

            sensor.IOId = IO.Id;
            sensor.PosX = request.PosX;
            sensor.PosY = request.PosY;
            sensor.IO = IO;

            await _sensorRepo.UpdateSensor(sensor);
            return Unit.Value;
        }
    }
}

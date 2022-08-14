using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Application.Policies;
using Faketory.Domain.Enums;
using Faketory.Domain.Exceptions;
using Faketory.Domain.Factories;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.CreateSensor
{
    public class CreateSensorHandler : IRequestHandler<CreateSensorCommand, Unit>
    {
        private readonly ISensorRepository _sensorRepo;
        private readonly IIORepository _iORepo;
        private readonly ISlotRepository _slotRepo;

        public CreateSensorHandler(ISlotRepository slotRepo, IIORepository iORepo, ISensorRepository sensorRepo)
        {
            _slotRepo = slotRepo;
            _iORepo = iORepo;
            _sensorRepo = sensorRepo;
        }

        public async Task<Unit> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
        {
            if (!await _slotRepo.SlotExists(request.SlotId))
                throw new NotFoundException("Slot does not exist");

            var ioFactory = new IOFactory(_iORepo);
            var io = await ioFactory.GetOrCreateIO(request.Bit, request.Byte, request.SlotId, IOType.Input);

            var policy = new InputOccupiedPolicy(_sensorRepo);
            var inputOccupied = await policy.IsOccupied(io.Id);
            if (inputOccupied)
                throw new OccupiedException("This input is already in use!");

            var sensor = new Sensor()
            {
                IOId = io.Id,
                PosX = request.PosX,
                PosY = request.PosY,
                NegativeLogic = request.NegativeLogic
            };

            await _sensorRepo.AddSensor(sensor);
            return Unit.Value;
        }
    }
}

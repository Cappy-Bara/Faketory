using System;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Exceptions;
using Faketory.Domain.Factories;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Commands.CreateConveyor
{
    public class CreateConveyorHandler : IRequestHandler<CreateConveyorCommand, Guid>
    {
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IIORepository _ioRepo;
        private readonly ISlotRepository _slotRepo;

        public CreateConveyorHandler(IIORepository ioRepo, IConveyorRepository conveyorRepo, ISlotRepository slotRepo)
        {
            _ioRepo = ioRepo;
            _conveyorRepo = conveyorRepo;
            _slotRepo = slotRepo;
        }

        public async Task<Guid> Handle(CreateConveyorCommand request, CancellationToken cancellationToken)
        {
            if (!await _slotRepo.SlotExists(request.SlotId))
                throw new NotFoundException("Slot does not exist");

            var ioFactory = new IOFactory(_ioRepo);
            var io = await ioFactory.GetOrCreateIO(request.Bit, request.Byte,request.SlotId, IOType.Output);

            var factory = new ConveyorFactory(_conveyorRepo);
            var conveyor = await factory.CreateConveyor(request.PosX, request.PosY, request.Length, request.Frequency, request.IsVertical, request.IsTurnedDownOrLeft, io.Id,request.NegativeLogic);
            var id = await _conveyorRepo.AddConveyor(conveyor);

            return id;
        }
    }
}

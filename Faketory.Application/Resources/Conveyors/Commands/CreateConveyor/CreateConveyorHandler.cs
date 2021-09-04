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
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Commands.CreateConveyor
{
    public class CreateConveyorHandler : IRequestHandler<CreateConveyorCommand>
    {
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IConveyingPointRepository _conveyingPointRepo;
        private readonly IIORepository _ioRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;

        public CreateConveyorHandler(IUserRepository userRepo, IIORepository ioRepo, IConveyingPointRepository conveyingPointRepo, IConveyorRepository conveyorRepo, ISlotRepository slotRepo)
        {
            _userRepo = userRepo;
            _ioRepo = ioRepo;
            _conveyorRepo = conveyorRepo;
            _conveyingPointRepo = conveyingPointRepo;
            _slotRepo = slotRepo;
        }

        public async Task<Unit> Handle(CreateConveyorCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User does not exist");

            if (!await _slotRepo.SlotExists(request.SlotId))            //sprawdzić czy slot jest tego usera
                throw new NotFoundException("Slot does not exist");

            Guid ioId;

            if (!await _ioRepo.IOExists(request.SlotId, request.Byte, request.Bit, IOType.Output))
            {
                var io = new IO()
                {
                    Bit = request.Bit,
                    Byte = request.Byte,
                    SlotId = request.SlotId,
                    Type = IOType.Output
                };
                ioId = await _ioRepo.CreateIO(io);
            }
            else
            {
                ioId = (await _ioRepo.GetIO(request.SlotId, request.Byte, request.Bit, IOType.Output)).Id;
            }

            var factory = new ConveyorFactory(_conveyingPointRepo);
            var conveyor = await factory.CreateConveyor(request.PosX, request.PosY, request.Length, request.Frequency, request.IsVertical, request.IsTurnedDownOrLeft, request.UserEmail,ioId);
            await _conveyorRepo.AddConveyor(conveyor);

            return Unit.Value;
        }
    }
}

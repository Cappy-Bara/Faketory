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
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Commands.UpdateConveyor
{
    public class UpdateConveyorHandler : IRequestHandler<UpdateConveyorCommand, Unit>
    {
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IIORepository _ioRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;

        public UpdateConveyorHandler(IUserRepository userRepo, IIORepository ioRepo, IConveyorRepository conveyorRepo, ISlotRepository slotRepo)
        {
            _userRepo = userRepo;
            _ioRepo = ioRepo;
            _conveyorRepo = conveyorRepo;
            _slotRepo = slotRepo;
        }

        public async Task<Unit> Handle(UpdateConveyorCommand request, CancellationToken cancellationToken)
        {
            var conveyor = await _conveyorRepo.GetConveyor(request.ConveyorId);
            if (conveyor == null)
                throw new NotFoundException("This conveyor does not exist");

            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User does not exist");

            if (!await _slotRepo.SlotExists(request.SlotId))
            {
                throw new NotFoundException("Slot does not exist");
            }//sprawdzić czy slot jest tego usera

            var ioFactory = new IOFactory(_ioRepo);
            var IO = await ioFactory.GetOrCreateIO(request.Bit,request.Byte,request.SlotId,IOType.Output);

            var factory = new ConveyorFactory(_conveyorRepo, request.UserEmail);

            conveyor = await factory.UpdateConveyor
                (conveyor,request.PosX,request.PosY,request.Length,request.Frequency,
                request.IsVertical,request.IsTurnedDownOrLeft,IO.Id,request.NegativeLogic);

            await _conveyorRepo.UpdateConveyor(conveyor);
            return Unit.Value;
        }
    }
}

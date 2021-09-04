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
        private readonly IConveyingPointRepository _conveyingPointRepo;
        private readonly IIORepository _ioRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;

        public UpdateConveyorHandler(IUserRepository userRepo, IIORepository ioRepo, IConveyingPointRepository conveyingPointRepo, IConveyorRepository conveyorRepo, ISlotRepository slotRepo)
        {
            _userRepo = userRepo;
            _ioRepo = ioRepo;
            _conveyorRepo = conveyorRepo;
            _conveyingPointRepo = conveyingPointRepo;
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

            Guid ioId = conveyor.IOId;
            if (request.SlotId != conveyor.IO.SlotId || request.Byte != conveyor.IO.Byte || request.Bit != conveyor.IO.Bit)
            {
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
            }

            var factory = new ConveyorFactory(_conveyingPointRepo);

            bool conveyorPointsWillChange = factory.ConveyorPointsWillChange(conveyor, request.PosX, request.PosY, request.IsVertical, request.IsTurnedDownOrLeft, request.Length);
            
            if (conveyorPointsWillChange)
                await _conveyingPointRepo.RemoveConveyingPoints(conveyor.ConveyingPoints);

            conveyor = await factory.UpdateConveyor
                (conveyor,request.PosX,request.PosY,request.Length,request.Frequency,
                request.IsVertical,request.IsTurnedDownOrLeft,ioId);
            
            if (conveyorPointsWillChange)
                await _conveyingPointRepo.AddConveyingPoints(conveyor.ConveyingPoints);

            await _conveyorRepo.UpdateConveyor(conveyor);
            return Unit.Value;
        }
    }
}

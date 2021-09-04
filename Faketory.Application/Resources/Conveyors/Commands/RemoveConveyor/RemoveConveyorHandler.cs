using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Commands.RemoveConveyor
{
    public class RemoveConveyorHandler : IRequestHandler<RemoveConveyorCommand>
    {
        private readonly IConveyorRepository _conveyorRepo;

        public RemoveConveyorHandler(IConveyorRepository conveyorRepo)
        {
            _conveyorRepo = conveyorRepo;
        }

        public async Task<Unit> Handle(RemoveConveyorCommand request, CancellationToken cancellationToken)
        {
            if (!await _conveyorRepo.ConveyorExists(request.ConveyorId))
                throw new NotFoundException("Conveyor with this Id does not exist");

            await _conveyorRepo.RemoveConveyor(request.ConveyorId);
            return Unit.Value;
        }
    }
}

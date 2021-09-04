using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Queries.GetConveyor
{
    public class GetConveyorHandler : IRequestHandler<GetConveyorQuery, Conveyor>
    {
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IUserRepository _userRepo;

        public GetConveyorHandler(IUserRepository userRepo, IConveyorRepository conveyorRepo)
        {
            _userRepo = userRepo;
            _conveyorRepo = conveyorRepo;
        }

        public async Task<Conveyor> Handle(GetConveyorQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.Email))
            {
                throw new NotFoundException("User does not exist!");
            }

            var conveyor = await _conveyorRepo.GetConveyor(request.ConveyorId);
            if (conveyor == null)
            {
                throw new NotFoundException("This conveyor does not exist.");
            }

            if (conveyor.UserEmail != request.Email)
            {
                throw new BadRequestException("This conveyor is not yours!");
            }

            return conveyor;
        }
    }
}

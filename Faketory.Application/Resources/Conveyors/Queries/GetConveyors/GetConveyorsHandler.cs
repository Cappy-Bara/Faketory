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

namespace Faketory.Application.Resources.Conveyors.Queries.GetConveyors
{
    public class GetConveyorsHandler : IRequestHandler<GetConveyorsQuery, List<Conveyor>>
    {
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IUserRepository _userRepo;

        public GetConveyorsHandler(IUserRepository userRepo, IConveyorRepository conveyorRepo)
        {
            _userRepo = userRepo;
            _conveyorRepo = conveyorRepo;
        }

        public async Task<List<Conveyor>> Handle(GetConveyorsQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.Email))
                throw new NotFoundException("This user does not exist!");

            return await _conveyorRepo.GetAllUserConveyors(request.Email);
        }
    }
}

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

        public GetConveyorsHandler(IConveyorRepository conveyorRepo)
        {
            _conveyorRepo = conveyorRepo;
        }

        public async Task<List<Conveyor>> Handle(GetConveyorsQuery request, CancellationToken cancellationToken)
        {
            return await _conveyorRepo.GetAllUserConveyors();
        }
    }
}

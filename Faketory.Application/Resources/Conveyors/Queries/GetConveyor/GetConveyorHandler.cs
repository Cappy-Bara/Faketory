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

        public GetConveyorHandler(IConveyorRepository conveyorRepo)
        {
            _conveyorRepo = conveyorRepo;
        }

        public async Task<Conveyor> Handle(GetConveyorQuery request, CancellationToken cancellationToken)
        {
            var conveyor = await _conveyorRepo.GetConveyor(request.ConveyorId);
            if (conveyor == null)
            {
                throw new NotFoundException("This conveyor does not exist.");
            }

            return conveyor;
        }
    }
}
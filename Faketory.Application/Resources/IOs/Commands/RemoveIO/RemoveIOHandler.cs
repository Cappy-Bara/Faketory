using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.RemoveIO
{
    public class RemoveIOHandler : IRequestHandler<RemoveIOCommand, Unit>
    {
        private readonly IIORepository _IORepo;

        public RemoveIOHandler(IIORepository iORepo)
        {
            _IORepo = iORepo;
        }

        public async Task<Unit> Handle(RemoveIOCommand request, CancellationToken cancellationToken)
        {
            if (! await _IORepo.IOExists(request.Id))
            {
                throw new NotFoundException("This IO does not exist.");
            }
            await _IORepo.RemoveIO(request.Id);
            return Unit.Value;
        }
    }
}

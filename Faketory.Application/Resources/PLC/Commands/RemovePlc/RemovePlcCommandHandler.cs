using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Infrastructure.Repositories;
using MediatR;

namespace Faketory.Application.Resources.PLC.Commands.RemovePlc
{
    public class RemovePlcCommandHandler : IRequestHandler<RemovePlcCommand, Unit>
    {
        private readonly IPlcEntityRepository _entityRepo;
        private readonly IPlcRepository _plcRepo;

        public RemovePlcCommandHandler(IPlcRepository plcRepo, IPlcEntityRepository entityRepo)
        {
            _plcRepo = plcRepo;
            _entityRepo = entityRepo;
        }

        public async Task<Unit> Handle(RemovePlcCommand request, CancellationToken cancellationToken)
        {
            if (!await _entityRepo.DeletePlc(request.PlcId))
                throw new NotFoundException("Plc does not exist.");
            await _plcRepo.DeletePlc(request.PlcId);

            return Unit.Value;
        }
    }
}

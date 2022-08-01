using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.PLC.Commands.ConnectToPlc
{
    public class ConnectToPlcHandler : IRequestHandler<ConnectToPlcCommand, bool>
    {
        private readonly IPlcRepository _plcRepo;
        private readonly IPlcEntityRepository _entityRepo;

        public ConnectToPlcHandler(IPlcEntityRepository entityRepo, IPlcRepository plcRepo)
        {
            _entityRepo = entityRepo;
            _plcRepo = plcRepo;
        }

        public async Task<bool> Handle(ConnectToPlcCommand request, CancellationToken cancellationToken)
        {
            var plcEntity = await _entityRepo.GetPlcById(request.PlcId);
            if (plcEntity == null)
                throw new NotFoundException("Plc not found.");

            if (!_plcRepo.PlcExists(request.PlcId))
                _plcRepo.CreatePlc(plcEntity);

            return await _plcRepo.ConnectToPlc(request.PlcId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.PLC.Commands.CreatePlc
{
    public class CreatePlcHandler : IRequestHandler<CreatePlcCommand, PlcEntity>
    {
        private readonly IPlcRepository _plcRepo;
        private readonly IPlcEntityRepository _entityRepo;
        private readonly IPlcModelRepository _modelRepo;
        private readonly IUserRepository _userRepo;

        public CreatePlcHandler(IPlcEntityRepository entityRepo, IPlcRepository plcRepo, IPlcModelRepository modelRepo, IUserRepository userRepo)
        {
            _entityRepo = entityRepo;
            _plcRepo = plcRepo;
            _modelRepo = modelRepo;
            _userRepo = userRepo;
        }

        public async Task<PlcEntity> Handle(CreatePlcCommand request, CancellationToken cancellationToken)
        {
            if(!await _userRepo.UserExists(request.UserEmail))
            {
                throw new NotFoundException("User does not exist!");
            }

            if (!await _modelRepo.ModelExists(request.ModelId))
                throw new NotCreatedException("This model does not exist!");

            var data = new PlcEntity()
            {
                Ip = request.Ip,
                ModelId =request.ModelId,
                UserEmail = request.UserEmail,
            };

            var output = await _entityRepo.CreatePlc(data);
            if (output == null || output.Model == null)
                throw new NotCreatedException("Wrong Plc data!");
            await _plcRepo.CreatePlc(output);
            return output;
        }
    }
}

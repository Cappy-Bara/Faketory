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

namespace Faketory.Application.Resources.PLC.Queries.GetUserPlcs
{
    public class GetUserPlcsHandler : IRequestHandler<GetUserPlcsQuery, IEnumerable<PlcEntity>>
    {
        private readonly IPlcEntityRepository _plcRepo;
        private readonly IUserRepository _userRepo;

        public GetUserPlcsHandler(IPlcEntityRepository plcRepo, IUserRepository userRepo)
        {
            _plcRepo = plcRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<PlcEntity>> Handle(GetUserPlcsQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User not found.");

            return await _plcRepo.GetUserPlcs(request.UserEmail);
        }
    }
}

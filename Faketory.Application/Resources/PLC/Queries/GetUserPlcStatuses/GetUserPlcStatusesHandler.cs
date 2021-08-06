using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.PLC.Queries.GetUserPlcStatuses
{
    public class GetUserPlcStatusesHandler : IRequestHandler<GetUserPlcStatusesQuery, IEnumerable<PlcConnectionStatus>>
    {
        public IPlcRepository _plcRepo { get; set; }
        public IUserRepository _userRepo { get; set; }
        public IPlcEntityRepository _entityRepo { get; set; }

        public GetUserPlcStatusesHandler(IPlcEntityRepository entityRepo, IUserRepository userRepo, IPlcRepository plcRepo)
        {
            _entityRepo = entityRepo;
            _userRepo = userRepo;
            _plcRepo = plcRepo;
        }
        //TODO - Spróbować Yield Return
        public async Task<IEnumerable<PlcConnectionStatus>> Handle(GetUserPlcStatusesQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.Email))
                throw new NotFoundException("User does not exist.");

            var plcIds = (await _entityRepo.GetUserPlcs(request.Email)).Select(x => x.Id);

            var output = new List<PlcConnectionStatus>();

            foreach (Guid id in plcIds)
            {
                output.Add(
                 new PlcConnectionStatus()
                 {
                     PlcId = id,
                     Status = await _plcRepo.IsConnected(id)
                 });
            }
            return output;
        }
    }
}

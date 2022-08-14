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

        public GetUserPlcsHandler(IPlcEntityRepository plcRepo)
        {
            _plcRepo = plcRepo;
        }

        public async Task<IEnumerable<PlcEntity>> Handle(GetUserPlcsQuery request, CancellationToken cancellationToken)
        {
            return await _plcRepo.GetUserPlcs();
        }
    }
}

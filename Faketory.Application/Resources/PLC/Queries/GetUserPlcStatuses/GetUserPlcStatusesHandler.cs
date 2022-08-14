﻿using System;
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

        public GetUserPlcStatusesHandler(IPlcEntityRepository entityRepo, IPlcRepository plcRepo)
        {
            _entityRepo = entityRepo;
            _plcRepo = plcRepo;
        }
        public async Task<IEnumerable<PlcConnectionStatus>> Handle(GetUserPlcStatusesQuery request, CancellationToken cancellationToken)
        {
            var plcIds = (await _entityRepo.GetUserPlcs()).Select(x => x.Id);

            var output = new List<PlcConnectionStatus>();

            foreach (Guid id in plcIds)
            {
                if (_plcRepo.PlcExists(id))
                {
                    output.Add(
                     new PlcConnectionStatus()
                     {
                         PlcId = id,
                         Status = _plcRepo.IsConnected(id)
                     });
                }
                else
                {
                    output.Add(
                     new PlcConnectionStatus()
                     {
                         PlcId = id,
                         Status = false
                     });
                }
            }
            return output;
        }
    }
}

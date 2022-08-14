using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using MediatR;

namespace Faketory.Application.Resources.PLC.Queries.GetUserPlcStatuses
{
    public class GetUserPlcStatusesQuery : IRequest<IEnumerable<PlcConnectionStatus>>
    {
    }
}

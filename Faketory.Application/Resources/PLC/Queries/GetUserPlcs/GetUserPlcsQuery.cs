using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.PLC.Queries.GetUserPlcs
{
    public class GetUserPlcsQuery :IRequest<IEnumerable<PlcEntity>>
    {
        public string UserEmail { get; set; }
    }
}

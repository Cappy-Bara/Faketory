using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.Slots.Queries.GetAllUserSlots
{
    public class GetAllUserSlotsQuery :IRequest<IEnumerable<Slot>>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.Slots.Queries.GetAllUserSlots
{
    public class GetAllUserSlotsHandler : IRequestHandler<GetAllUserSlotsQuery, IEnumerable<Slot>>
    {
        private readonly ISlotRepository _slotRepo;

        public GetAllUserSlotsHandler(ISlotRepository slotRepo)
        {
            _slotRepo = slotRepo;
        }

        public async Task<IEnumerable<Slot>> Handle(GetAllUserSlotsQuery request, CancellationToken cancellationToken)
        {
            return await _slotRepo.GetUserSlots();
        }
    }
}

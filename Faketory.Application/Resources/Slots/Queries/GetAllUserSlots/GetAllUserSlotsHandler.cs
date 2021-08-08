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

namespace Faketory.Application.Resources.Slots.Queries.GetAllUserSlots
{
    public class GetAllUserSlotsHandler : IRequestHandler<GetAllUserSlotsQuery, IEnumerable<Slot>>
    {
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;

        public GetAllUserSlotsHandler(ISlotRepository slotRepo, IUserRepository userRepo)
        {
            _slotRepo = slotRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Slot>> Handle(GetAllUserSlotsQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.Id))
                throw new NotFoundException("User does not exist.");

            return await _slotRepo.GetUserSlots(request.Id);
        }
    }
}

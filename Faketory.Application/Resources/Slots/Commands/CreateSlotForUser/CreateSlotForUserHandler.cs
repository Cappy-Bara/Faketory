using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.CreateSlotForUser
{
    public class CreateSlotForUserHandler : IRequestHandler<CreateSlotForUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly ISlotRepository _slotRepo;

        public CreateSlotForUserHandler(ISlotRepository slotRepo, IUserRepository userRepo)
        {
            _slotRepo = slotRepo;
            _userRepo = userRepo;
        }

        public async Task<Unit> Handle(CreateSlotForUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User does not exist!");

            await _slotRepo.CreateSlotForUser(request.UserEmail);
            return Unit.Value;
        }
    }
}

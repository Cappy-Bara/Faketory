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
        private readonly ISlotRepository _slotRepo;

        public CreateSlotForUserHandler(ISlotRepository slotRepo)
        {
            _slotRepo = slotRepo;
        }

        public async Task<Unit> Handle(CreateSlotForUserCommand request, CancellationToken cancellationToken)
        {
            await _slotRepo.CreateSlotForUser();
            return Unit.Value;
        }
    }
}

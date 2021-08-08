using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.DeleteSlotById
{
    public class DeleteSlotByIdHandler : IRequestHandler<DeleteSlotByIdCommand, Unit>
    {
        private readonly ISlotRepository _repo;
        public DeleteSlotByIdHandler(ISlotRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteSlotByIdCommand request, CancellationToken cancellationToken)
        {
            if (!await _repo.RemoveSlot(request.Id))
                throw new NotFoundException("Slot not found.");
            return Unit.Value;
        }
    }
}

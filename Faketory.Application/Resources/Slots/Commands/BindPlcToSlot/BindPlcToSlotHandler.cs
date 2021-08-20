using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.BindPlcToSlot
{
    public class BindPlcToSlotHandler : IRequestHandler<BindPlcToSlotCommand, Unit>
    {
        private readonly IPlcEntityRepository _entityRepo;
        private readonly ISlotRepository _slotRepo;

        public BindPlcToSlotHandler(IPlcEntityRepository entityRepo, ISlotRepository slotRepo)
        {
            _entityRepo = entityRepo;
            _slotRepo = slotRepo;
        }

        public async Task<Unit> Handle(BindPlcToSlotCommand request, CancellationToken cancellationToken)
        {
            if (!await _entityRepo.PlcExists(request.PlcId))
                throw new NotFoundException("Plc Doesn't exist.");
            if (!await _slotRepo.SlotExists(request.SlotId))
                throw new NotFoundException("Slot Doesn't exist.");

            await _slotRepo.UnbindPlcFromSlot(request.PlcId);

            await _slotRepo.BindPlcWithSlot(request.SlotId, request.PlcId);
            return Unit.Value;
        }
    }
}

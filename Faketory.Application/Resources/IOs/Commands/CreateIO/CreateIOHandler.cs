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

namespace Faketory.Application.Resources.IOs.Commands.CreateIO
{
    public class CreateIOHandler : IRequestHandler<CreateIOCommand, Unit>
    {
        private readonly IIORepository _IORepo;
        private readonly ISlotRepository _slotRepo;

        public CreateIOHandler(IIORepository iORepo, ISlotRepository slotRepo)
        {
            _IORepo = iORepo;
            _slotRepo = slotRepo;
        }

        public async Task<Unit> Handle(CreateIOCommand request, CancellationToken cancellationToken)
        {
            if (await _IORepo.IOExists(request.SlotId, request.Byte, request.Bit, request.Type))
                throw new NotCreatedException("IO connected to this point already exists.");

            if (!await _slotRepo.SlotExists(request.SlotId))
                throw new NotFoundException("Thist slot doesn't exist.");


            await _IORepo.CreateIO(request.GetIO());
            return Unit.Value;
        }
    }
}

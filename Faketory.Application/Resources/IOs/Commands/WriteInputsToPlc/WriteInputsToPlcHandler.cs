using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.WriteInputsToPlc
{
    public class WriteInputsToPlcHandler : IRequestHandler<WriteInputsToPlcCommand, Unit>
    {
        private readonly IPlcRepository _plcRepo;
        private readonly IIORepository _ioRepo;
        private readonly ISlotRepository _slotRepo;

        public WriteInputsToPlcHandler(ISlotRepository slotRepo, IIORepository ioRepo, IPlcRepository plcRepo)
        {
            _slotRepo = slotRepo;
            _ioRepo = ioRepo;
            _plcRepo = plcRepo;
        }

        public async Task<Unit> Handle(WriteInputsToPlcCommand request, CancellationToken cancellationToken)
        {
            foreach (string slotIdAsString in request.SlotIds)
            {
                if (!Guid.TryParse(slotIdAsString, out var slotId))
                    continue;

                if (!await _slotRepo.SlotExists(slotId))
                    continue;

                var inputs = await _ioRepo.GetSlotInputs(slotId);
                if (!inputs.Any())
                {
                    continue;
                }

                //TODO - DODAĆ PLC ID DO SLOTU, ŻEBY UNIKNĄĆ NIEPOTRZEBNEGO ZAPYTANIA?
                var plcId = (await _slotRepo.GetSlotById(slotId)).PlcId ?? Guid.Empty;

                if(plcId != Guid.Empty && await _plcRepo.PlcExists(plcId) && await _plcRepo.IsConnected(plcId))
                {
                    foreach (IO io in inputs)
                    {
                        await _plcRepo.WriteToPlc(plcId, io.Byte, io.Bit, io.Value);
                    }
                }
                await _ioRepo.UpdateIOs(inputs);
            }
            return Unit.Value;
        }
    }
}




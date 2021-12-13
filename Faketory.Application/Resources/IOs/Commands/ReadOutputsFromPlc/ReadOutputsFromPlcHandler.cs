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

namespace Faketory.Application.Resources.IOs.Commands.ReadOutputsFromPlc
{
    public class ReadOutputsFromPlcHandler : IRequestHandler<ReadOutputsFromPlcCommand, Unit>
    {
        private readonly IPlcRepository _plcRepo;
        private readonly IIORepository _ioRepo;
        private readonly ISlotRepository _slotRepo;

        public ReadOutputsFromPlcHandler(ISlotRepository slotRepo, IIORepository ioRepo, IPlcRepository plcRepo)
        {
            _slotRepo = slotRepo;
            _ioRepo = ioRepo;
            _plcRepo = plcRepo;
        }

        public async Task<Unit> Handle(ReadOutputsFromPlcCommand request, CancellationToken cancellationToken)
        {
            foreach (string slotIdAsString in request.SlotIds)
            {
                //TODO - DODAĆ PLC ID DO SLOTU, ŻEBY UNIKNĄĆ NIEPOTRZEBNEGO ZAPYTANIA?

                if (!Guid.TryParse(slotIdAsString, out var slotId))
                    continue;

                if (!await _slotRepo.SlotExists(slotId))
                    continue;

                var outputs = await _ioRepo.GetSlotOutputs(slotId);
                if (!outputs.Any())
                {
                    continue;
                }

                var plcId = (await _slotRepo.GetSlotById(slotId)).PlcId ?? Guid.Empty;

                if (plcId == Guid.Empty || !await _plcRepo.PlcExists(plcId) || !await _plcRepo.IsConnected(plcId))
                {
                    foreach (IO io in outputs)
                    {
                        io.Value = false;
                    }
                }
                else
                {
                    foreach (IO io in outputs)
                    {
                        io.Value = await _plcRepo.ReadFromPlc(plcId, io.Byte, io.Bit);
                    }
                }
                await _ioRepo.UpdateIOs(outputs);
            }
            return Unit.Value;
        }
    }
}

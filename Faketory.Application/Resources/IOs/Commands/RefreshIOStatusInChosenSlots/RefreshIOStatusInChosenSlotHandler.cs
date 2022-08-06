using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots
{
    public class RefreshIOStatusInChosenSlotHandler : IRequestHandler<WriteInputsToPlcQuery, Unit>
    {
        private readonly IPlcRepository _plcRepo;
        private readonly IIORepository _ioRepo;
        private readonly ISlotRepository _slotRepo;

        public RefreshIOStatusInChosenSlotHandler(ISlotRepository slotRepo, IIORepository ioRepo, IPlcRepository plcRepo)
        {
            _slotRepo = slotRepo;
            _ioRepo = ioRepo;
            _plcRepo = plcRepo;
        }

        public async Task<Unit> Handle(WriteInputsToPlcQuery request, CancellationToken cancellationToken)
        {
            foreach (string stringId in request.SlotIds)
            {
                //TODO - DODAĆ PLC ID DO SLOTU, ŻEBY UNIKNĄĆ NIEPOTRZEBNEGO ZAPYTANIA?
                //TODO - ROZDZIELENIE I OD O?
                //TODO - zwracać listę Slotów co ich nie aktualizuje

                if (!Guid.TryParse(stringId,out var id))
                    continue;


                if (!await _slotRepo.SlotExists(id))
                    continue;

                var ios = await _ioRepo.GetSlotIOs(id);
                if (!ios.Any())
                {
                    continue;
                }

                var plcId = (await _slotRepo.GetSlotById(id)).PlcId ?? Guid.Empty;

                if (plcId == Guid.Empty || !_plcRepo.PlcExists(plcId) || !_plcRepo.IsConnected(plcId))
                {
                    foreach (IO io in ios)
                    {
                        if (io.Type == IOType.Output)
                            io.Value = false;
                    }
                }
                else
                {
                    foreach (IO io in ios)
                    {
                        if (io.Type == IOType.Output)
                            io.Value = await _plcRepo.ReadFromPlc(plcId, io.Byte, io.Bit);
                        if (io.Type == IOType.Input)
                            await _plcRepo.WriteToPlc(plcId, io.Byte, io.Bit, io.Value);
                    }
                }

                await _ioRepo.UpdateIOs(ios);
            }
            return Unit.Value;
        }
    }
}

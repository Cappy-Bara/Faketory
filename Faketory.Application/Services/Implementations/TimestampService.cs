using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Application.Resources.IOs.Commands.ReadOutputsFromPlc;
using Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots;
using Faketory.Application.Resources.IOs.Commands.WriteInputsToPlc;
using Faketory.Application.Resources.Slots.Queries.GetAllUserSlots;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampService : ITimestampService
    {
        private readonly Scene _scene;
        private readonly IMediator _mediator;

        public TimestampService(Scene scene, IMediator mediator)
        {
            _scene = scene;
            _mediator = mediator;
        }


        public async Task<ModifiedUtils> Timestamp(string userEmail)
        {
            var slotsQuery = new GetAllUserSlotsQuery()
            {
                Id = userEmail,
            };
            var slots = await _mediator.Send(slotsQuery);

            await ReadFromOutputs(slots);

            var modifiedUtils = await _scene.HandleTimestamp(userEmail);

            await WriteToInputs(slots);

            return modifiedUtils;
        }

        private async Task WriteToInputs(IEnumerable<Slot> slots)
        {
            var writeCommand = new WriteInputsToPlcCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(writeCommand);
        }

        private async Task ReadFromOutputs(IEnumerable<Slot> slots)
        {
            var readOutputs = new ReadOutputsFromPlcCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(readOutputs);
        }


    }
}

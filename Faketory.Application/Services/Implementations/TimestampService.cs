using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots;
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

            await RefreshIO(slots);

            var modifiedUtils = await _scene.HandleTimestamp(userEmail);

            await RefreshIO(slots);

            return modifiedUtils;
        }

        private async Task RefreshIO(IEnumerable<Slot> slots)
        {
            var refreshIOCommand = new RefreshIOStatusInChosenSlotsCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(refreshIOCommand);
        }
    }
}

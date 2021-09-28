using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots;
using Faketory.Application.Resources.Slots.Queries.GetAllUserSlots;
using Faketory.Application.Resources.Timestamps.Responses;
using Faketory.Application.Services.Interfaces;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampService : ITimestampService
    {
        private readonly IPalletRepository _palletRepo;
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IConveyingPointRepository _CPRepo;
        private readonly IMediator _mediator;
        private readonly ISensorRepository _sensorRepo;

        public TimestampService(IMediator mediator, ISensorRepository sensorRepo, IConveyingPointRepository cPRepo, IConveyorRepository conveyorRepo, IPalletRepository palletRepo)
        {
            _mediator = mediator;
            _sensorRepo = sensorRepo;
            _CPRepo = cPRepo;
            _conveyorRepo = conveyorRepo;
            _palletRepo = palletRepo;
        }

        public async Task<DynamicUtils> Timestamp(string userEmail)
        {
            //refresh IO statuses!
            var slotsQuery = new GetAllUserSlotsQuery()
            {
                Id = userEmail,
            };
            var slots = await _mediator.Send(slotsQuery);

            var refreshIOCommand = new RefreshIOStatusInChosenSlotsCommand()
            {
                SlotIds = slots.Select(x => x.Id.ToString()).ToArray()
            };
            await _mediator.Send(refreshIOCommand);

            var sceneHandler = new SceneHandler(_palletRepo, _conveyorRepo, _CPRepo, _sensorRepo, userEmail);
            await sceneHandler.Timestamp();

            await _mediator.Send(refreshIOCommand);

            var output = new DynamicUtils()
            {
                Pallets = await _palletRepo.GetAllUserPallets(userEmail)
            };
            return output;
        }
    }
}

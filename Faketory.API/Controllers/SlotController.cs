using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.Slot;
using Faketory.Application.Resources.Slots.Commands.BindPlcToSlot;
using Faketory.Application.Resources.Slots.Commands.CreateSlotForUser;
using Faketory.Application.Resources.Slots.Commands.DeleteSlotById;
using Faketory.Application.Resources.Slots.Queries.GetAllUserSlots;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlotController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SlotController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation("Creates next slot fot user. Slot numer is generated.")]
        public async Task<ActionResult> CreateSlotForUser()
        {
            var command = new CreateSlotForUserCommand();

            await _mediator.Send(command);

            return Created("", null);        //TODO - POPRAWIĆ ZWROTKĘ, OGRANICZYĆ ILOŚĆ TWORZONYCH SLOTÓW.
        }                                   //JEST NIEOPTYMALNE.

        [HttpDelete]
        [SwaggerOperation("Removes slot with given Id.")]
        public async Task<ActionResult> RemoveSlot([FromQuery] Guid id)
        {
            var command = new DeleteSlotByIdCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Returns all user's slots.")]
        public async Task<ActionResult<ReturnSlotsDto>> GetUserSlots()
        {
            var query = new GetAllUserSlotsQuery();

            var slots = await _mediator.Send(query);

            if (!slots.Any())
                return NoContent();

            return Ok(new ReturnSlotsDto()
            {
                Slots = _mapper.Map<IEnumerable<ReturnSlotDto>>(slots)
            });
        }

        [HttpPatch]
        [SwaggerOperation("Binds the plc to chosen slot.")]
        public async Task<ActionResult> ConnectPlcWithSlot([FromQuery]Guid plcId,[FromQuery]Guid slotId)
        {
            var Command = new BindPlcToSlotCommand()
            {
                SlotId = slotId,
                PlcId = plcId,
            };

            await _mediator.Send(Command);

            return Ok();
        }
    }
}

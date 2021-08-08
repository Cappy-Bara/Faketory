using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("[controller]")]
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
        public async Task<ActionResult> CreateSlotForUser([FromQuery] string email)
        {
            var command = new CreateSlotForUserCommand()
            {
                UserEmail = email,
            };

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

        [HttpGet("{email}")]
        [SwaggerOperation("Returns all user's Plcs.")]
        public async Task<ActionResult> GetUserSlots([FromRoute] string email)
        {
            var query = new GetAllUserSlotsQuery()
            {
                Id = email
            };

            var slots = await _mediator.Send(query);

            if (slots == null)
                return NoContent();

            return Ok(new ReturnSlotsDto()
            {
                Slots = _mapper.Map<IEnumerable<ReturnSlotDto>>(slots)
            });
        }

        [HttpPut]
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

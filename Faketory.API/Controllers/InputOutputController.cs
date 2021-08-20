using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.IOs;
using Faketory.API.Dtos.IOs.Requests;
using Faketory.Application.Resources.IOs.Commands.CreateIO;
using Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots;
using Faketory.Application.Resources.IOs.Commands.RemoveIO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InputOutputController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InputOutputController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [SwaggerOperation("Creates IO. Throws exception if IO already exists. Not really used in app.")]
        [HttpPost]
        public async Task<ActionResult> CreteIO([FromQuery]CreateIODto dto)
        {
            var command = _mapper.Map<CreateIOCommand>(dto);

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPatch]
        [SwaggerOperation("Refreshes the IO status in chosen slots.")]
        //TODO - NOT OPTIMAL
        public async Task<ActionResult> UpdateIOStatesInSlots([FromBody]UpdateIOStatusesDto dto)
        {
            var command = _mapper.Map<RefreshIOStatusInChosenSlotsCommand>(dto);

            if (!command.SlotIds.Any())
                return NoContent();

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation("Removes chosen IO.")]
        public async Task<ActionResult> RemoveIO([FromQuery] Guid id)
        {
            var command = new RemoveIOCommand()
            {
                Id = id,
            };

            await _mediator.Send(command);
            return Ok();
        }


    }
}

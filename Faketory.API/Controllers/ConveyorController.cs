using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.Conveyors.Requests;
using Faketory.API.Dtos.Conveyors.Responses;
using Faketory.Application.Resources.Conveyors.Commands.CreateConveyor;
using Faketory.Application.Resources.Conveyors.Commands.RemoveConveyor;
using Faketory.Application.Resources.Conveyors.Commands.UpdateConveyor;
using Faketory.Application.Resources.Conveyors.Queries.GetConveyor;
using Faketory.Application.Resources.Conveyors.Queries.GetConveyors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConveyorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserDataProvider _dataProvider;

        public ConveyorController(IMediator mediator, IMapper mapper, IUserDataProvider dataProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dataProvider = dataProvider;
        }

        //SPRAWDZIĆ CZY DODAJE CP DO BAZY AUTOMATYCZNIE, BO ISTNIEJE RELACJA!
        //USUNĄĆ CONSTRUCTOR, FACTORY W TESTACH

        [HttpPost]
        [SwaggerOperation("Creates Conveyor and conveying points in database.")]
        public async Task<ActionResult> CreateConveyor([FromBody] CreateConveyorDto dto) 
        {
            var email = _dataProvider.UserEmail();

            var command = new CreateConveyorCommand()
            {
                Bit = dto.Bit ?? 0,
                Byte = dto.Byte ?? 0,
                Frequency = dto.Frequency ?? 0,
                IsTurnedDownOrLeft = dto.IsTurnedDownOrLeft ?? false,
                IsVertical = dto.IsVertical ?? false,
                Length = dto.Length ?? 0,
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                SlotId = dto.SlotId ?? Guid.Empty,
                UserEmail = email,
                NegativeLogic = dto.NegativeLogic ?? false
            };

            var id = await _mediator.Send(command);
            return Created("", id);
        }

        [HttpDelete]
        [SwaggerOperation("Removes Conveyor and conveying points from database.")]
        public async Task<ActionResult> RemoveConveyor([FromBody] RemoveConveyorDto dto)
        {
            var command = new RemoveConveyorCommand() { ConveyorId = dto.ConveyorId ?? Guid.Empty };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [SwaggerOperation("Changes conveyor and conveying points in database.")]
        public async Task<ActionResult> UpdateConveyor([FromBody] UpdateConveyorDto dto)
        {
            var command = new UpdateConveyorCommand()
            {
                UserEmail = _dataProvider.UserEmail(),
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                IsTurnedDownOrLeft = dto.IsTurnedDownOrLeft ?? false,
                Bit = dto.Bit ?? 0,
                Byte = dto.Byte ?? 0,
                ConveyorId = dto.ConveyorId ?? Guid.Empty,
                Frequency = dto.Frequency ?? 0,
                IsVertical = dto.IsVertical ?? false,
                Length = dto.Length ?? 0,
                SlotId = dto.SlotId ?? Guid.Empty,
                NegativeLogic = dto.NegativeLogic ?? false,
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Returns conveyor with given Id")]
        public async Task<ActionResult<ConveyorDto>> GetConveyor([FromQuery]GetConveyorDto dto)
        {
            var query = new GetConveyorQuery()
            {
                ConveyorId = dto.Id,
                Email = _dataProvider.UserEmail(),
            };

            var conveyor = await _mediator.Send(query);
            if (conveyor is null)
                return NoContent();

            var output = _mapper.Map<ConveyorDto>(conveyor);

            return Ok(output);
        }

        [HttpGet("all")]
        [SwaggerOperation("Returns all user conveyors")]
        public async Task<ActionResult<ConveyorsDto>> GetConveyors()
        {
            var query = new GetConveyorsQuery()
            {
                Email = _dataProvider.UserEmail(),
            };

            var conveyors = await _mediator.Send(query);
            if (!conveyors.Any())
                return NoContent();

            var output = new ConveyorsDto()
            {
                Conveyors = _mapper.Map<List<ConveyorDto>>(conveyors)
            };
            return Ok(output);
        }
    }
}

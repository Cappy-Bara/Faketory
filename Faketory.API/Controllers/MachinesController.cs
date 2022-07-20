using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.Machine.Requests;
using Faketory.API.Dtos.Machine.Responses;
using Faketory.Application.Resources.Machines.Commands.CreateMachine;
using Faketory.Application.Resources.Machines.Commands.DeleteMachine;
using Faketory.Application.Resources.Machines.Commands.UpdateMachine;
using Faketory.Application.Resources.Machines.Queries.GetMachine;
using Faketory.Application.Resources.Machines.Queries.GetMachines;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IUserDataProvider _userDataProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public MachinesController(IMediator mediator, IMapper mapper, IUserDataProvider userDataProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userDataProvider = userDataProvider;
        }

        [HttpPost]
        [SwaggerOperation("Creates machine in position choosen by user.")]
        public async Task<ActionResult> CreateMachine([FromBody] CreateMachineDto dto)
        {
            var command = new CreateMachineCommand()
            {
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                UserEmail = _userDataProvider.UserEmail(),
                ProcessingTimestampAmount = dto.ProcessingTimestampAmount ?? 0,
                RandomFactor = dto.RandomFactor ?? 0,
            };

            var id = await _mediator.Send(command);
            return Created("", id);
        }

        [HttpDelete]
        [SwaggerOperation("Removes machine with given id")]
        public async Task<ActionResult> RemoveMachine([FromBody] RemoveMachineDto dto)
        {
            var command = new DeleteMachineCommand()
            {
                Id = dto.MachineId ?? Guid.Empty,
                UserEmail = _userDataProvider.UserEmail(),
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Returns machine with given Id.")]
        public async Task<ActionResult<MachineDto>> GetMachine([FromQuery] GetMachineDto dto)
        {
            var command = new GetMachineQuery()
            {
                UserEmail = _userDataProvider.UserEmail(),
                MachineId = dto.MachineId ?? Guid.Empty
            };

            var machine = await _mediator.Send(command);

            if (machine is null)
                return NoContent();

            var output = _mapper.Map<MachineDto>(machine);

            return Ok(output);
        }

        [HttpPut]
        [SwaggerOperation("Updates machine with given Id.")]
        public async Task<ActionResult> UpdateMachine([FromBody] UpdateMachineDto dto)
        {
            var command = new UpdateMachineCommand()
            {
                UserEmail = _userDataProvider.UserEmail(),
                MachineId = dto.Id ?? Guid.Empty,
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                ProcessingTimestampAmount = dto.ProcessingTimestampAmount ?? 0,
                RandomFactor = dto.RandomFactor ?? 0
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("all")]
        [SwaggerOperation("Returns all user machines")]
        public async Task<ActionResult<MachinesDto>> GetMachines()
        {
            var command = new GetMachinesQuery()
            {
                UserEmail = _userDataProvider.UserEmail()
            };

            var machines = await _mediator.Send(command);

            if (machines is null || !machines.Any())
                return NoContent();

            var output = new MachinesDto()
            {
                Machines = _mapper.Map<List<MachinesDto>>(machines)
            };

            return Ok(output);
        }
    }
}

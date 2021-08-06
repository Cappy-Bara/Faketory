using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos;
using Faketory.Application.Resources.PLC.Commands.ConnectToPlc;
using Faketory.Application.Resources.PLC.Commands.CreatePlc;
using Faketory.Application.Resources.PLC.Commands.RemovePlc;
using Faketory.Application.Resources.PLC.Queries.GetUserPlcs;
using Faketory.Application.Resources.PLC.Queries.GetUserPlcStatuses;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlcController :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PlcController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Creates Plc Entity in Db and Plc object in Dictionary.")]
        public async Task<ActionResult<GetPlcResponse>> CreatePlc([FromBody] CreatePlcDto dto)
        {
            var command = _mapper.Map<CreatePlcCommand>(dto);
            var plcs = await _mediator.Send(command);

            var output = _mapper.Map<GetPlcResponse>(plcs);

            return Created($"Plc/{output.Id}",output);
        }
        
        [HttpGet]
        [SwaggerOperation("Returns all user's PLCs.")]
        public async Task<ActionResult<GetPlcsResponse>> GetAllUserPlcs([FromQuery] string email)
        {
            var command = new GetUserPlcsQuery()
            {
                UserEmail = email
            };

            var plcs = await _mediator.Send(command);
            if (plcs == null)
                return NoContent();

            var output = new GetPlcsResponse()
            {
                Plcs = _mapper.Map<IEnumerable<GetPlcResponse>>(plcs),
            };

            return Ok(output);
        }

        [HttpDelete]
        [SwaggerOperation("Removes user's PLC from database and Dictionary.")]
        public async Task<ActionResult> DeletePlc([FromQuery] Guid plcId)
        {
            var command = new RemovePlcCommand()
            {
                PlcId = plcId
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [SwaggerOperation("Connects user with chosen PLC.")]
        public async Task<ActionResult> ConnectToPlc([FromQuery] Guid plcId)
        {
            var command = new ConnectToPlcCommand()
            {
                PlcId = plcId,
            };

            if(await _mediator.Send(command))
                return Ok();
            return Conflict("Connection Failed");
        }

        [HttpGet("/Connections")]
        [SwaggerOperation("Returns all user connections with PLCs.")]
        public async Task<ActionResult<PlcsWithStatusesDto>> GetUserPlcsStatuses(string userEmail)
        {
            var query = new GetUserPlcStatusesQuery()
            {
                Email = userEmail
            };

            var statuses = await _mediator.Send(query);

            var output = new PlcsWithStatusesDto()
            {
                Plcs = _mapper.Map<IEnumerable<PlcWithStatusDto>>(statuses)
            };
            return Ok(output);
        }
    }
}

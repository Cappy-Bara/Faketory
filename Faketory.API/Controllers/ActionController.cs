using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.ActionResponses;
using Faketory.API.Dtos.Conveyors.Responses;
using Faketory.API.Dtos.Machine.Responses;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.API.Dtos.Sensors.Responses;
using Faketory.Application.Resources.Conveyors.Queries.GetConveyors;
using Faketory.Application.Resources.Machines.Queries.GetMachines;
using Faketory.Application.Resources.Pallets.Query.GetPallets;
using Faketory.Application.Resources.Sensors.Queries.GetSensors;
using Faketory.Application.Services.Implementations;
using Faketory.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly TimestampOrchestrator _timestampOrchestrator;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ActionController(TimestampOrchestrator timestampOrchestrator, IMapper mapper, IMediator mediator)
        {
            _timestampOrchestrator = timestampOrchestrator;
            _mapper = mapper;
            _mediator = mediator;
        }

        [SwaggerOperation("Simulates one unit of time. Moves elements, Read/write IOs, etc.")]
        [HttpPost("timestamp")]
        public async Task<ActionResult<TimestampResponse>> Timestamp()
        {
            var data = await _timestampOrchestrator.Timestamp();

            return Ok(new TimestampResponse
            {
                Pallets = _mapper.Map<IEnumerable<PalletDto>>(data.Pallets),
                Conveyors = data.Conveyors,
                Sensors = data.Sensors,
                Machines = data.Machines,
            });
        }

        [SwaggerOperation("Returns all logged user static objects (everything excluding pallets)")]
        [HttpGet("elements/static")]
        public async Task<ActionResult<StaticElementsResponse>> StaticElements()
        {
            var conveyorsQuery = new GetConveyorsQuery();
            var conveyors = await _mediator.Send(conveyorsQuery);

            var sensorsQuery = new GetSensorsQuery();
            var sensors = await _mediator.Send(sensorsQuery);

            var machinesQuery = new GetMachinesQuery();
            var machines = await _mediator.Send(machinesQuery);

            return Ok(new StaticElementsResponse
            {
                Conveyors = _mapper.Map<List<ConveyorDto>>(conveyors),
                Sensors = _mapper.Map<List<SensorDto>>(sensors),
                Machines = _mapper.Map<List<MachineDto>>(machines)
            });
        }

        [HttpGet("elements/all")]
        [SwaggerOperation("Returns all logged user objects")]
        public async Task<ActionResult<AllElementsResponse>> AllUserElements()
        {
            var conveyorsQuery = new GetConveyorsQuery();
            var conveyors = await _mediator.Send(conveyorsQuery);

            var sensorsQuery = new GetSensorsQuery();
            var sensors = await _mediator.Send(sensorsQuery);

            var palletsQuery = new GetPalletsQuery();
            var pallets = await _mediator.Send(palletsQuery);

            var machinesQuery = new GetMachinesQuery();
            var machines = await _mediator.Send(machinesQuery);

            return Ok(new AllElementsResponse
            {
                Conveyors = _mapper.Map<List<ConveyorDto>>(conveyors),
                Sensors = _mapper.Map<List<SensorDto>>(sensors),
                Pallets = _mapper.Map<List<PalletDto>>(pallets),
                Machines = _mapper.Map<List<MachineDto>>(machines)
            });
        }
    }
}

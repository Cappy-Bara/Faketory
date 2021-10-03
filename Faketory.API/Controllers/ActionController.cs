using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos;
using Faketory.API.Dtos.ActionResponses;
using Faketory.API.Dtos.Conveyors.Responses;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.API.Dtos.Sensors.Responses;
using Faketory.Application.Resources.Conveyors.Queries.GetConveyors;
using Faketory.Application.Resources.Sensors.Queries.GetSensors;
using Faketory.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly ITimestampService _timestampService;
        private readonly IUserDataProvider _dataProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ActionController(ITimestampService timestampService, IUserDataProvider dataProvider, IMapper mapper, IMediator mediator)
        {
            _timestampService = timestampService;
            _dataProvider = dataProvider;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("timestamp")]
        public async Task<ActionResult> Timestamp()
        {
            var email = _dataProvider.UserEmail();
            var data = await _timestampService.Timestamp(email);

            return Ok(new TimestampResponse
            {
                Pallets = _mapper.Map<List<PalletDto>>(data.Pallets)
            });
        }

        [HttpGet("elements/static")]
        public async Task<ActionResult> StaticElements()
        {
            var email = _dataProvider.UserEmail();

            var conveyorsQuery = new GetConveyorsQuery { Email = email };
            var conveyors = await _mediator.Send(conveyorsQuery);

            var sensorsQuery = new GetSensorsQuery { UserEmail = email };
            var sensors = await _mediator.Send(sensorsQuery);

            return Ok(new StaticElementsResponse
            {
                Conveyors = _mapper.Map<List<ConveyorDto>>(conveyors),
                Sensors = _mapper.Map<List<SensorDto>>(sensors)
            });;
        }
    }
}

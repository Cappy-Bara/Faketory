using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.Pallets.Requests;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.API.Dtos.Sensors.Reqests;
using Faketory.API.Dtos.Sensors.Responses;
using Faketory.Application.Resources.Pallets.Commands;
using Faketory.Application.Resources.Pallets.Commands.CreatePallet;
using Faketory.Application.Resources.Pallets.Commands.RemovePallet;
using Faketory.Application.Resources.Pallets.Query.GetPallet;
using Faketory.Application.Resources.Pallets.Query.GetPallets;
using Faketory.Application.Resources.Sensors.Commands;
using Faketory.Application.Resources.Sensors.Commands.CreateSensor;
using Faketory.Application.Resources.Sensors.Commands.RemoveSensor;
using Faketory.Application.Resources.Sensors.Commands.UpdateSensor;
using Faketory.Application.Resources.Sensors.Queries.GetSensor;
using Faketory.Application.Resources.Sensors.Queries.GetSensors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly IUserDataProvider _userDataProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SensorController(IMediator mediator, IMapper mapper, IUserDataProvider userDataProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userDataProvider = userDataProvider;
        }

        [HttpPost]
        [SwaggerOperation("Creates sensor in position choosen by user.")]
        public async Task<ActionResult> CreateSensor([FromBody] CreateSensorDto dto)
        {
            var command = new CreateSensorCommand()
            {
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                UserEmail = _userDataProvider.UserEmail(),
                SlotId = dto.SlotId ?? Guid.Empty,
                Bit = dto.Bit ?? 0,
                Byte = dto.Byte ?? 0,
            };

            await _mediator.Send(command);
            return Created("", null);
        }

        [HttpDelete]
        [SwaggerOperation("Removes sensor with given id")]
        public async Task<ActionResult> RemoveSensor([FromBody] RemoveSensorDto dto)
        {
            var command = new RemoveSensorCommand()
            {
                SensorId = dto.SensorId,
                UserEmail = _userDataProvider.UserEmail(),
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Returns sensor with given Id.")]
        public async Task<ActionResult> GetSensor([FromQuery] GetSensorDto dto)
        {
            var command = new GetSensorQuery()
            {
                UserEmail = _userDataProvider.UserEmail(),
                SensorId = dto.SensorId
            };

            var sensor = await _mediator.Send(command);

            if (sensor is null)
                return NoContent();

            var output = _mapper.Map<SensorDto>(sensor);

            return Ok(output);
        }

        [HttpPut]
        [SwaggerOperation("Changes existing sensor")]
        public async Task<ActionResult> UpdateSensor([FromBody] UpdateSensorDto dto)
        {
            var command = new UpdateSensorCommand()
            {
                UserEmail = _userDataProvider.UserEmail(),
                SensorId = dto.SensorId ?? Guid.Empty,
                Bit = dto.Bit ?? 0,
                Byte = dto.Byte ?? 0,
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                SlotId = dto.SlotId ?? Guid.Empty,
                NegativeLogic = dto.NegativeLogic
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("all")]
        [SwaggerOperation("Returns all user sensors")]
        public async Task<ActionResult> GetSensors()
        {
            var command = new GetSensorsQuery()
            {
                UserEmail = _userDataProvider.UserEmail()
            };

            var sensors = await _mediator.Send(command);

            if (sensors is null || !sensors.Any())
                return NoContent();

            var output = new SensorsDto()
            {
                Sensors = _mapper.Map<List<SensorDto>>(sensors)
            };

            return Ok(output);
        }
    }
}

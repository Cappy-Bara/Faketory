using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos;
using Faketory.Application.Resources.PLC.Commands.CreatePlc;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<PlcEntity>> CreatePlc([FromBody] CreatePlcDto dto)
        {
            var command = _mapper.Map<CreatePlcCommand>(dto);
            return await _mediator.Send(command);
        }










    }
}

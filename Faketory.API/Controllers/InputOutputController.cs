using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.IOs;
using Faketory.Application.Resources.IOs.Commands.CreateIO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
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
        public async Task<ActionResult> CreteIO([FromBody]CreateIODto dto)
        {
            var command = _mapper.Map<CreateIOCommand>(dto);

            await _mediator.Send(command);
            return Ok();
        }

        public async Task<ActionResult> UpdateIOStatesInSlot()
        {
        }




    }
}

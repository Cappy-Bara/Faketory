using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.Application.Resources.Slots.Commands.CreateSlotForUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateSlotForUser([FromQuery] string email)
        {
            var command = new CreateSlotForUserCommand()
            {
                UserEmail = email,
            };

            await _mediator.Send(command);

            return Created("",null);        //TODO - POPRAWIĆ ZWROTKĘ, OGRANICZYĆ ILOŚĆ
        }

        public Task<ActionResult> RemoveSlot()
        {
            ;
        }

        public Task<ActionResult> GetUserSlots()
        {
            ;
        }

        public Task<ActionResult> ConnectPlcWithSlot()
        {
            ;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos.Pallets.Requests;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.Application.Resources.Pallets.Commands;
using Faketory.Application.Resources.Pallets.Commands.CreatePallet;
using Faketory.Application.Resources.Pallets.Commands.RemovePallet;
using Faketory.Application.Resources.Pallets.Query.GetPallet;
using Faketory.Application.Resources.Pallets.Query.GetPallets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PalletController : ControllerBase
    {
        private readonly IUserDataProvider _userDataProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PalletController(IMediator mediator, IMapper mapper, IUserDataProvider userDataProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userDataProvider = userDataProvider;
        }

        [HttpPost]
        [SwaggerOperation("Creates pallet in position choosen by user.")]
        public async Task<ActionResult> CreatePallet([FromBody]CreatePalletDto dto)
        {
            var command = new CreatePalletCommand()
            {
                PosX = dto.PosX ?? 0,
                PosY = dto.PosY ?? 0,
                UserEmail = _userDataProvider.UserEmail()
            };

            await _mediator.Send(command);
            return Created("",null);
        }

        [HttpDelete]
        [SwaggerOperation("Removes pallet with given id")]
        public async Task<ActionResult> RemovePallet([FromBody] RemovePalletDto dto)
        {
            var command = new RemovePalletCommand()
            {
                PalletId = dto.PalletId,
                UserEmail = _userDataProvider.UserEmail(),
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Returns pallet with given Id.")]
        public async Task<ActionResult> GetPallet([FromQuery] GetPalletDto dto)
        {
            var command = new GetPalletQuery()
            {
                UserEmail = _userDataProvider.UserEmail(),
                PalletId = dto.PalletId
            };

            var pallet = await _mediator.Send(command);

            if (pallet is null)
                return NoContent();

            var output = _mapper.Map<PalletDto>(pallet);

            return Ok(output);
        }

        [HttpGet("all")]
        [SwaggerOperation("Returns all user pallets")]
        public async Task<ActionResult> GetPallets()
        {
            var command = new GetPalletsQuery()
            {
                UserEmail = _userDataProvider.UserEmail()
            };

            var pallets = await _mediator.Send(command);

            if (pallets is null || !pallets.Any())
                return NoContent();

            var output = new PalletsDto()
            {
                Pallets = _mapper.Map<List<PalletDto>>(pallets)
            };

            return Ok(output);
        }
    }
}

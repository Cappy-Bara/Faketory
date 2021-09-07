using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using Faketory.API.Dtos;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TimestampController : ControllerBase
    {
        private readonly ITimestampService _timestampService;
        private readonly IUserDataProvider _dataProvider;
        private readonly IMapper _mapper;

        public TimestampController(ITimestampService timestampService, IUserDataProvider dataProvider, IMapper mapper)
        {
            _timestampService = timestampService;
            _dataProvider = dataProvider;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Timestamp()
        {
            var email = _dataProvider.UserEmail();
            var data = await _timestampService.Timestamp(email);

            return Ok(new TimestampResponse
            {
                Pallets = _mapper.Map<List<PalletDto>>(data.Pallets)
            });
        }
    }
}

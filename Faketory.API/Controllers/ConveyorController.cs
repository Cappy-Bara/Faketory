using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Authentication.DataProviders.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConveyorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserDataProvider _dataProvider;

        public ConveyorController(IMediator mediator, IMapper mapper, IUserDataProvider dataProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dataProvider = dataProvider;
        }

        public async Task<ActionResult> CreateConveyor() 
        { 
        //Sprawdź czy jest IO, jak jest to dodaj, jak nie ma to stwórz nowe, a potem conveyor
        //i  dodaj tez conveying points do database
        
        
        }









    }
}

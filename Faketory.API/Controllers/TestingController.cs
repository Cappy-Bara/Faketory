using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Faketory.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TestingController : ControllerBase
    {
        [HttpGet("ex1")]
        public async Task<ActionResult> Get()
        {
            throw new DomainException("Middleware Test", 404);
        }

        [HttpGet("ex2")]
        public async Task<ActionResult> Get2()
        {
            throw new DomainException("Second One", 400);
        }

        [HttpGet("ok")]
        public async Task<ActionResult> GetOK()
        {
            return Ok();
        }
    }
}

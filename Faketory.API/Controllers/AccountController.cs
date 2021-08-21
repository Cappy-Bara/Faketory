using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Authentication;
using Faketory.API.Dtos.Users;
using Faketory.Application.Resources.Users.LoginUser;
using Faketory.Application.Resources.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Faketory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly AuthenticationSettings _authSettings;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, AuthenticationSettings settings)
        {
            _mediator = mediator;
            _authSettings = settings;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var command = new RegisterUserCommand()
            {
                Email = dto.Email,
                Password = dto.Password
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto dto)
        {
            var command = new LoginUserCommand()
            {
                Email = dto.Email,
                Password = dto.Password
            };

            var claims = await _mediator.Send(command);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authSettings.JwtIssuer, _authSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var output = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(output);
        }







    }
}

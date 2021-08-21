using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Users.LoginUser
{
    public class LoginUserCommand : IRequest<List<Claim>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

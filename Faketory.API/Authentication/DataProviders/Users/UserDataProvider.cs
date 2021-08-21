using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Faketory.API.Authentication.DataProviders.Users
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserDataProvider(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string UserEmail()
        {
            return _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Authentication.DataProviders.Users
{
    public interface IUserDataProvider
    {
        public string UserEmail();
    }
}

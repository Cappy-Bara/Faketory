using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Resources.PLCRelated
{
    public class User
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}

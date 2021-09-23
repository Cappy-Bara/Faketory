using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Exceptions
{
    public class OccupiedException : DomainException
    {
        public OccupiedException(string message) : base(message, 409)
        {
        }
    }
}

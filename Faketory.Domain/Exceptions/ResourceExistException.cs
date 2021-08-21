using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Exceptions
{
    public class ResourceExistException : DomainException
    {
        public ResourceExistException(string message) : base(message, 409)
        {

        }
    }
}

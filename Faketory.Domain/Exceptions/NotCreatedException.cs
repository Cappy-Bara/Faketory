using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Exceptions
{
    public class NotCreatedException : DomainException
    {
        public NotCreatedException(string message) : base(message, 400)
        {
        }
    }
}

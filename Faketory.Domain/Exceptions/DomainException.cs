using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Faketory.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode { get; set; }

        public DomainException(string message, int code) : base(message)
        {
            StatusCode = code;
        }
    }
}

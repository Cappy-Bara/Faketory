﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Middleware.ExceptionHandlingMiddleware
{
    public class ResponseDetails
    {
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}

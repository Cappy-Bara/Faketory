﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Plc.Requests
{
    public class CreatePlcDto
    {
        public string Ip { get; set; }
        public int ModelId { get; set; }
    }
}

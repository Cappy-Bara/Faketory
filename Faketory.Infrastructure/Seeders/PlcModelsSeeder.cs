﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;
using S7.Net;

namespace Faketory.Infrastructure.Seeders
{
    public static class PlcModelsSeeder
    {
        public static PlcModel[] GetData()
        {
            return new PlcModel[]
            {
                new PlcModel()
                {
                    CpuModel = 1200,
                    Cpu = CpuType.S71200,
                    Rack = 0,
                    Slot = 1
                },
                new PlcModel()
                {
                    CpuModel = 1500,
                    Cpu = CpuType.S71500,
                    Rack = 0,
                    Slot = 1
                },
                new PlcModel()
                {
                    CpuModel = 300,
                    Cpu = CpuType.S7300,
                    Rack = 0,
                    Slot = 2
                },
                new PlcModel()
                {
                    CpuModel = 400,
                    Cpu = CpuType.S7400,
                    Rack = 0,
                    Slot = 2
                }
            };
        }
    }
}

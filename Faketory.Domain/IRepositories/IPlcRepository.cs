﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface IPlcRepository
    {
        public void CreatePlc(PlcEntity entity);
        public void DeletePlc(Guid id);
        public Task<bool> ConnectToPlc(Guid id);
        public bool PlcExists(Guid id);
        public bool IsConnected(Guid id);
        public Task WriteToPlc(Guid id, int @byte, int @bit, bool value);
        public Task<bool> ReadFromPlc(Guid id, int @byte, int @bit);
    }
}

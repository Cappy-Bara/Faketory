using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Machine.Responses
{
    public class MachineDto
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool IsProcessing { get; private set; }
        public int ProcessingTimestampAmount { get; set; }
        public int RandomFactor { get; set; }
    }
}

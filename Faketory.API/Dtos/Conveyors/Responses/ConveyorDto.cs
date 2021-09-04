using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Conveyors.Responses
{
    public class ConveyorDto
    {
        public Guid Id { get; set; }
        private int PosX { get; set; }
        private int PosY { get; set; }
        private int Length { get; set; }
        private bool IsVertical { get; set; }
        private bool IsTurnedDownOrLeft { get; set; }
        public bool IsRunning { get; set; }
        public int Frequency { get; set; }
        public Guid Slot { get; set; }
        public int Byte { get; set; }
        public int Bit { get; set; }
    }
}

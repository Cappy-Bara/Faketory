using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Pallets.Requests
{
    public class UpdatePalletDto
    {
        public Guid PalletId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}

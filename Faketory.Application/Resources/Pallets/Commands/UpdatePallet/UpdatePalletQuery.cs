using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.UpdatePallet
{
    public class UpdatePalletQuery : IRequest<Unit>
    {
        public string UserEmail { get; set; }
        public Guid PalletId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}

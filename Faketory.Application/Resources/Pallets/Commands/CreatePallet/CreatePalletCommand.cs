using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.CreatePallet
{
    public class CreatePalletCommand : IRequest<bool>
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string UserEmail { get; set; }
    }
}

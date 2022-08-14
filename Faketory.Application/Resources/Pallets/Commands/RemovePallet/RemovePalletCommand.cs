using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.RemovePallet
{
    public class RemovePalletCommand : IRequest
    {
        public Guid PalletId { get; set; }
    }
}

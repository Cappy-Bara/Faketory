using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Query.GetPallets
{
    public class GetPalletsQuery : IRequest<List<Pallet>>
    {
        public string UserEmail { get; set; }
    }
}

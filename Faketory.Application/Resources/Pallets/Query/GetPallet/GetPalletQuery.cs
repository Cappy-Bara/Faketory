﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Query.GetPallet
{
    public class GetPalletQuery : IRequest<Pallet>
    {
        public Guid PalletId { get; set; }
        public string UserEmail { get; set; }
    }
}

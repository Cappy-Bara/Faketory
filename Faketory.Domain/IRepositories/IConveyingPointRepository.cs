using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.IRepositories
{
    public interface IConveyingPointRepository
    {
        public Task AddConveyorPoints(List<ConveyingPoint> conveyingPoints);
        public Task UpdateConveyingPoints(List<ConveyingPoint> conveyingPoints);
    }
}

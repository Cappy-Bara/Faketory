using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;

namespace Faketory.Domain.Aggregates
{
    public class Scene
    {
        private readonly IPalletRepository _palletRepo;
        private readonly ISensorRepository _sensorRepo;
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IIORepository _iORepository;

        public Scene(IPalletRepository palletRepo, ISensorRepository sensorRepo, IConveyorRepository conveyorRepo, IIORepository iORepository)
        {
            _palletRepo = palletRepo;
            _sensorRepo = sensorRepo;
            _conveyorRepo = conveyorRepo;
            _iORepository = iORepository;
        }

        public Task HandleTimestamp(string userEmail)
        {
            throw new NotImplementedException();










        }
    }
}

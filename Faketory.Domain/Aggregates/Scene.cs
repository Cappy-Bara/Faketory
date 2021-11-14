using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Services;

namespace Faketory.Domain.Aggregates
{
    public class Scene
    {
        private readonly IPalletRepository _palletRepo;
        private readonly ISensorRepository _sensorRepo;
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IIORepository _iORepository;
        private readonly ISlotRepository _slotRepository;

        private List<Conveyor> _userConveyors;
        private List<Sensor> _userSensors;
        private List<Pallet> _userPallets;

        public Scene(IPalletRepository palletRepo, ISensorRepository sensorRepo, IConveyorRepository conveyorRepo, IIORepository iORepository)
        {
            _palletRepo = palletRepo;
            _sensorRepo = sensorRepo;
            _conveyorRepo = conveyorRepo;
            _iORepository = iORepository;
        }

        public async Task HandleTimestamp(string userEmail)
        {
            await GetUserUtils(userEmail);

            //pobranie slotów

            //odświeżenie inputów

            var conveyorService = new ConveyorService(_userConveyors, _userPallets);

            var modifiedConveyors = await conveyorService.HandleConveyorStatusUpdate();

            await conveyorService.HandleConveyorMovement();

            var sensorService = new SensorSenseService(_userPallets,_userSensors);
            sensorService.HandleSensing();

            //odświeżenie inputów po sensorach

            //odświeżenie outputów

            //aktualizacja w DB
        }

        private async Task GetUserUtils(string userEmail)
        {
            _userConveyors = await _conveyorRepo.GetAllUserConveyors(userEmail);
            _userPallets = await _palletRepo.GetAllUserPallets(userEmail);
            _userSensors = await _sensorRepo.GetUserSensors(userEmail);
        }
    }
}

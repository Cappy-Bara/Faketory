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

        private List<Conveyor> _userConveyors;
        private List<Sensor> _userSensors;
        private List<Pallet> _userPallets;

        public Scene(IPalletRepository palletRepo, ISensorRepository sensorRepo, IConveyorRepository conveyorRepo)
        {
            _palletRepo = palletRepo;
            _sensorRepo = sensorRepo;
            _conveyorRepo = conveyorRepo;
        }

        public async Task<ModifiedUtils> HandleTimestamp(string userEmail)
        {
            await GetUserUtils(userEmail);

            var conveyingService = new ConveyingService(_userConveyors, _userPallets);
            await conveyingService.HandleConveyorMovement();

            var sensingService = new SensingService(_userPallets,_userSensors);
            sensingService.HandleSensing();

            await UpdateInDatabase();

            return new ModifiedUtils()
            {
                Sensors = sensingService.ModifiedSensors,
                Pallets = conveyingService.ModifiedPallets,
                Conveyors = conveyingService.ModifiedConveyors,
            };
        }

        private async Task GetUserUtils(string userEmail)
        {
            _userConveyors = await _conveyorRepo.GetAllUserConveyors(userEmail);
            _userPallets = await _palletRepo.GetAllUserPallets(userEmail);
            _userSensors = await _sensorRepo.GetUserSensors(userEmail);
        }
        private async Task UpdateInDatabase()
        {
            await _conveyorRepo.UpdateConveyors(_userConveyors);
            await _palletRepo.UpdatePallets(_userPallets);
            await _sensorRepo.UpdateSensors(_userSensors);
        }
    }
}

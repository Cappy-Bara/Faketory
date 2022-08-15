using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PalletRepository : IPalletRepository
    {
        private readonly Dictionary<Guid, Pallet> _pallets;

        public PalletRepository()
        {
            _pallets = new();
        }

        public Task AddPallet(Pallet pallet)
        {
            pallet.Id = Guid.NewGuid();

            _pallets.Add(pallet.Id,pallet);
            return Task.CompletedTask;
        }

        public Task<List<Pallet>> GetAllUserPallets()
        {
            return Task.FromResult(_pallets.Values.ToList());
        }

        public Task<Pallet> GetPallet(Guid palletId)
        {
            _pallets.TryGetValue(palletId, out var output);
            return Task.FromResult(output);
        }

        public Task<bool> PalletCollides(int posX, int posY)
        {
            return Task.FromResult(_pallets.Values.Any(x => x.PosX == posX && x.PosY == posY));
        }

        public Task RemovePallet(Pallet pallet)
        {
            _pallets.Remove(pallet.Id);
            return Task.CompletedTask;
        }

        public Task UpdatePallet(Pallet pallet)
        {
            _pallets[pallet.Id] = pallet;
            return Task.CompletedTask;
        }

        public Task UpdatePallets(List<Pallet> pallets)
        {
            foreach (var pallet in pallets)
            {
                _pallets[pallet.Id] = pallet;
            }

            return Task.CompletedTask;
        }
    }
}

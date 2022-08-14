using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.IRepositories
{
    public interface IPalletRepository
    {
        public Task<List<Pallet>> GetAllUserPallets();
        public Task UpdatePallets(List<Pallet> pallets);
        public Task UpdatePallet(Pallet pallet);
        public Task AddPallet(Pallet pallet);
        public Task<Pallet> GetPallet(Guid palletId);
        public Task<bool> PalletCollides(int posX, int posY);
        public Task RemovePallet(Pallet pallet);
    }
}

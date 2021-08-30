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
        public Task<List<Pallet>> GetAllUserPallets(string email);
        public Task UpdatePallets(List<Pallet> pallets);
    }
}

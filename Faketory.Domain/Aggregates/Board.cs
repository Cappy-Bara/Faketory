using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class Board
    {
        public List<Pallet> Pallets;
        private Dictionary<(int, int), MovedPallet> _board { get; set; } = new Dictionary<(int, int), MovedPallet>();
    
        //public Task AddPallets(List<MovedPallet> pallet)
        //{
        //    _board.TryAdd()




        //}
    
    
    
    
    
    
    
    
    
    
    
    }

    

















}

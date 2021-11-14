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
        private Dictionary<(int, int), MovedPallet> _board { get; set; } = new Dictionary<(int, int), MovedPallet>();
        public IEnumerable<Pallet> MovedPallets { get => _board.Values.Where(x => x.AlreadyMoved == false).Select(x => x.Pallet); }

        public void AddPallets(List<MovedPallet> pallets)
        {
            foreach(MovedPallet pallet in pallets)
            {
                var addSucceded = _board.TryAdd(pallet.NewPosition, pallet);

                if (addSucceded)
                    continue;

                ResolveConflict(pallet);
            }
        }
        private void ResolveConflict(MovedPallet pallet)
        {
            _board.Remove(pallet.NewPosition, out var presentPallet);

            if (pallet.MovePriority < presentPallet.MovePriority)
            {
                AddPrioritizedAndResolveOther(pallet, presentPallet);
            }

            //TODO - CHECK IF NEEDED
            //else if(pallet.MovePriority == presentPallet.MovePriority)
            //{
            //    if (pallet.AlreadyMoved)
            //        AddPrioritizedAndResolveOther(pallet, presentPallet);
            //    else
            //        AddPrioritizedAndResolveOther(presentPallet, pallet);
            //}

            else
            {
                AddPrioritizedAndResolveOther(presentPallet, pallet);
            }
        }
        private void AddPrioritizedAndResolveOther(MovedPallet palletToAdd, MovedPallet palletToResolve)
        {
            _board.Add(palletToAdd.NewPosition, palletToAdd);
            palletToResolve.UndoMovement();
            var addSucceded = _board.TryAdd(palletToResolve.NewPosition, palletToResolve);
            if (!addSucceded)
                ResolveConflict(palletToResolve);
        }

        public IEnumerable<Pallet> GetPallets()
        {
            return _board.Values.Select(x => x.Pallet);
        }
    }



















}

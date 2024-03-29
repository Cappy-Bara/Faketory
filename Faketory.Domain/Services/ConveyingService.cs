﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Services
{
    public class ConveyingService
    {
        private List<Conveyor> _conveyors { get; set; }
        private List<Pallet> _pallets { get; set; }

        public List<ConveyorState> ModifiedConveyors { get; private set; } = new List<ConveyorState>();
        public List<Pallet> ModifiedPallets { get; private set; } = new List<Pallet>();

        public ConveyingService(List<Conveyor> conveyors, List<Pallet> pallets)
        {
            _conveyors = conveyors ?? new List<Conveyor>();
            _pallets = pallets ?? new List<Pallet>();
        }

        public void HandleConveyorMovement()
        {
            HandleConveyorStatusUpdate();

            var board = new Board();
            var unmovedPallets = new List<Pallet>();

            unmovedPallets.AddRange(_pallets);

            foreach (Conveyor conveyor in _conveyors)
            {
                var conveyorPallets = unmovedPallets.Where(pallet => 
                    conveyor.OccupiedPoints.Any(x => x == (pallet.PosX, pallet.PosY))).ToList();

                conveyorPallets.ForEach(x => unmovedPallets.Remove(x));

                var movedPallets = conveyor.MovePallets(conveyorPallets.ToList());
                board.AddPallets(movedPallets);

                if (!unmovedPallets.Any())
                {
                    break;
                }
            }

            board.AddPallets(unmovedPallets.Select(x => new MovedPallet(x)).ToList());
            ModifiedPallets = board.MovedPallets.ToList();
        }
        private void HandleConveyorStatusUpdate()
        {
            foreach (Conveyor conveyor in _conveyors)
            {
                var changed = conveyor.RefreshStatusAndCheckIfChanged();
                if (changed)
                    ModifiedConveyors.Add(new ConveyorState(conveyor));
            }
        }
    }
}

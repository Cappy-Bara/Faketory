using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Resources.IndustrialParts
{
    public class Machine
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string UserEmail { get; set; }

        public Guid LastProcessedPalletId { get; private set; } = Guid.Empty;
        public bool PalletAlreadyProcessed { get; private set; }
        public int ProcessingTimestampAmount { get; set; }
        public int Ticks { get; set; }
        public int RandomFactor { get; set; }
        public bool IsProcessing { get; private set; }

        public void Process(IEnumerable<Pallet> pallets)
        {
            pallets ??= new List<Pallet>();
            var palletOnMachine = pallets.FirstOrDefault(x => x.PosX == PosX && x.PosY == PosY);

            if (palletOnMachine == null)
            {
                LastProcessedPalletId = Guid.Empty;
                IsProcessing = false;
                Ticks = 0;
                return;
            }

            if (PalletHasChanged(palletOnMachine))
            {
                Ticks = 0;
                IsProcessing=true;
                PalletAlreadyProcessed = false;
            }

            if ((Ticks < ProcessingTimestampAmount + GetDisturbance()) && !PalletAlreadyProcessed)
            {
                Ticks++;
                IsProcessing = true;
                palletOnMachine.Process();
                LastProcessedPalletId = palletOnMachine.Id;
                return;
            }

            IsProcessing = false;
            PalletAlreadyProcessed = true;
            Ticks = 0;
        }
        private bool PalletHasChanged(Pallet pallet)
        {
            return pallet.Id != LastProcessedPalletId;
        }
        private int GetDisturbance()
        {
           var randomNumberGenerator = new Random();
            return randomNumberGenerator.Next(-RandomFactor,RandomFactor);
        }
    }
}

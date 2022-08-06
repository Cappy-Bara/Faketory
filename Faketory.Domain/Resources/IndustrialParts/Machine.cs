using Faketory.Domain.Exceptions;
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

        private Machine()
        {
        }
        public Machine(int posX, int posY, string userEmail, int processingTimestampAmount, int randomFactor)
        {
            PosX = posX;
            PosY = posY;
            UserEmail = userEmail;
            RandomFactor = randomFactor;
            ProcessingTimestampAmount = processingTimestampAmount;

            if (processingTimestampAmount - randomFactor < 0)
                throw new NotCreatedException("Processing timestamp amount including random factor cannot be lower than 0.");
        }

        public void Process(IEnumerable<Pallet> pallets)
        {
            pallets ??= new List<Pallet>();
            var palletOnMachine = pallets.FirstOrDefault(x => x.PosX == PosX && x.PosY == PosY);

            if (palletOnMachine == null)
            {
                LastProcessedPalletId = Guid.Empty;
                Ticks = 0;
                return;
            }

            if (PalletHasChanged(palletOnMachine))
            {
                IsProcessing=true;
                palletOnMachine.Process();
                PalletAlreadyProcessed = false;
                LastProcessedPalletId = palletOnMachine.Id;
                return;
            }
            else if ((Ticks < ProcessingTimestampAmount + GetDisturbance()) && !PalletAlreadyProcessed)
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
            return pallet?.Id != LastProcessedPalletId;
        }
        private int GetDisturbance()
        {
           var randomNumberGenerator = new Random();
            return randomNumberGenerator.Next(-RandomFactor,RandomFactor);
        }
        public void TurnOffIfNoPallets(IEnumerable<Pallet> pallets)
        {
            pallets ??= new List<Pallet>();
            var palletOnMachine = pallets.FirstOrDefault(x => x.PosX == PosX && x.PosY == PosY);

            if (palletOnMachine == null)
            {
                IsProcessing = false;
                return;
            }
            else if (PalletHasChanged(palletOnMachine))
            {
                IsProcessing = true;
            }
        }
    }
}

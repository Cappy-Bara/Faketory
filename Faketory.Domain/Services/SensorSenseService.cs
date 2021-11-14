using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Services
{
    public class SensorSenseService
    {
        private List<Pallet> _pallets { get; set; }
        private List<Sensor> _sensors { get; set; }

        public SensorSenseService(List<Pallet> pallets, List<Sensor> sensors)
        {
            _pallets = pallets ?? new List<Pallet>();
            _sensors = sensors ?? new List<Sensor>();
        }


        public void HandleSensing()
        {
            var activePallets = new List<Pallet>();
            activePallets.AddRange(_pallets);

            foreach (Sensor sensor in _sensors)
            {
                var pallet = sensor.Sense(activePallets);

                if (pallet != null)
                    activePallets.Remove(pallet);

                if (activePallets.Count == 0)
                    break;
            }
        }
    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Services
{
    public class SensorService
    {
        private List<Pallet> _pallets { get; set; }
        private List<Sensor> _sensors { get; set; }

        public List<SensorState> ModifiedSensors { get; set; } = new List<SensorState>();

        public SensorService(List<Pallet> pallets, List<Sensor> sensors)
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
        public void HandleIOStatusUpdate()
        {
            foreach (Sensor sensor in _sensors)
            {
                var sensorModified = sensor.RefreshIOState();
                if (sensorModified)
                    ModifiedSensors.Add(new SensorState(sensor));
            }
        }
    }




}

using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.Seeders;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Infrastructure.DbContexts
{
    public class FaketoryInMemoryDbContext
    {
        private FaketoryDataset _dataset;
        private readonly string _path;

        public FaketoryInMemoryDbContext(IConfiguration configuration)
        {
            _path = configuration.GetValue<string>("StorageFile");
            Load();
        }

        public Dictionary<string, User> Users { get => _dataset.Users; }
        public List<PlcModel> PlcModels { get => _dataset.PlcModels; }
        public Dictionary<Guid, PlcEntity> Plcs { get => _dataset.Plcs; }
        public Dictionary<Guid, Slot> Slots { get => _dataset.Slots; }
        public Dictionary<Guid, IO> InputsOutputs { get => _dataset.InputsOutputs; }
        public Dictionary<Guid, Pallet> Pallets { get => _dataset.Pallets; }
        public Dictionary<Guid, Conveyor> Conveyors { get => _dataset.Conveyors; }
        public Dictionary<Guid, Sensor> Sensors { get => _dataset.Sensors; }
        public Dictionary<Guid, Machine> Machines { get => _dataset.Machines; }
    
        public async Task Persist()
        {
            var serialized = JsonConvert.SerializeObject(_dataset);
            var data = Encoding.ASCII.GetBytes(serialized);
            var filestream = File.Open(_path, FileMode.Create);
            await filestream.WriteAsync(data);
            filestream.Close();
        }
        public void Load()
        {
            if(!File.Exists(_path))
            {
                _dataset = new FaketoryDataset();
                _dataset.PlcModels = PlcModelsSeeder.GetData().ToList();
                return;
            }

            var bytes = File.ReadAllBytes(_path);
            var decoded = Encoding.ASCII.GetString(bytes);
            _dataset = JsonConvert.DeserializeObject<FaketoryDataset>(decoded);
        }
    }
}
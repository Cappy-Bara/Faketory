using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.Factories
{
    public class IOFactory
    {
        private readonly IIORepository _ioRepo;

        public IOFactory(IIORepository ioRepo)
        {
            _ioRepo = ioRepo;
        }

        public async Task<IO> GetOrCreateIO(int @bit, int @byte, Guid slotId, IOType type)
        {
            IO IO;

            if (!await _ioRepo.IOExists(slotId, @byte, @bit, type))
            {
                IO = new IO()
                {
                    Bit = @bit,
                    Byte = @byte,
                    SlotId = slotId,
                    Type = type
                };
                var ioId = await _ioRepo.CreateIO(IO);
                IO.Id = ioId;
            }
            else
            {
                IO = (await _ioRepo.GetIO(slotId, @byte, @bit, type));
            }
            return IO;
        }
    }
}

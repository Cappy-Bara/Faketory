using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faketory.Infrastructure.DbConfigurations
{
    public class PlcModelConfiguration : IEntityTypeConfiguration<PlcModel>
    {
        public void Configure(EntityTypeBuilder<PlcModel> builder)
        {
            builder.ToTable("PlcModels");
            builder.HasKey(x => x.CpuModel);

            builder.HasData(PlcModelsSeeder.GetData());

        }
    }
}

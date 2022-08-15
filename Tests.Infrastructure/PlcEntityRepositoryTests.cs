using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Faketory.Infrastructure.DbContexts;
using Faketory.Domain.Resources.PLCRelated;
using FluentAssertions;
using S7.Net;
using Faketory.Infrastructure.Seeders;
using Faketory.Infrastructure.Repositories.Database;

namespace Tests.Infrastructure
{
    public class PlcEntityRepositoryTests
    {

        public async Task<PlcEntityRepository> GetRepo()
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            context.PlcModels.AddRange(PlcModelsSeeder.GetData().ToList());
            await context.SaveChangesAsync();
            return new PlcEntityRepository(context);
        }



        [Fact]
        public async void RepoTest_PlcAdded_ShouldHaveModel()
        {
            //arrange
            PlcEntity plc = new PlcEntity()
            {
                Ip = "192.168.1.1",
                ModelId = 1200
            };

            var repo = await GetRepo();

            //Act
            var output = await repo.CreatePlc(plc);
            //assert
            output.Model.Should().NotBeNull();
            output.Model.Cpu.Should().Be(CpuType.S71200);
            output.Model.Rack.Should().Be(0);
            output.Model.Slot.Should().Be(1);
        }


        [Fact]
        public async void RepoTest_PlcRemoved_ShouldBeDeleted()
        {
            //arrange
            PlcEntity plc = new PlcEntity()
            {
                Ip = "192.168.1.1",
                ModelId = 1200
            };

            var repo = await GetRepo();
            var output = await repo.CreatePlc(plc);

            //Act
            await repo.DeletePlc(output.Id);

            //assert
            (await repo.GetPlcById(output.Id)).Should().BeNull();
        }
    }
}

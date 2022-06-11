using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Faketory.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests.Infrastructure
{
    public class SlotRepositoryTests
    {

        private async Task<ISlotRepository> GetRepo(List<Slot> slots, List<User> users)
        {
            var options = new DbContextOptionsBuilder<FaketoryDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var context = new FaketoryDbContext(options);
            context.Slots.AddRange(slots);
            await context.SaveChangesAsync();
            return new SlotRepository(context);
        }


        [Theory]
        [InlineData(new int[] {1,2,3})]
        [InlineData(new int[] {1,2,3,4,5,6,7})]
        [InlineData(new int[] {-1,22,14,3,5})]
        [InlineData(new int[] {0,0,1,2,8})]
        [InlineData(new int[] {1,1,1})]
        public async void AddSlot_SlotNumbersTest_ShouldNumerateThemRight(int[] nums)
        {
            var email = "kacper@wp.pl";
            //arrange
            var user = new User { Email = email };
            List<User> users = new() { user };

            var slots = nums.Select(x => new Slot()
            {
                Id = Guid.NewGuid(),
                UserEmail = email,
                Number = x
            }
            ).ToList();

            var repo = await GetRepo(slots,users);
            //act
            await repo.CreateSlotForUser(email);
            var slotNumbers = (await repo.GetUserSlots(email)).Select(x=>x.Number);
            //assert
            slotNumbers.Max().Should().Be(slotNumbers.Count());
            slotNumbers.Min().Should().Be(1);
            slotNumbers.Count().Should().Be(nums.Length+1);
        }
    }
}

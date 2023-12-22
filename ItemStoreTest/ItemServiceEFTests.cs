using FluentAssertions;
using ItemStore.CustomException;
using ItemStore.Entities;
using ItemStore.Interfaces;
using ItemStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemStoreTest
{
    public class ItemServiceEFTests
    {
        [Fact]
        public async Task Get_GivenValidId_ReturnsDto()
        {
            //Arrange
            int id = 1;

            var testRepository = new Mock<IItemRepositoryEF>();
            testRepository.Setup(m => m.GetById(id)).ReturnsAsync(new ItemStore.Entities.ItemEntity
            {
                Id = id
            });

            var itemService = new ItemServiceEF(testRepository.Object);

            // Act
            var result = await itemService.GetById(id);

            // Assert
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = 1;
            var testRepository = new Mock<IItemRepositoryEF>();
            testRepository.Setup(m => m.GetById(id)).Returns(Task.FromResult<ItemEntity?>(null));

            var itemService = new ItemServiceEF(testRepository.Object);

            //Act Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await itemService.GetById(id));
        }
        [Fact]
        public async Task Get_ReturnsItemDtos()
        {
            // Arrange
            //var fakeItems = new List<ItemEntity> { };

            var itemRepositoryMock = new Mock<IItemRepositoryEF>();
            var yourClass = new ItemServiceEF(itemRepositoryMock.Object);

            //Mock the behavior of the item repository
           var fakeItems = new List<ItemEntity>
       {
            new ItemEntity { Id = 1, Name = "Item1", Price = 10.99m },
            new ItemEntity { Id = 2, Name = "Item2", Price = 20.49m }
       };
            itemRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(fakeItems);

            // Act
            var result = await yourClass.Get();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeItems.Count, result.Count);

            // Compare properties of each item in the list
            for (int i = 0; i < fakeItems.Count; i++)
            {
                Assert.Equal(fakeItems[i].Id, result[i].Id);
                Assert.Equal(fakeItems[i].Name, result[i].Name);
                Assert.Equal(fakeItems[i].Price, result[i].Price);

                // Add more assertions based on your specific requirements
            }
        }
    }
}

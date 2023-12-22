using ItemStore.CustomException;
using ItemStore.Dtos;
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
    public class ItemServiceCreateTest
    {
        [Fact]
        public async Task Create_ValidItemDto_CallsRepositoryCreate()
        {
            // Arrange
            var itemDto = new ItemDto
            {
                Id = 1,
                Name = "Test Item",
                Price = 10.99m
            };

            var mockRepository = new Mock<IItemRepositoryEF>();
            var yourClassInstance = new ItemServiceEF(mockRepository.Object);

            // Act
            await yourClassInstance.Create(itemDto);

            // Assert
            mockRepository.Verify(repo => repo.Create(It.IsAny<ItemEntity>()), Times.Once);
        }

        [Fact]
        public async Task Create_NullItemDto_ThrowsItemNotFoundException()
        {
            // Arrange
            var yourClassInstance = new ItemServiceEF(Mock.Of<IItemRepositoryEF>());

            // Act & Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await yourClassInstance.Create(null));
        }

        [Fact]
        public async Task Create_InvalidItemDto_ThrowsInvalidItemException()
        {
            // Arrange
            var invalidItemDto = new ItemDto(); // Assuming this is an invalid state
            //var invalidItemDto = new ItemDto
            //{
            //    Id = 1,
            //    Name = "Test Item",
            //    Price = 10.99m
            //};

            var yourClassInstance = new ItemServiceEF(Mock.Of<IItemRepositoryEF>());

            // Act & Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(async () => await yourClassInstance.Create(invalidItemDto));
        }
    }
}

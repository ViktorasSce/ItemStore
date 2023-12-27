using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using ItemStore.CustomException;
using ItemStore.Dtos;
using ItemStore.Entities;
using ItemStore.Interfaces;
using ItemStore.Repositories;
using ItemStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemStoreTest.ServicesTests
{
    public class ItemServiceEFTests
    {
        private readonly Mock<IItemRepositoryEF> _itemRepositoryMock;
        private readonly ItemServiceEF _itemServiceMock;
        private readonly Fixture _fixture;

        public ItemServiceEFTests()
        {
            _itemRepositoryMock = new Mock<IItemRepositoryEF>();
            _itemServiceMock = new ItemServiceEF(_itemRepositoryMock.Object);
            _fixture = new Fixture();
        }
        [Theory]
        [AutoData]//Autofixture.xunit2
        public async Task Get_GivenValidId_ReturnsDto(ItemEntity entity)
        {
            //Arrange
            //int id = 1;

            //var testRepository = new Mock<IItemRepositoryEF>();
            _itemRepositoryMock.Setup(m => m.GetById(entity.Id)).ReturnsAsync(entity);

            //var itemService = new ItemServiceEF(testRepository.Object);

            // Act
            var result = await _itemServiceMock.GetById(entity.Id);

            // Assert
            result.Id.Should().Be(entity.Id);
        }

        [Fact]
        public async Task Get_GivenInvalidId_ThrowsItemNotFoundException()
        {
            //Arrange
            int id = _fixture.Create<int>();

            _itemRepositoryMock.Setup(m => m.GetById(id)).Returns(Task.FromResult<ItemEntity?>(null));

            //Act Assert
            //await Assert.ThrowsAsync<ItemNotFoundException>(async () => await _itemServiceMock.GetById(id));

            //Act
            Func<Task> act = async () => await _itemServiceMock.GetById(id); //The Func<Task> is used to capture the asynchronous operation that is expected to throw an exception.

            //Fluent Assertions
            await act.Should().ThrowAsync<ItemNotFoundException>();
        }

        [Fact]
        public async Task Get_NullItemDto()
        {
            // Arrange
            var fakeItems = new List<ItemEntity> { };
            //var fakeItems = new List<ItemEntity>
            //{
            //    new ItemEntity { Id = 1, Name = "Item1", Price = 10.99m },
            //    new ItemEntity { Id = 2, Name = "Item2", Price = 20.49m }
            //};

            _itemRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(fakeItems);

            // Act
            var result = await _itemServiceMock.Get();

            // Assert
            result.Should().NotBeNull();
        }

        //Create---------------------------------
        [Theory]
        [AutoData]//Autofixture.xunit2
        public async Task Create_ValidItemDto_CallsRepositoryCreate(ItemDto itemDto)
        {
            // Arrange
            //var itemDto = new ItemDto
            //{
            //    Id = 1,
            //    Name = "Test Item",
            //    Price = 10.99m
            //};

            // Act
            await _itemServiceMock.Create(itemDto);

            // Assert
            _itemRepositoryMock.Verify(repo => repo.Create(It.Is<ItemEntity>(x => x.Name == itemDto.Name && x.Price == itemDto.Price)), Times.Once);
        }

        [Fact]
        public async Task Create_NullItemDto_ThrowsItemNotFoundException()
        {
            //Act
            Func<Task> act = async () => await _itemServiceMock.Create(null);  //The Func<Task> is used to capture the asynchronous operation that is expected to throw an exception.

            //Fluent Assertions
            await act.Should().ThrowAsync<ItemNotFoundException>();
        }

        //Delete--------------------------------------------
        [Fact]
        public async Task Delete_ExistingItemId_DeletesItemAndReturnsNonZero()
        {
            // Arrange
            var id = _fixture.Create<int>();
        
            _itemRepositoryMock.Setup(repo => repo.Delete(id)).ReturnsAsync(1);

            // Act
            await _itemServiceMock.Delete(id);

            // Assert
            _itemRepositoryMock.Verify(repo => repo.Delete(id), Times.Once);
        }

        [Fact]
        public async Task Delete_NonExistingItemId_ThrowsItemNotFoundException()
        {
            // Arrange
            var fakeId = _fixture.Create<int>();

            _itemRepositoryMock.Setup(repo => repo.Delete(fakeId)).ReturnsAsync(0);

            //Act
            Func<Task> act = async () => await _itemServiceMock.Delete(fakeId); //The Func<Task> is used to capture the asynchronous operation that is expected to throw an exception.

            //Fluent Assertions
            await act.Should().ThrowAsync<ItemNotFoundException>();
        }
    }
}

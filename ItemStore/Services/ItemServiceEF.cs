using ItemStore.CustomException;
using ItemStore.Dtos;
using ItemStore.Entities;
using ItemStore.Interfaces;
using ItemStore.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace ItemStore.Services
{
    public class ItemServiceEF
    {
        private readonly IItemRepositoryEF _itemRepository;

        public ItemServiceEF(IItemRepositoryEF itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemDto>> Get()
        {

            var items = await _itemRepository.Get();

            if (!items.Any())
            {
                // Throw a custom exception if items is null
                throw new ItemNotFoundException();
            }

            var itemDtos = items.Select(t => new ItemDto
            {
                Id = t.Id,
                Name = t.Name,
                Price = t.Price,
            }).ToList();

            return itemDtos;
        }

        public async Task<ItemDto> GetById(int id)
        {
            var entity = await _itemRepository.GetById(id) ?? throw new ItemNotFoundException();

            return new ItemDto { Id = entity.Id, Name = entity.Name, Price = entity.Price };

        }

        public async Task Create(ItemDto itemDto)
        {
            if (itemDto == null)
            {
                // Throw a custom exception if items is null
                throw new ItemNotFoundException();
            }
            var entity = new ItemEntity
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await _itemRepository.Create(entity);
        }

        public async Task Edit(ItemDto itemDto)
        {
            var entity = new ItemEntity
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await _itemRepository.Edit(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _itemRepository.Delete(id);
            if (entity == 0)
            {
                throw new ItemNotFoundException();
            }

        }
    }
}
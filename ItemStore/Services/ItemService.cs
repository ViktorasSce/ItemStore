using ItemStore.CustomException;
using ItemStore.Dtos;
using ItemStore.Entities;
using ItemStore.Interfaces;
using ItemStore.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ItemStore.Services
{
    public class ItemService //: IItemService
    {
        //private readonly IItemRepository _itemRepository;
        private readonly ItemRepositoryEF _itemRepository;

        //public ItemService(ItemRepository eItemRepository)
        //{
        //    _itemRepository = itemRepository;
        //    //_eItemRepository = eItemRepository;
        //}
        public ItemService(ItemRepositoryEF itemRepository)
        {
            _itemRepository = itemRepository;
        }

        //public async Task<decimal> BuyItem(int id, int quantity)
        //{
        //    decimal itemPrice = await _itemRepository.BuyItem(id);
        //    decimal totalPrice = itemPrice * quantity;
        //    if (quantity >= 10)
        //    {
        //        totalPrice = totalPrice * 0.9M;
        //    }
        //    else if(quantity >= 20) 
        //    {
        //        totalPrice = totalPrice * 0.8M;
        //    }

        //    return totalPrice;
        //}
        public async Task<List<ItemDto>> Get()
        {

            var items = await _itemRepository.Get();

           

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
            var entity = await _itemRepository.GetById(id);
            if (entity == null)
            {
                throw new ItemNotFoundException();
            }

            return new ItemDto { Id = entity.Id, Name = entity.Name, Price = entity.Price };

        }

        public async Task Create(CreateDto createDto)
        {
            var entity = new ItemEntity
            {
                Name = createDto.Name,
                Price = createDto.Price
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
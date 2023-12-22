using ItemStore.Dtos;
using ItemStore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItemStore.Interfaces
{
    public interface IItemService
    {
        public Task<int> CreateItem(ItemDto itemDto);
        //public Task<IEnumerable<Product>> Items();
        //public Task<ItemEntity> Item(int id);
        public Task<decimal> BuyItem(int id, int quantity);
        //public Task<int> UpdateProductById(UpdateProductByIdDTO updateProductByIdDTO);
        public Task<int> DeleteItem(int id);
        public Task<int> CreateByEntity(ItemDto itemDto);

    }
}

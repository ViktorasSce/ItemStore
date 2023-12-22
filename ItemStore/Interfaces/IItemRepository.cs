using ItemStore.Dtos;
using ItemStore.Entities;

namespace ItemStore.Interfaces
{
    public interface IItemRepository
    {
        Task<int> CreateItem(ItemDto itemDto);
        //public Task<IEnumerable<Product>> Items();
        //public Task<ItemEntity> Item(int id);
        public Task<decimal> BuyItem(int id);
        //public Task<int> UpdateProductById(UpdateProductByIdDTO updateProductByIdDTO);
        public Task<int> DeleteItem(int id);
        public Task<int> CreateByEntity(ItemDto itemDto);
    }
}

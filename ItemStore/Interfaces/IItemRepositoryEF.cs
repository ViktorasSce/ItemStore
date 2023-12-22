using ItemStore.Entities;

namespace ItemStore.Interfaces
{
    public interface IItemRepositoryEF
    {
        Task Create(ItemEntity entity);
        Task<int> Delete(int id);
        Task Edit(ItemEntity itemEntity);
        Task<List<ItemEntity>> Get();
        Task<ItemEntity?> GetById(int id);
    }
}
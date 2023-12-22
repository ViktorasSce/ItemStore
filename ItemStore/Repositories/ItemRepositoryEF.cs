using Dapper;
using ItemStore.Contexts;
using ItemStore.Entities;
using ItemStore.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ItemStore.Repositories
{
    public class ItemRepositoryEF : IItemRepositoryEF
    {
        private readonly DataContext _dataContext;

        public ItemRepositoryEF(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<ItemEntity>> Get()
        {
            return await _dataContext.Items.ToListAsync();
        }

        public async Task<ItemEntity?> GetById(int id)
        {
            var result = await _dataContext.Items.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }

        public async Task Create(ItemEntity entity)
        {
            await _dataContext.Items.AddAsync(entity);
            _dataContext.SaveChanges();
        }

        public async Task Edit(ItemEntity itemEntity)
        {
            int result = await _dataContext.Items.Where(t => t.Id == itemEntity.Id).ExecuteUpdateAsync(s => s
        .SetProperty(b => b.Name, itemEntity.Name)
        .SetProperty(b => b.Price, itemEntity.Price));
        }

        public async Task<int> Delete(int id)
        {
            int result = await _dataContext.Items.Where(t => t.Id == id).ExecuteDeleteAsync();
            return result;
        }
    }
}

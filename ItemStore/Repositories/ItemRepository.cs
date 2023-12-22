using Dapper;
using ItemStore.Contexts;
using ItemStore.Entities;
using ItemStore.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ItemStore.Repositories
{
    public class ItemRepository //: IItemRepository
    {
        //private readonly IDbConnection _connection;
        private readonly DataContext _dataContext;

        public ItemRepository(DataContext dataContext)
        {
            //_connection = connection;
            _dataContext = dataContext;
        }

        //public async Task<int> CreateItem(ItemEntity itemEntity)
        //{
        //    string sql = $"INSERT INTO item (name, price) VALUES (@name, @price)";

        //    int result = await _connection.ExecuteAsync(sql, itemEntity);

        //    return result == 0 ? throw new Exception("Something wrong creating product") : result;
        //}

        //public async Task<IEnumerable<Product>> Items()
        //{
        //    var result = await _connection.QueryAsync<Product>("SELECT * FROM products WHERE deleted_at IS NULL");

        //    return result == null ? throw new Exception("There are no products to display") : result;
        //}

        //public async Task<ItemEntity> Item(int id)
        //{
        //    string sql = "SELECT * FROM item WHERE id = @id";

        //    var result = await _connection.QueryAsync<ItemEntity>(sql, id);

        //    return result.FirstOrDefault() ?? throw new Exception("Something wrong getting product");
        //}

        //public async Task<int> UpdateProductById(UpdateProductByIdDTO updateProductByIdDTO)
        //{
        //    string sql = "UPDATE products SET Name = @name, Description = @description, Picture = @picture, Amount = @amount, Price = @price WHERE id = @id";

        //    int result = await _connection.ExecuteAsync(sql, updateProductByIdDTO);

        //    return result == 0 ? throw new Exception("Something wrong updating product") : result;
        //}

        //public async Task<int> DeleteItem(int id)
        //{
        //    string sql = "UPDATE item SET deleted_at = current_timestamp WHERE id = @id";

        //    int result = await _connection.ExecuteAsync(sql, id);

        //    return result == 0 ? throw new Exception("Something wrong deleting product") : result;
        //}

        //public async Task<decimal> BuyItem(int id)
        //{
        //    string sql = "SELECT price FROM item WHERE id = @id";

        //    decimal result = await _connection.QuerySingleAsync<decimal>(sql, id);

        //    return result == 0 ? throw new Exception("Something wrong getting product") : result;
        //}

        public async Task<List<ItemEntity>> Get()
        {
            return await _dataContext.Items.ToListAsync();
        }

        public async Task<ItemEntity?> GetById(int id)
        {
            var result = await _dataContext.Items.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }

        public async Task CreateByEntity(ItemEntity entity)
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

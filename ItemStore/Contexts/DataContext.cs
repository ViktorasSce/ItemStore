using ItemStore.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<ItemEntity> Items { get; set; }

        public DataContext(DbContextOptions<DataContext>
            options) : base(options)
        {

        }
    }   
}
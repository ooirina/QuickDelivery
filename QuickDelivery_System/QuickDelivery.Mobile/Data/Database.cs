using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickDelivery.Mobile.Models;

namespace QuickDelivery.Mobile.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CartItem>().Wait();
            _database.CreateTableAsync<OrderHistory>().Wait();
        }

        //met pt cos(CartItem)
        public Task<List<CartItem>> GetItemsAsync() => _database.Table<CartItem>().ToListAsync();
        public Task<int> SaveItemAsync(CartItem item) => _database.InsertAsync(item);
        public Task<int> DeleteItemAsync(CartItem item) => _database.DeleteAsync(item);

        //met pt istoric (OrderHistory)
        public Task<int> SaveHistoryAsync(OrderHistory item) => _database.InsertAsync(item);
        public Task<List<OrderHistory>> GetHistoryAsync() => _database.Table<OrderHistory>().ToListAsync();
        public Task<int> UpdateItemAsync(CartItem item)
        {
            return _database.UpdateAsync(item);
        }
        public Task<int> ClearHistoryAsync()
        {
            return _database.DeleteAllAsync<OrderHistory>();
        }



    }
}

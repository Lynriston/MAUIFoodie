using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using MAUIFoodie.Models;

namespace MAUIFoodie.Services
{
    public class DBServices
    {
        public const string DatabaseFilename = "Foodies.db3";

        private SQLiteAsyncConnection _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Foodies.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Food>();
        }

        public async Task<List<Food>> GetFoodItemsAsync()
        {
            await Init();
            return await _database.Table<Food>().ToListAsync();
        }

        public async Task<int> SaveFoodItemAsync(Food item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteFoodItemAsync(Food item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}

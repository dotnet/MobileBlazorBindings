// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using SQLite;

namespace BlazorBindingsToDo
{
    public class TodoItemDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly Func<Task> _dbChanged;

        public TodoItemDatabase(string dbPath, Func<Task> dbChanged)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TodoItem>().Wait();
            _dbChanged = dbChanged ?? throw new ArgumentNullException(nameof(dbChanged));
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            return await _database.Table<TodoItem>().ToListAsync();
        }

        //public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        //{
        //    return await _database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        //public async Task<TodoItem> GetItemAsync(int id)
        //{
        //    return await _database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                var result = await _database.UpdateAsync(item);
                await _dbChanged();
                return result;
            }
            else
            {
                var result = await _database.InsertAsync(item);
                await _dbChanged();
                return result;
            }
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            var result = await _database.DeleteAsync(item);
            await _dbChanged();
            return result;
        }
    }
}

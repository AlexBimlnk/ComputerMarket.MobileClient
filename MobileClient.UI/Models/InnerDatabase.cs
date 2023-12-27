using MobileClient.Logic.Data;
using MobileClient.Logic.Data.Models;

using SQLite;

namespace MobileClient.UI.Models;

public sealed class InnerDatabase : IInnerDatabase
{
    private SQLiteAsyncConnection _database;

    private async Task InitAsync()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.FLAGS);
        var result = await _database.CreateTableAsync<UserModel>();
    }

    public async Task<List<UserModel>> GetItemsAsync()
    {
        await InitAsync();
        return await _database.Table<UserModel>().ToListAsync();
    }

    public async Task<int> SaveItemAsync(UserModel item)
    {
        await InitAsync();
        if (item.Id != 0)
        {
            return await _database.UpdateAsync(item);
        }
        else
        {
            return await _database.InsertAsync(item);
        }
    }

    public async Task<int> DeleteItemAsync(UserModel item)
    {
        await InitAsync();
        return await _database.DeleteAsync(item);
    }
}

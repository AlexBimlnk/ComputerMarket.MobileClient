using MobileClient.Logic.Data.Models;

namespace MobileClient.Logic.Data;

public interface IInnerDatabase
{
    public Task<List<UserModel>> GetItemsAsync();

    public Task<int> SaveItemAsync(UserModel item);

    public Task<int> DeleteItemAsync(UserModel item);
}

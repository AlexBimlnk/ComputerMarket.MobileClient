using SQLite;

namespace MobileClient.Logic.Data.Models;

public class UserModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

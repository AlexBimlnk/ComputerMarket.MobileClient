using SQLite;

namespace MobileClient.Logic.Data.Models;

public class UserModel
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int UserType { get; set; }
}

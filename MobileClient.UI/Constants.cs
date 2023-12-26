namespace MobileClient.UI;

public static class Constants
{
    public const string DATABASE_FILENAME = "ComputerMarketMobileUsers.db3";

    public const SQLite.SQLiteOpenFlags FLAGS =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.Current.AppDataDirectory, DATABASE_FILENAME);
}

namespace SmartFamily.Core.Paths;

public sealed class DatabasePath
{
    public string DatabaseRootPath { get; }

    public string DatabaseContentPath { get; }

    public DatabasePath(string databaseRootPath)
    {
        DatabaseRootPath = databaseRootPath;
        DatabaseContentPath = databaseRootPath;
    }

    public static implicit operator string(DatabasePath databasePath) => databasePath.DatabaseContentPath;
}
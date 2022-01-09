namespace SmartFamily.Core.Contracts.Services
{
    public interface IDatabaseService
    {
        string CreateDatabase(string databaseName);

        string OpenDatabase(string databaseName);
    }
}
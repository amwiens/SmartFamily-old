using SmartFamily.Core.Paths;

namespace SmartFamily.Core.Instance;

/// <summary>
/// Provides module for managing the database instance.
/// <br/>
/// <br/>
/// This API is exposed.
/// </summary>
public interface IDatabaseInstance : IDisposable
{
    DatabasePath DatabasePath { get; }
}
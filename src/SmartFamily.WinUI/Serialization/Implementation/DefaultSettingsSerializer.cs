namespace SmartFamily.WinUI.Serialization.Implementation;

internal class DefaultSettingsSerializer : ISettingsSerializer
{
    protected string? _filePath;

    public virtual bool CreateFile(string path)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.Open(path, FileMode.OpenOrCreate).Dispose();
            _filePath = path;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public virtual string? ReadFromFile()
    {
        ArgumentNullException.ThrowIfNull(_filePath);

        return File.ReadAllText(_filePath);
    }

    public virtual bool WriteToFile(string? text)
    {
        ArgumentNullException.ThrowIfNull(_filePath);

        try
        {
            File.WriteAllText(_filePath, text);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
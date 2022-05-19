namespace SmartFamily.Backend.Services;

public interface IClipboardService
{
    bool SetData<TData>(TData data);
}
using SmartFamily.Backend.Services;

using Windows.ApplicationModel.DataTransfer;

namespace SmartFamily.WinUI.ServiceImplementation;

internal sealed class ClipboardService : IClipboardService
{
    public bool SetData<TData>(TData data)
    {
        return data switch
        {
            string strData => InitializeAndSetDataToClipboard((dp) => dp.SetText(strData)),
            _ => false,
        };
    }

    private static bool InitializeAndSetDataToClipboard(Action<DataPackage> setDataCallback)
    {
        var dataPackage = new DataPackage();
        setDataCallback(dataPackage);
        Clipboard.SetContent(dataPackage);
        Clipboard.Flush();

        return true;
    }
}
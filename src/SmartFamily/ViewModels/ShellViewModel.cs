using Prism.Mvvm;

using SmartFamily.Core;

namespace SmartFamily.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _title = Platform.AppName;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ShellViewModel()
        {
        }
    }
}
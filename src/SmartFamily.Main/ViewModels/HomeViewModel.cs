using Microsoft.Extensions.Logging;

using Prism.Mvvm;

namespace SmartFamily.Main.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly ILogger<HomeViewModel> _logger;

        public HomeViewModel(ILogger<HomeViewModel> logger)
        {
            _logger = logger;
        }
    }
}
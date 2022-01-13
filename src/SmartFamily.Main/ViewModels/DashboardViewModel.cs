using Microsoft.Extensions.Logging;

using Prism.Mvvm;

namespace SmartFamily.Main.ViewModels
{
    internal class DashboardViewModel : BindableBase
    {
        private readonly ILogger<DashboardViewModel> _logger;

        public DashboardViewModel(ILogger<DashboardViewModel> logger)
        {
            _logger = logger;
        }
    }
}
using Microsoft.Extensions.Logging;

using Prism.Mvvm;

namespace SmartFamily.People.ViewModels
{
    public class PeopleViewModel : BindableBase
    {
        private readonly ILogger<PeopleViewModel> _logger;

        public PeopleViewModel(ILogger<PeopleViewModel> logger)
        {
            _logger = logger;
        }
    }
}
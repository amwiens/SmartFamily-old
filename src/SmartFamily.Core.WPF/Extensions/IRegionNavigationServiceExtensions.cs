namespace Prism.Regions
{
    /// <summary>
    /// Extensions for the Region navigation service.
    /// </summary>
    public static class IRegionNavigationServiceExtensions
    {
        /// <summary>
        /// Checks to see if the navigation service can navigate to the given target.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="target">Target</param>
        /// <returns><c>true</c> if it can be navigated to, otherwise <c>false</c>.</returns>
        public static bool CanNavigate(this IRegionNavigationService navigationService, string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return false;
            }

            if (navigationService.Journal.CurrentEntry == null)
            {
                return true;
            }

            return target != navigationService.Journal.CurrentEntry.Uri.ToString();
        }
    }
}
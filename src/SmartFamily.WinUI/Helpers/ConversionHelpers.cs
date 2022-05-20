using Microsoft.UI.Xaml.Media.Animation;

using SmartFamily.Backend.Models.Transitions;

namespace SmartFamily.WinUI.Helpers;

internal static class ConversionHelpers
{
    public static NavigationTransitionInfo? ToNavigationTransitionInfo(TransitionModel? transitionModel)
    {
        return transitionModel switch
        {
            SlideTransitionModel slideTransitionModel => new SlideNavigationTransitionInfo() { Effect = (SlideNavigationTransitionEffect)slideTransitionModel.SlideTransitionDirection },
            SuppressTransitionModel => new SuppressNavigationTransitionInfo(),
            EntranceTransitionModel => new EntranceNavigationTransitionInfo(),
            DrillInTransitionModel => new DrillInNavigationTransitionInfo(),
            ContinuumTransitionModel => new ContinuumNavigationTransitionInfo(),
            _ => transitionModel?.IsCustom ?? false ? new SuppressNavigationTransitionInfo() : null
        };
    }
}
namespace SmartFamily.Backend.ViewModels.Dashboard.Navigation;

public sealed class NavigationItemViewModel
{
    public bool IsLeading { get; set; }

    public int Index { get; set; }

    public Action<NavigationItemViewModel?>? NavigationAction { get; init; }

    public string? SectionName { get; set; }
}
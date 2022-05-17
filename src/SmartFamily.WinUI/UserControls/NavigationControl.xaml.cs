using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using SmartFamily.Backend.ViewModels.Pages;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI.UserControls;

public sealed partial class NavigationControl : UserControl
{
    public NavigationControl()
    {
        this.InitializeComponent();
    }



    public BasePageViewModel PageViewModel
    {
        get => (BasePageViewModel)GetValue(PageViewModelProperty);
        set => SetValue(PageViewModelProperty, value);
    }
    public static readonly DependencyProperty PageViewModelProperty =
        DependencyProperty.Register("PageViewModel", typeof(BasePageViewModel), typeof(NavigationControl), new PropertyMetadata(null));
}
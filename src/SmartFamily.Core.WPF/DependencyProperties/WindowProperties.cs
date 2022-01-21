using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SmartFamily.Core.WPF.DependencyProperties
{
    public class WindowProperties
    {
        public static readonly DependencyProperty WindowClosingProperty =
           DependencyProperty.RegisterAttached("WindowClosing", typeof(ICommand), typeof(WindowProperties), new UIPropertyMetadata(null, WindowClosing));

        public static object GetWindowClosing(DependencyObject depObj)
        {
            return (ICommand)depObj.GetValue(WindowClosingProperty);
        }

        public static void SetWindowClosing(DependencyObject depObj, ICommand value)
        {
            depObj.SetValue(WindowClosingProperty, value);
        }

        private static void WindowClosing(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var element = (Window)depObj;

            if (element != null)
                element.Closing += OnWindowClosing;
        }

        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {
            ICommand command = (ICommand)GetWindowClosing((DependencyObject)sender);
            command.Execute((Window)sender);
        }
    }
}
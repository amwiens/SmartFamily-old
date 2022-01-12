using MahApps.Metro.Controls;

using System.Windows;
using System.Windows.Controls;

namespace SmartFamily.Core.WPF.TemplateSelectors
{
    /// <summary>
    /// Menu item template selector
    /// </summary>
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template for the Glyph menu item.
        /// </summary>
        public DataTemplate GlyphDataTemplate { get; set; }

        /// <summary>
        /// Template for the Image menu item.
        /// </summary>
        public DataTemplate ImageDataTemplate { get; set; }

        /// <summary>
        /// Selects the template for the menu item.
        /// </summary>
        /// <param name="item">Menu item.</param>
        /// <param name="container">Container.</param>
        /// <returns>Template.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is HamburgerMenuGlyphItem)
            {
                return GlyphDataTemplate;
            }

            if (item is HamburgerMenuImageItem)
            {
                return ImageDataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
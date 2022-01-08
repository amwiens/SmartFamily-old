﻿using MahApps.Metro.Controls;

using System.Windows;
using System.Windows.Controls;

namespace SmartFamily.Core.WPF.TemplateSelectors
{
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GlyphDataTemplate { get; set; }

        public DataTemplate ImageDataTemplate { get; set; }

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
﻿using System.Windows;

namespace SmartFamily.Core.WPF.Controls
{
    /// <summary>
    /// Simple Card with content and <see cref="Footer"/>.
    /// </summary>
    public class Card : System.Windows.Controls.ContentControl
    {
        /// <summary>
        /// Property for <see cref="Footer"/>.
        /// </summary>
        public static readonly DependencyProperty FooterProperty =
            DependencyProperty.Register(nameof(Footer), typeof(object), typeof(Card),
                new PropertyMetadata(null, FooterChangedCallback));

        /// <summary>
        /// Property for <see cref="HasFooter"/>.
        /// </summary>
        public static readonly DependencyProperty HasFooterProperty =
            DependencyProperty.Register(nameof(HasFooter), typeof(bool), typeof(Card),
                new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets additional content displayed at the bottom.
        /// </summary>
        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        /// <summary>
        /// Gets information whether the <see cref="Card"/> has a <see cref="Footer"/>.
        /// </summary>
        public bool HasFooter
        {
            get => (bool)GetValue(HasFooterProperty);
            internal set => SetValue(HasFooterProperty, value);
        }

        private static void FooterChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Card control) return;

            control.SetValue(HasFooterProperty, control.Footer != null);
        }
    }
}
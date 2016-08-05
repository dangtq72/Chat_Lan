using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;

namespace Nvs_Monitor.Themes.Dark
{
    public partial class NoteBox
    {
        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window w = Window.GetWindow((System.Windows.DependencyObject)sender);
            w.DragMove();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow((System.Windows.DependencyObject)sender);
            w.DialogResult = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow((System.Windows.DependencyObject)sender);
            w.DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Window w = Window.GetWindow((System.Windows.DependencyObject)sender);
            w.DialogResult = false;
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            Window w = Window.GetWindow((System.Windows.DependencyObject)sender);
            if (e.Key == Key.Escape)
                w.Close();
        }
    }
}

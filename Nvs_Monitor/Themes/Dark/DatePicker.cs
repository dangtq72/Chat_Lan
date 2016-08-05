using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nvs_Monitor.Themes.Dark
{
    public partial class DatePicker
    {
        public string s = "";
        BrushConverter bc = new BrushConverter();
        System.Windows.Controls.DatePicker dt;

        private void GotFocused(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Control ctrl = (System.Windows.Controls.Control)sender;
            dt = (System.Windows.Controls.DatePicker)sender;
            s = "#008acf";
            Brush brush = (Brush)bc.ConvertFromString(s);
            brush.Freeze();
            ctrl.BorderBrush = brush;
        }

        private void LostFocused(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Control ctrl = (System.Windows.Controls.Control)sender;
            s = "#171e28";
            Brush brush = (Brush)bc.ConvertFromString(s);
            brush.Freeze();
            ctrl.BorderBrush = brush;
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}

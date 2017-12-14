using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WatermarkTextBoxAndPasswordBoxSampleWpf.Helpers
{
    class PasswordBoxHelper : DependencyObject
    {
        private static bool isInitialized = false;

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(" ", OnWatermarkChanged));

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p = d as PasswordBox;
            if (p == null)
                return;

            CheckPasswordLength(p);
            if (!isInitialized)
            {
                p.PasswordChanged += new RoutedEventHandler(passwordBoxChanged);
                p.Unloaded += new RoutedEventHandler(passwordBoxUnloaded);
                isInitialized = true;
            }

        }

        private static void passwordBoxUnloaded(object sender, RoutedEventArgs e)
        {
            var p = sender as PasswordBox;
            if (p == null)
                return;
            p.PasswordChanged -= new RoutedEventHandler(passwordBoxChanged);
        }

        private static void passwordBoxChanged(object sender, RoutedEventArgs e)
        {
            var p = sender as PasswordBox;
            if (p == null)
                return;
            CheckPasswordLength(p);
        }

        private static void CheckPasswordLength(PasswordBox p)
        {
            p.SetValue(PasswordBoxHelper.IsPasswordLengthZeroProperty, String.IsNullOrEmpty(p.Password));
        }

        public static bool GetIsPasswordLengthZero(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordLengthZeroProperty);
        }

        public static void SetIsPasswordLengthZero(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordLengthZeroProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsPasswordLengthZero.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPasswordLengthZeroProperty =
            DependencyProperty.RegisterAttached("IsPasswordLengthZero", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(true));
    }
}

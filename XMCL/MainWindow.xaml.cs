using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using XMCL.Core;

namespace XMCL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Process.GetCurrentProcess().Kill();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Game.downLoadHelper.Owner = this;
            Game.downLoadHelper.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowLogin.WindowLoginOwner = this;
            Background = System.Windows.SystemParameters.WindowGlassBrush;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var windowMode = this.ResizeMode;
                if (this.ResizeMode != ResizeMode.NoResize)
                { this.ResizeMode = ResizeMode.NoResize; }
                this.UpdateLayout();
                DragMove();
                if (this.ResizeMode != windowMode)
                { this.ResizeMode = windowMode; }
                this.UpdateLayout();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }
        private void frame_Navigated(object sender, NavigationEventArgs e)
        {
            text1.Text = frame.Content.ToString();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Game.Stop();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}

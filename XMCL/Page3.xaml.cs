using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace XMCL
{
    /// <summary>
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        static Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Page3()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.ISOnline)
                TL1.Text = "已登录";
            else TL1.Text = "离线";

            L1.Content = "accessToken:" + ConfigurationManager.AppSettings["accessToken"];
            L2.Content = "clientToken:" + ConfigurationManager.AppSettings["clientToken"];
            L3.Content = "uuid:" + ConfigurationManager.AppSettings["uuid"];
            TL.Text = ConfigurationManager.AppSettings["userName"];
            if (App.ISOnline)
            {
                head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + ConfigurationManager.AppSettings["userName"] + "\\head.png"));
                skin.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + ConfigurationManager.AppSettings["userName"] + "\\skin.png"));
            }
            else
            {
                BS.IsEnabled = false;
            }
        }

        private void BS_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BM_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.minecraft.net");
        }

        private void BW_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://minecraft-zh.gamepedia.com/Minecraft_Wiki");
        }

        private void BL_Click(object sender, RoutedEventArgs e)
        {
            App.ISLogin = App.ISOnline = false;
            _config.AppSettings.Settings.Remove("accessToken");
            _config.AppSettings.Settings.Add("accessToken", "");
            _config.AppSettings.Settings.Remove("clientToken");
            _config.AppSettings.Settings.Add("clientToken", "");
            _config.AppSettings.Settings.Remove("userName");
            _config.AppSettings.Settings.Add("userName", "steve");
            _config.AppSettings.Settings.Remove("uuid");
            _config.AppSettings.Settings.Add("uuid", "");
            _config.Save();
            ConfigurationManager.RefreshSection("appSettings");
            this.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void L1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(L1.Content.ToString());
        }
    }
}

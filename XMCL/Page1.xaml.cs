using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XMCL.Core;

namespace XMCL
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationManager.RefreshSection("appSettings");
            button1.IsEnabled = false;
            BL.IsEnabled = false;
            c1.IsEnabled = false;
            bool full = Convert.ToBoolean(ConfigurationManager.AppSettings["FullScreen"]);
            string value = " ";
            bool assetIndex = Convert.ToBoolean(ConfigurationManager.AppSettings["assetIndex"]);
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["MoreValue"])) { }
            else value = ConfigurationManager.AppSettings["JavaValue"];
            Value.Set
                (
                    ConfigurationManager.AppSettings["userName"],
                    ConfigurationManager.AppSettings["Memory"],
                    ConfigurationManager.AppSettings["Game"],
                    ConfigurationManager.AppSettings["JavaPath"],
                    c1.Text,
                    value,
                    ConfigurationManager.AppSettings["uuid"],
                    ConfigurationManager.AppSettings["accessToken"],
                    full,
                    assetIndex
                );
            Game.Run();
            button1.IsEnabled = true;
            BL.IsEnabled = true;
            c1.IsEnabled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BL.IsEnabled = false;
            BL.Content = ConfigurationManager.AppSettings["userName"];
            Task task = new Task(() =>
            {
                if (App.ISLogin)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (App.ISOnline)
                        {
                            head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + ConfigurationManager.AppSettings["userName"] + "\\head.png"));
                            TL.Content = "已登录";
                        }
                        else TL.Content = "离线";
                        BL.IsEnabled = true;
                    }));
                }
                else
                {
                    if (XMCL.Core.Authenticate.Refresh(ConfigurationManager.AppSettings["accessToken"], ConfigurationManager.AppSettings["clientToken"]))
                    {
                        App.ISLogin = true; App.ISOnline = true;
                        XMCL.Core.Tools.GetSkins(ConfigurationManager.AppSettings["uuid"]);
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            BL.IsEnabled = true;
                            TL.Content = "已登录";
                            head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + ConfigurationManager.AppSettings["userName"] + "\\head.png"));
                        }));
                    }
                    else
                    {
                        bool a = false;
                        if (ConfigurationManager.AppSettings["uuid"].Length > 0)
                            if (ConfigurationManager.AppSettings["userName"].Length > 0)
                                a = true;
                        if (a)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                TL.Content = "离线";
                                BL.IsEnabled = true;
                            }));
                        }
                        else
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                WindowLogin.WindowLoginShow(false);
                                ConfigurationManager.RefreshSection("appSettings");
                                BL.Content = ConfigurationManager.AppSettings["userName"];
                                BL.IsEnabled = true;
                            }));
                        }
                        App.ISLogin = true;
                    }
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LL1.Source = null;
                    LL1.Visibility = Visibility.Collapsed;
                    TL.Visibility = Visibility.Visible;
                }));
            });
            task.Start();

        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.ItemsSource = Tools.GetVersions(ConfigurationManager.AppSettings["Game"]);
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string a = comboBox.Text;
            if (a == "")
            { }
            else
            {
                if (a.Length > 25)
                {
                    button1.Content = "开始游戏" + "\r\n" + a.Substring(0, 25) + "...";
                }
                else
                {
                    button1.Content = "开始游戏" + "\r\n" + a;
                }

            }
        }

        private void BL_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page3.xaml", UriKind.Relative));
        }
    }
}

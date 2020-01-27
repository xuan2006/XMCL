using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Form = System.Windows.Forms;

namespace XMCL
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        static Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public Page2()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Background = System.Windows.SystemParameters.WindowGlassBrush;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultDirectory"]))
            { C1.IsChecked = true; T1.IsEnabled = false; BO.IsEnabled = false; }
            else C2.IsChecked = true;
            TW.Text = ConfigurationManager.AppSettings["Width"];
            TH.Text = ConfigurationManager.AppSettings["Height"];
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["FullScreen"]))
                C3.IsChecked = true;
            T1.Text = ConfigurationManager.AppSettings["Game"];
            T2.Text = ConfigurationManager.AppSettings["JavaPath"];
            T3.Text = ConfigurationManager.AppSettings["Memory"];
            T4.Text = ConfigurationManager.AppSettings["JavaValue"];
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["MoreValue"]))
                C6_2.IsChecked = true;
            else C6_1.IsChecked = true;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutoMemory"]))
                C4.IsChecked = true;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["assetIndex"]))
                C5.IsChecked = true;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(100, 64, 64, 64));
            Grid Grid = (Grid)sender;
            Grid.Background = solidColorBrush;
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(100, 30, 30, 30));
            Grid Grid = (Grid)sender;
            Grid.Background = solidColorBrush;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (C1.IsChecked == true)
            {
                _config.AppSettings.Settings.Remove("DefaultDirectory");
                _config.AppSettings.Settings.Add("DefaultDirectory", "true");
            }
            else
            {
                _config.AppSettings.Settings.Remove("Game");
                _config.AppSettings.Settings.Add("Game", T1.Text);
                _config.AppSettings.Settings.Remove("DefaultDirectory");
                _config.AppSettings.Settings.Add("DefaultDirectory", "false");
            }
            _config.AppSettings.Settings.Remove("Width");
            _config.AppSettings.Settings.Add("Width", TW.Text);
            _config.AppSettings.Settings.Remove("Height");
            _config.AppSettings.Settings.Add("Height", TH.Text);
            if (C3.IsChecked == true)
            {
                _config.AppSettings.Settings.Remove("FullScreen");
                _config.AppSettings.Settings.Add("FullScreen", "true");
            }
            else
            {
                _config.AppSettings.Settings.Remove("FullScreen");
                _config.AppSettings.Settings.Add("FullScreen", "false");
            }
            _config.AppSettings.Settings.Remove("JavaPath");
            _config.AppSettings.Settings.Add("JavaPath", T2.Text);
            _config.AppSettings.Settings.Remove("Memory");
            _config.AppSettings.Settings.Add("Memory", T3.Text);
            if (C4.IsChecked == true)
            {
                _config.AppSettings.Settings.Remove("AutoMemory");
                _config.AppSettings.Settings.Add("AutoMemory", "true");
            }
            else
            {
                _config.AppSettings.Settings.Remove("AutoMemory");
                _config.AppSettings.Settings.Add("AutoMemory", "false");
            }
            if (C5.IsChecked == true)
            {
                _config.AppSettings.Settings.Remove("assetIndex");
                _config.AppSettings.Settings.Add("assetIndex", "true");
            }
            else
            {
                _config.AppSettings.Settings.Remove("assetIndex");
                _config.AppSettings.Settings.Add("assetIndex", "false");
            }
            if (C6_1.IsChecked == true)
            {
                _config.AppSettings.Settings.Remove("MoreValue");
                _config.AppSettings.Settings.Add("MoreValue", "false");
                _config.AppSettings.Settings.Remove("JavaValue");
                _config.AppSettings.Settings.Add("JavaValue", " ");
            }
            else
            {
                _config.AppSettings.Settings.Remove("MoreValue");
                _config.AppSettings.Settings.Add("MoreValue", "true");
                _config.AppSettings.Settings.Remove("JavaValue");
                _config.AppSettings.Settings.Add("JavaValue", T4.Text);
            }
            _config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void GameOpen(object sender, RoutedEventArgs e)
        {
            Form.FolderBrowserDialog a = new Form.FolderBrowserDialog();
            a.ShowDialog();
            if (a.SelectedPath.Contains(" "))
                MessageBox.Show("目录中不能包含有空格!");
            else T1.Text = a.SelectedPath;
        }

        private void JavaOpen(object sender, RoutedEventArgs e)
        {
            Form.OpenFileDialog a = new Form.OpenFileDialog();
            a.Filter = "java.exe|java.exe|javaw.exe|javaw.exe";
            a.Title = "打开Java";
            a.ShowDialog();
            T2.Text = a.FileName;
        }

        private void OpenVersion(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private void JavaDownload(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.java.com/zh_CN/");
        }

        private void C1_Checked(object sender, RoutedEventArgs e)
        {
            T1.IsEnabled = false;
            BO.IsEnabled = false;
        }

        private void C2_Checked(object sender, RoutedEventArgs e)
        {
            T1.IsEnabled = true;
            BO.IsEnabled = true;
        }

        private void C6_1_Checked(object sender, RoutedEventArgs e)
        {
            T4.IsEnabled = JVMGuide.IsEnabled = JVMVaule.IsEnabled = false;
        }

        private void C6_2_Checked(object sender, RoutedEventArgs e)
        {
            T4.IsEnabled = JVMGuide.IsEnabled = JVMVaule.IsEnabled = true;
        }

        private void JVMVaule_Click(object sender, RoutedEventArgs e)
        {
            T4.Text = " -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=16M -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow  -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump";
        }

        private void JVMGuide_Click(object sender, RoutedEventArgs e)
        {
            T4.Text = XMCL.Core.GuideJVM.GuideJVMShow();
        }
    }
}

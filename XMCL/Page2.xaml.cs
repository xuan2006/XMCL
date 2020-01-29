using System;
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
        public Page2()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Background = System.Windows.SystemParameters.WindowGlassBrush;
            if (Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
            { C1.IsChecked = true; T1.IsEnabled = false; BO.IsEnabled = false; }
            else C2.IsChecked = true;
            TW.Text = Json.Read("Video", "Width");
            TH.Text = Json.Read("Video", "Height");
            if (Convert.ToBoolean(Json.Read("Video", "IsFullScreen")))
                C3.IsChecked = true;
            T1.Text = Json.Read("Files", "GamePath");
            T2.Text = Json.Read("Files", "JavaPath");
            T3.Text = Json.Read("JVM", "Memory");
            T4.Text = Json.Read("JVM", "Value");
            if (Convert.ToBoolean(Json.Read("JVM", "MoreValueEnabled")))
                C6_2.IsChecked = true;
            else C6_1.IsChecked = true;
            if (Convert.ToBoolean(Json.Read("JVM", "AutoMemory")))
                C4.IsChecked = true;
            if (Convert.ToBoolean(Json.Read("Files", "CompleteResource")))
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
                Json.Write("Files", "UseDefaultDirectory", "true");
            }
            else
            {
                Json.Write("Files", "UseDefaultDirectory", "false");
            }
            Json.Write("Files", "GamePath", T1.Text);
            Json.Write("Video", "Width", TW.Text);
            Json.Write("Video", "Height", TH.Text);
            if (C3.IsChecked == true)
            {
                Json.Write("Video", "IsFullScreen", "true");
            }
            else
            {
                Json.Write("Video", "IsFullScreen", "false");
            }
            Json.Write("Files", "JavaPath", T2.Text);
            Json.Write("JVM", "Memory", T3.Text);
            if (C4.IsChecked == true)
            {
                Json.Write("JVM", "AutoMemory", "true");
            }
            else
            {
                Json.Write("JVM", "AutoMemory", "false");
            }
            if (C5.IsChecked == true)
            {
                Json.Write("Files", "CompleteResource", "true");
            }
            else
            {
                Json.Write("Files", "CompleteResource", "false");
            }
            if (C6_1.IsChecked == true)
            {
                Json.Write("JVM", "MoreValueEnabled", "false");
            }
            else
            {
                Json.Write("JVM", "MoreValueEnabled", "true");
                Json.Write("JVM", "Value", T4.Text);
            }
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

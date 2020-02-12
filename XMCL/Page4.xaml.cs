using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace XMCL
{
    /// <summary>
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        List<string> T = new List<string>();
        public Page4()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Task task = new Task(() =>
            {
                string[] vs = XMCL.Core.Tools.GetVersionsListAll().ToArray();
                for (int i = 0; i < vs.Count(); i++)
                {
                    string[] a1 = vs[i].Split(',');
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lv1.Items.Add(new { T1 = a1[0], T2 = a1[1], T3 = a1[2], T4 = a1[3] });
                    }));
                    T.Add(a1[2]);
                }
            });
            task.Start();
        }

        private void lv1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int a = lv1.SelectedIndex;
            string c = System.IO.Path.GetFileName(T[a]);
            MessageBox.Show(c);
            string b;
            if (Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
                b = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ "\\.minecraft\\versions\\" + c.Substring(0, c.Length - 5) + "\\";
            else b = Json.Read("Files", "GamePath") + "\\versions\\" + c.Substring(0, c.Length - 5) + "\\";
            WebClient client = new WebClient();
            if (Directory.Exists(b))
            { }
            else { Directory.CreateDirectory(b); }
            client.DownloadFile(T[a], b + System.IO.Path.GetFileName(T[a]));
            MessageBox.Show("下载完成");
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }
    }
}

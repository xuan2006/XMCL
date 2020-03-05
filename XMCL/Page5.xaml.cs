using System;
using System.Windows;
using System.Windows.Controls;

namespace XMCL
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            F5();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void ToPage4(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Page4.xaml", UriKind.Relative));
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
            {
                XMCL.Core.Game.DelectDir(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\versions\\" + ListBox1.SelectedItem);
            }
            else XMCL.Core.Game.DelectDir(Json.Read("Files", "GamePath") + "\\versions\\" + ListBox1.SelectedItem);
            F5();
        }

        private void F5_Click(object sender, RoutedEventArgs e)
        {
            F5();
        }
        void F5 ()
        {
            ListBox1.ItemsSource = new string[] { };
            if (Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
            {
                string[] a = XMCL.Core.Tools.GetVersions(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"\\.minecraft");
                ListBox1.ItemsSource = a;
            }
            else
            {
                string[] a = XMCL.Core.Tools.GetVersions(Json.Read("Files", "GamePath"));
                ListBox1.ItemsSource = a;
            }
        }
    }
}

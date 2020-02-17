using System.Windows;
namespace XMCL.Core
{
    /// <summary>
    /// Java.xaml 的交互逻辑
    /// </summary>
    public partial class Java : Window
    {
        public static Window OwnerWindows { get; set; }
        public static string a { get; set; }
        public Java()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            a = ListBox1.SelectedItem.ToString();
            this.Close();
        }

        public static string ChooseJava()
        {
            Java java = new Java();
            try { java.Owner = OwnerWindows; } catch { }
            java.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            java.ShowDialog();
            string b;
            if (a == null)
                b = "";
            else b = a;
            return b;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox1.ItemsSource = Tools.GetJavaList();
        }

        private void JavaOpen(object sender, RoutedEventArgs e)
        {
            a = ListBox1.SelectedItem.ToString();
            this.Close();
        }
    }
}

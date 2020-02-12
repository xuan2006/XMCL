using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XMCL.Core
{
    /// <summary>
    /// Java.xaml 的交互逻辑
    /// </summary>
    public partial class Java : Window
    {
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
    }
}

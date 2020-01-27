using System.Text;
using System.Windows;

namespace XMCL.Core
{
    /// <summary>
    /// GuideJVM.xaml 的交互逻辑
    /// </summary>
    public partial class GuideJVM : Window
    {
        static StringBuilder A = new StringBuilder();
        public GuideJVM()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (T1.IsChecked == true)
                A.Append(" " + T1.Content + " ");
            if (T2.IsChecked == true)
                A.Append(" " + T2.Content + " ");
            if (T3.IsChecked == true)
                A.Append(" " + T3.Content + " ");
            if (T4.IsChecked == true)
                A.Append(" " + T4.Content + " ");
            if (T5.IsChecked == true)
                A.Append(" " + T5.Content + " ");
            if (T6.IsChecked == true)
                A.Append(" " + T6.Content + " ");
            if (T7.IsChecked == true)
                A.Append(" " + T7.Content + " ");
            if (T8.IsChecked == true)
                A.Append(" " + T8.Content + " ");
            if (T9.IsChecked == true)
                A.Append(" " + T9.Content + " ");
            if (T10.IsChecked == true)
                A.Append(" " + T10.Content + " ");
            if (T11.IsChecked == true)
                A.Append(" " + T11.Content + " ");
            if (T12.IsChecked == true)
                A.Append(" " + T12.Content + " ");
            this.Close();
        }
        public static string GuideJVMShow()
        {
            GuideJVM guideJVM = new GuideJVM();
            guideJVM.ShowDialog();
            return A.ToString();
        }
    }
}

using System;
using System.Windows;

namespace XMCL.Core
{
    /// <summary>
    /// WindowLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WindowLogin : Window
    {
        static WindowLogin windowLogin = new WindowLogin();

        static bool a = true;
        public WindowLogin()
        {
            InitializeComponent();
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (C1.Text == "正版登录")
            {
                L2.Visibility = T2.Visibility = Visibility.Visible;
            }
            else
            {
                L2.Visibility = T2.Visibility = Visibility.Hidden;
            }
        }
        private void B1_Click(object sender, RoutedEventArgs e)
        {
            if (C1.Text == "正版登录")
            {
                if (Authenticate.Login(T1.Text, T2.Password))
                {
                    a = true;
                    Tools.GetSkins(Json.Read("Login", "uuid"));
                    this.Owner.Activate();
                    this.Close();
                }
            }
            else
            {
                if (T1.Text == "")
                {
                    MessageBox.Show("必须键入用户名");
                    this.Owner.Activate();
                }
                else
                {
                    Authenticate.Offline(T1.Text);
                    a = false;
                    this.Owner.Activate();
                    this.Close();
                }
            }

        }
        private void B2_Click(object sender, RoutedEventArgs e)
        {
            a = false;
            this.Close();
        }
        public static Window WindowLoginOwner { get; set; }
        public static bool WindowLoginShow()
        {
            windowLogin = new WindowLogin();
            windowLogin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            windowLogin.Owner = WindowLoginOwner;
            windowLogin.ShowDialog();
            return a;
        }
        public static bool WindowLoginShow(bool CanClose)
        {
            windowLogin = new WindowLogin();
            windowLogin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            windowLogin.Owner = WindowLoginOwner;
            windowLogin.B2.IsEnabled = CanClose;
            windowLogin.ShowDialog();
            return a;
        }

    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XMCL.Core;

namespace XMCL
{
    public class Json
    {
        static string a = System.IO.Directory.GetCurrentDirectory() + "\\XMCL.json";
        public static string Read(string Section, string Name)
        {
            if (System.IO.File.Exists(a))
            {
                string b = System.IO.File.ReadAllText(a);
                try
                {
                    return JObject.Parse(b)[Section][Name].ToString();
                }
                catch { return null; }
            }
            else
            {
                return null;
            }
        }
        public static void Write(string Section, string Name, string Text)
        {
            if (System.IO.File.Exists(a))
            {
                string b = System.IO.File.ReadAllText(a);
                try
                {
                    JObject jObject = JObject.Parse(b);
                    jObject[Section][Name] = Text;
                    string convertString = Convert.ToString(jObject);
                    System.IO.File.WriteAllText(a, convertString);
                }
                catch { }
            }
            else
            {

            }
        }

    }
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
            button1.IsEnabled = false;
            BL.IsEnabled = false;
            c1.IsEnabled = false;
            string value = " ";
            string GamePath;
            bool assetIndex = Convert.ToBoolean(Json.Read("Files", "CompleteResource"));
            if (Convert.ToBoolean(Json.Read("JVM", "MoreValueEnabled")))
            { }
            else value = Json.Read("JVM", "Value");
            if (Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
            {
                string c = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
                if (System.IO.Directory.Exists(c))
                { }
                else
                {
                    System.IO.Directory.CreateDirectory(c);
                }
                GamePath = c;
            }
            else
            {
                GamePath = Json.Read("Files", "GamePath");
            }
            Value.Set
                (
                    Json.Read("Login", "userName"),
                    Json.Read("JVM", "Memory"),
                    GamePath,
                    Json.Read("Files", "JavaPath"),
                    c1.Text,
                    value,
                    Json.Read("Login", "uuid"),
                    Json.Read("Login", "accessToken"),
                    Convert.ToBoolean(Json.Read("Video", "IsFullScreen")),
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
            BL.Content = Json.Read("Login", "userName");
            Task task = new Task(() =>
            {
                if (App.ISLogin)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (App.ISOnline)
                        {
                            head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + Json.Read("Login", "userName") + "\\head.png"));
                            TL.Content = "已登录";
                        }
                        else TL.Content = "离线";
                        BL.IsEnabled = true;
                    }));
                }
                else
                {
                    if (XMCL.Core.Authenticate.Refresh(Json.Read("Login", "accessToken"), Json.Read("Login", "clientToken")))
                    {
                        App.ISLogin = true; App.ISOnline = true;
                        XMCL.Core.Tools.GetSkins(Json.Read("Login", "uuid"));
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            BL.IsEnabled = true;
                            TL.Content = "已登录";
                            head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + Json.Read("Login", "userName") + "\\head.png"));
                        }));
                    }
                    else
                    {
                        bool a = false;
                        if (Json.Read("Login", "uuid").Length > 0)
                            if (Json.Read("Login", "userName").Length > 0)
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
                                if (WindowLogin.WindowLoginShow(false))
                                {
                                    App.ISOnline = true;
                                    TL.Content = "已登录";
                                    head.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\user\\" + Json.Read("Login", "userName") + "\\head.png"));
                                }
                                BL.Content = Json.Read("Login", "userName");
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
            string GamePath;
            if (Convert.ToBoolean(Json.Read("Files", "UseDefaultDirectory")))
            {
                string c = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
                if (System.IO.Directory.Exists(c))
                { }
                else
                {
                    System.IO.Directory.CreateDirectory(c);
                }
                GamePath = c;
            }
            else
            {
                GamePath = Json.Read("Files", "GamePath");
            }
            comboBox.ItemsSource = Tools.GetVersions(GamePath);
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

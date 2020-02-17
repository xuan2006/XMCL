using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using XMCL.Core;

namespace XMCL
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    public partial class MainWindow : Window
    {
        #region 透明
        ///透明[
        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        internal enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
        /// ]
        #endregion
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }
        public MainWindow()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(Json.Read("Individualization", "EnableImage")))
            {
                try
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri(Json.Read("Individualization", "ImageFile")));
                    Background = imageBrush;
                }
                catch { }
            }
            else
            {
                try
                {
                    string[] a = Json.Read("Individualization", "BackgroundColor").Split(',');
                    grid.Background = new SolidColorBrush(Color.FromArgb(Convert.ToByte(a[0]), Convert.ToByte(a[1]), Convert.ToByte(a[2]), Convert.ToByte(a[3])));
                }
                catch { }
            }
            EnableBlur();
            System.GC.Collect();
            Game.downLoadHelper.Owner = this;
            Game.downLoadHelper.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowLogin.WindowLoginOwner = this;
            Java.OwnerWindows = this;
            if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + "\\XMCL.json"))
            { }
            else
            {
                FileStream fs1 = new FileStream(System.Environment.CurrentDirectory + "\\XMCL.json", FileMode.Create, FileAccess.ReadWrite);
                try
                {
                    fs1.Write(Resource1.XMCL, 0, Resource1.XMCL.Length);
                    fs1.Flush();
                    fs1.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            System.GC.Collect();
            Menu.Width = 50;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                var windowMode = this.ResizeMode;
                if (this.ResizeMode != ResizeMode.NoResize)
                { this.ResizeMode = ResizeMode.NoResize; }
                this.UpdateLayout();
                DragMove();
                if (this.ResizeMode != windowMode)
                { this.ResizeMode = windowMode; }
                this.UpdateLayout();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Menu.Width == 160)
                Menu.Width = 50;
            else Menu.Width = 160;
        }

        private void ToPage1(object sender, RoutedEventArgs e)
        {
            if (frame.Content.ToString().Contains("Page1.xaml")) 
            { }
            else frame.Navigate(new Page1());
            if (Menu.Width == 160)
                Menu.Width = 50;
            else Menu.Width = 160;
        }

        private void ToPage2(object sender, RoutedEventArgs e)
        {
            if (frame.Content.ToString().Contains("Page2.xaml"))
            { }
            else frame.Navigate(new Page2());
            if (Menu.Width == 160)
                Menu.Width = 50;
        }

        private void ToPage5_Click(object sender, RoutedEventArgs e)
        {
            if (frame.Content.ToString().Contains("Page5.xaml"))
            { }
            else frame.Navigate(new Page5());
            if (Menu.Width == 160)
                Menu.Width = 50;
        }
    }
}

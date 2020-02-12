using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
namespace XMCL.Core
{
    public class Check
    {
        public static bool CanLauch = true;
    }
    public class Game
    {
        public static System.Collections.Generic.List<string> ZipList = new System.Collections.Generic.List<string>();
        public static DownLoadHelper downLoadHelper = new DownLoadHelper();
        static Process process = new Process();
        public static void Run()
        {
            downLoadHelper.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            Value.Resource();
            string A = Value.Arguments();
            downLoadHelper.start();
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\start.log", "java " + A);
            DelectDir(Value.GamePath + "\\bin");
            for (int i = 0; i < ZipList.Count; i++)
            {
                if (Directory.Exists(Value.GamePath + "\\bin\\META-INF"))
                    DelectDir(Value.GamePath + "\\bin\\META-INF");
                try { ZipFile.ExtractToDirectory(ZipList[i], Value.GamePath + "\\bin"); } catch { }
            }
            if (Check.CanLauch)
            {
                process.StartInfo.FileName = Value.JavaPath;
                process.StartInfo.Arguments = A;
                process.Start();
            }
            else
            {
                downLoadHelper.Owner.Activate();
                System.Windows.MessageBox.Show(Error.ToString());
                Error = new StringBuilder();
                Check.CanLauch = true;
            }
            Window window = downLoadHelper.Owner;
            downLoadHelper = new DownLoadHelper();
            downLoadHelper.Owner = window;
        }
        public static void Stop()
        {
            try
            {
                if (process.HasExited)
                { }
                else { process.Kill(); }
            }
            catch { }
        }
        public static StringBuilder Error = new StringBuilder();
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch
            {

            }
        }
    }
}

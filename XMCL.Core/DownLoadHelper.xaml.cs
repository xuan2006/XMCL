﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace XMCL.Core
{
    /// <summary>
    /// DownLoadHelper.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoadHelper : Window
    {
        public List<string> JarsList = new List<string>();
        public List<string> AssetsList = new List<string>();
        public List<string> JarURLsList = new List<string>();
        public List<string> AssetURLsList = new List<string>();
        public DownLoadHelper()
        {
            InitializeComponent();
        }
        public void DownloadFile(string URL, string filename, ProgressBar prog, Label label)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (prog != null)
                    {
                        prog.Maximum = (int)totalBytes;
                    }
                }));

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (prog != null)
                        {
                            prog.Value = (int)totalDownloadedByte;
                        }
                    }));
                    osize = st.Read(by, 0, (int)by.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        label.Content = "正在下载..." + percent.ToString() + "%    " + System.IO.Path.GetFileName(URL);
                    }));
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public void start()
        {
            int a = 0;
            Task task = new Task(() =>
            {
                for (int i = 0; i < JarsList.Count; i++)
                {
                    DownloadFile(JarURLsList[i], JarsList[i], PB1, Label1);
                }
                a += 1;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Label1.Content = "下载完成";
                    if (a == 2)
                    {
                        this.Hide();
                        JarsList.Clear();
                        JarURLsList.Clear();
                        AssetsList.Clear();
                        AssetURLsList.Clear();
                    }
                }));
            });
            task.Start();
            Task task1 = new Task(() =>
            {
                for (int i = 0; i < AssetsList.Count; i++)
                {
                    DownloadFile(AssetURLsList[i], AssetsList[i], PB2, Label2);
                }
                a += 1;
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Label2.Content = "下载完成";
                    if (a == 2)
                    {
                        this.Hide();
                        JarsList.Clear();
                        JarURLsList.Clear();
                        AssetsList.Clear();
                        AssetURLsList.Clear();
                    }
                }));
            });
            task1.Start();
            this.ShowDialog();

        }
    }
}

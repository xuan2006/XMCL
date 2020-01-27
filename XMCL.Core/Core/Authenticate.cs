using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace XMCL.Core
{
    public class Authenticate
    {
        /// <summary>
        /// Minecraft Authenticate 登录方法
        /// </summary>
        static Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public static bool Refresh(string accessToken, string clientToken)
        {
            try
            {
                WebRequest wr = WebRequest.Create("https://authserver.mojang.com/refresh");//创建请求对象
                wr.ContentType = "application/json";//定义Content-Type
                wr.Method = "post";//定义请求类型
                string uls = "{\"accessToken\": \"" + accessToken + "\",\"clientToken\": \"" + clientToken + "\"}";
                byte[] bs = Encoding.UTF8.GetBytes(uls);//上传数据部分
                wr.ContentLength = bs.Length;//上传数据部分
                Stream sw = wr.GetRequestStream();//上传数据部分
                sw.Write(bs, 0, bs.Length);//上传数据部分
                sw.Flush();//上传数据部分
                sw.Close();//上传数据部分
                StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream());//读取返回数据部分
                string rtxt = sr.ReadToEnd();//读取返回数据部分
                JObject jObject = JObject.Parse(rtxt);
                JObject jObject1 = JObject.Parse(jObject["selectedProfile"].ToString());
                _config.AppSettings.Settings.Remove("accessToken");
                _config.AppSettings.Settings.Add("accessToken", jObject["accessToken"].ToString());
                _config.AppSettings.Settings.Remove("clientToken");
                _config.AppSettings.Settings.Add("clientToken", jObject["clientToken"].ToString());
                _config.AppSettings.Settings.Remove("userName");
                _config.AppSettings.Settings.Add("userName", jObject1["name"].ToString());
                _config.AppSettings.Settings.Remove("uuid");
                _config.AppSettings.Settings.Add("uuid", jObject1["id"].ToString());
                _config.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Login(string email, string password)
        {
            try
            {
                WebRequest wr = WebRequest.Create("https://authserver.mojang.com/authenticate");//创建请求对象
                wr.ContentType = "application/json";//定义Content-Type
                wr.Method = "post";//定义请求类型
                string uls = "{\"agent\": {\"name\": \"Minecraft\", \"version\": 1 },\"username\": \"" + email + "\", \"password\": \"" + password + "\"}";
                byte[] bs = Encoding.UTF8.GetBytes(uls);//上传数据部分
                wr.ContentLength = bs.Length;//上传数据部分
                Stream sw = wr.GetRequestStream();//上传数据部分
                sw.Write(bs, 0, bs.Length);//上传数据部分
                sw.Flush();//上传数据部分
                sw.Close();//上传数据部分
                StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream());//读取返回数据部分
                string rtxt = sr.ReadToEnd();//读取返回数据部分
                JObject jObject = JObject.Parse(rtxt);
                JObject jObject1 = JObject.Parse(jObject["selectedProfile"].ToString());
                _config.AppSettings.Settings.Remove("accessToken");
                _config.AppSettings.Settings.Add("accessToken", jObject["accessToken"].ToString());
                _config.AppSettings.Settings.Remove("clientToken");
                _config.AppSettings.Settings.Add("clientToken", jObject["clientToken"].ToString());
                _config.AppSettings.Settings.Remove("userName");
                _config.AppSettings.Settings.Add("userName", jObject1["name"].ToString());
                _config.AppSettings.Settings.Remove("uuid");
                _config.AppSettings.Settings.Add("uuid", jObject1["id"].ToString());
                _config.Save();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("登陆失败," + ex.Message);
                return false;
            }
        }
        public static void Offline(string name)
        {
            _config.AppSettings.Settings.Remove("accessToken");
            _config.AppSettings.Settings.Add("accessToken", Guid.NewGuid().ToString("N"));
            _config.AppSettings.Settings.Remove("clientToken");
            _config.AppSettings.Settings.Add("clientToken", "");
            _config.AppSettings.Settings.Remove("userName");
            _config.AppSettings.Settings.Add("userName", name);
            _config.AppSettings.Settings.Remove("uuid");
            _config.AppSettings.Settings.Add("uuid", Guid.NewGuid().ToString("N"));
            _config.Save();
        }
    }
}

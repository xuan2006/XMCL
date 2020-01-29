using Newtonsoft.Json.Linq;
using System;
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
        public static string[] GetLogin { get; set; }
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
                Json.Write("Login", "accessToken", jObject["accessToken"].ToString());
                Json.Write("Login", "clientToken", jObject["clientToken"].ToString());
                Json.Write("Login", "userName", jObject1["name"].ToString());
                Json.Write("Login", "uuid", jObject1["id"].ToString());
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
                Json.Write("Login", "accessToken", jObject["accessToken"].ToString());
                Json.Write("Login", "clientToken", jObject["clientToken"].ToString());
                Json.Write("Login", "userName", jObject1["name"].ToString());
                Json.Write("Login", "uuid", jObject1["id"].ToString());
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
            Json.Write("Login", "accessToken", Guid.NewGuid().ToString("N"));
            Json.Write("Login", "clientToken", "");
            Json.Write("Login", "userName", name);
            Json.Write("Login", "uuid", Guid.NewGuid().ToString("N"));
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace XMCL.Core
{
    public partial class Tools
    {
        public static List<string> GetVersionsListAll()
        {
            string pageHtml = "";
            WebClient MyWebClient = new WebClient();
            MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
            Byte[] pageData = MyWebClient.DownloadData("https://launchermeta.mojang.com/mc/game/version_manifest.json"); //从指定网站下载数据
            MemoryStream ms = new MemoryStream(pageData);
            using (StreamReader sr = new StreamReader(ms, Encoding.GetEncoding("GB2312")))
            {
                pageHtml = sr.ReadLine();
            }
            List<string> vs = new List<string>();
            JObject jObject = JObject.Parse(pageHtml);
            JArray jArray = JArray.Parse(jObject["versions"].ToString());
            for (int i = 0; i < jArray.Count(); i++)
            {
                JObject jObject1 = JObject.Parse(jArray[i].ToString());
                vs.Add(jObject1["id"].ToString() + "," + jObject1["type"].ToString() + "," + jObject1["url"].ToString() + "," + jObject1["releaseTime"].ToString());
            }
            return vs;
        }
        public static string[] GetVersions(string MinecraftFile)
        {
            string a = MinecraftFile;
            List<string> vs = new List<string>();
            if (Directory.Exists(a + "\\versions"))
            { }
            else { Directory.CreateDirectory(a + "\\versions"); }
            string[] b = Directory.GetDirectories(a + "\\versions");
            for (int i = 0; i < b.Length; i++)
            {
                int c = (a + "\\versions\\").Length;
                string d = b[i].Substring(c, b[i].Length - c);
                if (File.Exists(b[i] + "\\" + d + ".json"))
                    vs.Add(d);
            }
            string[] vs1 = vs.ToArray();
            return vs1;
        }
    }
}

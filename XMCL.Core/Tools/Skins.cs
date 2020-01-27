using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;

namespace XMCL.Core
{
    public partial class Tools
    {
        public static void GetSkins(string uuid)
        {
            try
            {
                WebRequest wr = WebRequest.Create("https://sessionserver.mojang.com/session/minecraft/profile/" + uuid);//创建请求对象
                wr.ContentType = "application/json";//定义Content-Type
                wr.Method = "GET";//定义请求类型
                StreamReader sr = new StreamReader(wr.GetResponse().GetResponseStream());//读取返回数据部分
                string rtxt = sr.ReadToEnd();//读取返回数据部分
                JObject jObject = JObject.Parse(rtxt);
                string a = jObject["properties"].ToString();
                string a1 = a.Substring(5, a.Length - 7);
                JObject jObject3 = JObject.Parse(a1);
                string a2 = DecodeBase64("utf-8", jObject3["value"].ToString());
                JObject jObject1 = JObject.Parse(a2);
                JObject jObject2 = JObject.Parse(jObject1["textures"].ToString());
                JObject jObject4 = JObject.Parse(jObject2["SKIN"].ToString());
                string a3 = Directory.GetCurrentDirectory() + "\\user\\" + jObject1["profileName"].ToString();
                if (Directory.Exists(a3))
                { }
                else Directory.CreateDirectory(a3);
                WebClient client = new WebClient();
                client.DownloadFile(jObject4["url"].ToString(), a3 + "\\" + "skin.png");
                cutImage(a3 + "\\" + "skin.png", a3 + "\\" + "head.png");
            }
            catch
            {

            }
        }
        static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        static void cutImage(string openfile, string savefile)
        {

            byte[] bytes = File.ReadAllBytes(openfile);

            MemoryStream memoryStream = new MemoryStream(bytes);
            Image image = Image.FromStream(memoryStream);
            Image cutedImage = null;

            //先初始化一个位图对象，来存储截取后的图像
            Bitmap bmpDest = new Bitmap(8, 8, PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(bmpDest);

            //矩形定义,将要在被截取的图像上要截取的图像区域的左顶点位置和截取的大小
            Rectangle rectSource = new Rectangle(8, 8, 8, 8);


            //矩形定义,将要把 截取的图像区域 绘制到初始化的位图的位置和大小
            //rectDest说明，将把截取的区域，从位图左顶点开始绘制，绘制截取的区域原来大小
            Rectangle rectDest = new Rectangle(0, 0, 8, 8);

            //第一个参数就是加载你要截取的图像对象，第二个和第三个参数及如上所说定义截取和绘制图像过程中的相关属性，第四个属性定义了属性值所使用的度量单位
            g.DrawImage(image, rectDest, rectSource, GraphicsUnit.Pixel);

            //在GUI上显示被截取的图像
            cutedImage = (Image)bmpDest;
            try
            {
                cutedImage.Save(savefile);
            }
            catch
            {
                try
                {
                    cutedImage.Save(savefile);
                }
                catch
                {

                }
            }
            g.Dispose();
        }
    }
}

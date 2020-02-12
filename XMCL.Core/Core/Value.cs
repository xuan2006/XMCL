using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace XMCL.Core
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
                catch
                {
                }
            }
            else
            {
                System.Windows.MessageBox.Show("");
            }
        }

    }
    public class Value
    {
        public static void Set(string name, string memory, string gamepath, string javapath, string version, string morevalue, string uuid, string accesstoken, bool fullscreen, bool complementaryResources)
        {
            UserName = name; Memory = memory; GamePath = gamepath; JavaPath = javapath; Selected_Version = version; MoreValue = morevalue; UUID = uuid; AccessToken = accesstoken; IsFullScreen = fullscreen; ComplementaryResources = complementaryResources;
        }
        public static string UserName { get; set; }
        public static string Memory { set; get; }
        public static string GamePath { set; get; }
        public static string JavaPath { set; get; }
        public static string Selected_Version { set; get; }
        public static string MoreValue { get; set; }
        public static string UUID { get; set; }
        public static string AccessToken { get; set; }
        public static bool IsFullScreen { get; set; }
        public static bool ComplementaryResources { get; set; }
        public static string Arguments()
        {
            JObject jObject = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".json"));
            StringBuilder Main = new StringBuilder();
            StringBuilder CP = new StringBuilder();
            Main.Append(" -Xmx" + Memory + "M");
            Main.Append(MoreValue);
            Main.Append(" -Djava.library.path=" + GamePath + "\\bin");
            Main.Append(" -Dminecraft.launcher.brand=XMCL  -Dminecraft.launcher.version=2");
            if (jObject["id"].ToString().ToLower().Contains("forge"))
            {
                string form = jObject["inheritsFrom"].ToString();
                JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                JArray jArray = JArray.Parse(jO["libraries"].ToString());
                JArray jA = JArray.Parse(jObject["libraries"].ToString());
                if (File.Exists(GamePath + "\\versions\\" + form + "\\" + form + ".jar"))
                {
                    Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + form + "\\" + form + ".jar");
                }
                else
                {
                    try
                    {
                        JObject jObject1 = JObject.Parse(jO["downloads"].ToString());
                        JObject jObject2 = JObject.Parse(jObject["client"].ToString());
                        Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + form + "\\" + form + ".jar");
                        Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                    }
                    catch (Exception ex)
                    { Check.CanLauch = false; System.Windows.Forms.MessageBox.Show(ex.Message); }
                }
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject1 = JObject.Parse(jArray[i].ToString());
                    string a1 = jObject1["name"].ToString();
                    string[] vs = a1.Split(':');

                    bool IsNatives = false;
                    try
                    {
                        jObject1["natives"].ToString();
                        IsNatives = true;
                    }
                    catch { }
                    if (IsNatives == false)
                    {
                        if (vs[2] == "3.2.1")
                        { }
                        else
                        {
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                            if (File.Exists(b1))
                            {

                                if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                { CP.Append(b1 + ";"); }
                                else
                                {
                                    File.Delete(b1);
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                            else
                            {
                                Game.downLoadHelper.JarsList.Add(b1);
                                Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                CP.Append(b1 + ";");
                            }
                        }
                    }
                    else
                    {
                        if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                        { }
                        else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                        bool X;
                        try
                        {
                            JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                            jObject2["windows"].ToString();
                            X = true;
                        }
                        catch { X = false; }
                        if (X == true)
                        {
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                            JObject jObject4; try { jObject4 = JObject.Parse(jObject3["natives-windows"].ToString()); } catch { jObject4 = JObject.Parse(jObject3["natives-windows-32"].ToString()); }
                            
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                            if (File.Exists(b1))
                            {
                                if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                {
                                    Game.ZipList.Add(b1);
                                }
                                else
                                {
                                    File.Delete(b1);
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                    Game.ZipList.Add(b1);
                                }
                            }
                            else
                            {
                                try
                                {
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                    Game.ZipList.Add(b1);
                                }
                                catch
                                {
                                    try
                                    {
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < jA.Count; i++)
                {
                    JObject jObject1 = JObject.Parse(jA[i].ToString());
                    string a1 = jObject1["name"].ToString();
                    string[] vs = a1.Split(':');
                    bool @new = true;
                    try
                    {
                        JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                        JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                        @new = true;
                    }
                    catch { @new = false; }
                    if (@new == true)
                    {
                        string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                        if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                        { }
                        else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                        if (File.Exists(b1.Substring(0, b1.Length - 1)))
                        {
                            CP.Append(b1);
                        }
                        else
                        {
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                            if (jObject3["url"].ToString() == "")
                            { }
                            else
                            {
                                Game.downLoadHelper.JarsList.Add(b1.Substring(0, b1.Length - 1));
                                Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                CP.Append(b1);
                            }
                        }
                    }
                    else
                    {
                        string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                        if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                        { }
                        else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                        if (File.Exists(b1.Substring(0, b1.Length - 1)))
                        {
                            CP.Append(b1);
                        }
                        else
                        {
                            try
                            {
                                Game.downLoadHelper.JarsList.Add(b1.Substring(0, b1.Length - 1));
                                Game.downLoadHelper.JarURLsList.Add(jObject1["url"].ToString());
                                CP.Append(b1);
                            }
                            catch { }
                        }
                    }
                }
                Main.Append(" -cp " + CP.ToString());
                Main.Append(GamePath + "\\versions\\" + form + "\\" + form + ".jar ");
            }
            else if (jObject["id"].ToString().ToLower().Contains("optifine"))
            {
                string form = jObject["inheritsFrom"].ToString();
                JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                JArray jArray = JArray.Parse(jO["libraries"].ToString());
                JArray jA = JArray.Parse(jObject["libraries"].ToString());
                if (File.Exists(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar"))
                {
                    Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                }
                else
                {
                    Check.CanLauch = false;
                }
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject1 = JObject.Parse(jArray[i].ToString());
                    string a1 = jObject1["name"].ToString();
                    string[] vs = a1.Split(':');

                    bool IsNatives = false;
                    try
                    {
                        jObject1["natives"].ToString();
                        IsNatives = true;
                    }
                    catch { }
                    if (IsNatives == false)
                    {
                        if (vs[2] == "3.2.1")
                        { }
                        else
                        {
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                            if (File.Exists(b1))
                            {
                                if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                { CP.Append(b1 + ";"); }
                                else
                                {
                                    File.Delete(b1);
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                            else
                            {
                                Game.downLoadHelper.JarsList.Add(b1);
                                Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                CP.Append(b1 + ";");
                            }
                        }
                    }
                    else
                    {
                        if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                        { }
                        else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                        bool X;
                        try
                        {
                            JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                            jObject2["windows"].ToString();
                            X = true;
                        }
                        catch { X = false; }
                        if (X == true)
                        {
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                            JObject jObject4 = JObject.Parse(jObject3["natives-windows"].ToString());
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                            if (File.Exists(b1))
                            {
                                if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                {
                                    Game.ZipList.Add(b1);
                                }
                                else
                                {
                                    File.Delete(b1);
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                    Game.ZipList.Add(b1);
                                }
                            }
                            else
                            {
                                try
                                {
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                    Game.ZipList.Add(b1);
                                }
                                catch
                                {
                                    try
                                    {
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < jA.Count; i++)
                {
                    JObject jObject1 = JObject.Parse(jA[i].ToString());
                    string a1 = jObject1["name"].ToString();
                    string[] vs = a1.Split(':');
                    string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                    if (File.Exists(b1.Substring(0, b1.Length - 1)))
                    {
                        CP.Append(b1);
                    }
                    else
                    {
                        Check.CanLauch = false;
                    }
                }
                Main.Append(" -cp " + CP.ToString());
                Main.Append(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar ");
            }
            else
            {
                JArray jArray = JArray.Parse(jObject["libraries"].ToString());
                if (File.Exists(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar"))
                {
                    JObject jObject1 = JObject.Parse(jObject["downloads"].ToString());
                    JObject jObject2 = JObject.Parse(jObject1["client"].ToString());
                    Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                    if (new FileInfo(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar").Length == Convert.ToInt64(jObject2["size"].ToString()))
                    { }
                    else
                    {
                        Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                        Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                    }
                }
                else
                {
                    JObject jObject1 = JObject.Parse(jObject["downloads"].ToString());
                    JObject jObject2 = JObject.Parse(jObject1["client"].ToString());
                    Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                    Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                }
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject1 = JObject.Parse(jArray[i].ToString());
                    string a1 = jObject1["name"].ToString();
                    string[] vs = a1.Split(':');

                    bool IsNatives = false;
                    try
                    {
                        jObject1["natives"].ToString();
                        IsNatives = true;
                    }
                    catch { }
                    if (IsNatives == false)
                    {
                        if (vs[2] == "3.2.1")
                        { }
                        else
                        {
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                            if (File.Exists(b1))
                            {
                                if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                { CP.Append(b1 + ";"); }
                                else
                                {
                                    File.Delete(b1);
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                            else
                            {
                                Game.downLoadHelper.JarsList.Add(b1);
                                Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                CP.Append(b1 + ";");
                            }
                        }
                    }
                    else
                    {
                        if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                        { }
                        else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                        bool X;
                        try
                        {
                            JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                            jObject2["windows"].ToString();
                            X = true;
                        }
                        catch { X = false; }
                        if (X == true)
                        {
                            try
                            {
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                                JObject jObject4 = JObject.Parse(jObject3["natives-windows"].ToString());
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                                if (File.Exists(b1))
                                {
                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                    {
                                        Game.ZipList.Add(b1);
                                    }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
                Main.Append(" -cp " + CP.ToString());
                Main.Append(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar ");
            }
            Main.Append(jObject["mainClass"].ToString());
            Main.Append(" --username " + UserName);
            if (jObject["id"].ToString().ToLower().Contains("forge"))
            {
                string[] a = jObject["id"].ToString().Split('-');
                int b = Convert.ToInt32(a[0].Split('.')[1]);
                if (b < 13)
                {
                    string form = jObject["inheritsFrom"].ToString();
                    JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                    try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                    try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                    Main.Append(" --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker");
                }
                else
                {
                    string form = jObject["inheritsFrom"].ToString();
                    JObject jObject5 = JObject.Parse(jObject["arguments"].ToString());
                    JArray jArray1 = JArray.Parse(jObject5["game"].ToString());
                    JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                    try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                    try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                    Main.Append(" --launchTarget " + jArray1[1]);
                    Main.Append(" --fml.forgeVersion " + jArray1[3]);
                    Main.Append(" --fml.mcVersion " + jArray1[5]);
                    Main.Append(" --fml.forgeGroup " + jArray1[7]);
                    Main.Append(" --fml.mcpVersion " + jArray1[9]);
                }
            }
            else if (jObject["id"].ToString().ToLower().Contains("optifine"))
            {
                string form = jObject["inheritsFrom"].ToString();
                JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                Main.Append(" --tweakClass optifine.OptiFineTweaker");
            }
            else
            {
                try { JObject jObject2 = JObject.Parse(jObject["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                try { Main.Append("  --version " + jObject["id"].ToString()); } catch { }
            }
            Main.Append(" --gameDir " + GamePath);
            if (Directory.Exists(GamePath + "\\assets"))
                Main.Append(" --assetsDir " + GamePath + "\\assets");
            Main.Append(" --uuid " + UUID);
            Main.Append(" --accessToken " + AccessToken);
            Main.Append(" --userProperties {}");
            Main.Append(" --versionType XMCL.Core");
            Main.Append(" --Wdith 854");
            Main.Append(" --Height 480");
            if (IsFullScreen == true)
                Main.Append(" --fullscreen");
            return Main.ToString();
            /*
            try
            {
                JObject jObject = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".json"));
                StringBuilder Main = new StringBuilder();
                StringBuilder CP = new StringBuilder();
                Main.Append(" -Xmx" + Memory + "M");
                Main.Append(MoreValue);
                Main.Append(" -Djava.library.path=" + GamePath + "\\bin");
                Main.Append(" -Dminecraft.launcher.brand=XMCL  -Dminecraft.launcher.version=2");
                if (jObject["id"].ToString().ToLower().Contains("forge"))
                {
                    string form = jObject["inheritsFrom"].ToString();
                    JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                    JArray jArray = JArray.Parse(jO["libraries"].ToString());
                    JArray jA = JArray.Parse(jObject["libraries"].ToString());
                    if (File.Exists(GamePath + "\\versions\\" + form + "\\" + form + ".jar"))
                    {
                        Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + form + "\\" + form + ".jar");
                    }
                    else
                    {
                        try
                        {
                            JObject jObject1 = JObject.Parse(jO["downloads"].ToString());
                            JObject jObject2 = JObject.Parse(jObject["client"].ToString());
                            Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + form + "\\" + form + ".jar");
                            Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                        }
                        catch (Exception ex)
                        { Check.CanLauch = false; System.Windows.Forms.MessageBox.Show(ex.Message); }
                    }
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        JObject jObject1 = JObject.Parse(jArray[i].ToString());
                        string a1 = jObject1["name"].ToString();
                        string[] vs = a1.Split(':');

                        bool IsNatives = false;
                        try
                        {
                            jObject1["natives"].ToString();
                            IsNatives = true;
                        }
                        catch { }
                        if (IsNatives == false)
                        {
                            if (vs[2] == "3.2.1")
                            { }
                            else
                            {
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                                if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                                { }
                                else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                                if (File.Exists(b1))
                                {

                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                    { CP.Append(b1 + ";"); }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                        CP.Append(b1 + ";");
                                    }
                                }
                                else
                                {
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            bool X;
                            try
                            {
                                JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                                jObject2["windows"].ToString();
                                X = true;
                            }
                            catch { X = false; }
                            if (X == true)
                            {
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                                JObject jObject4 = JObject.Parse(jObject3["natives-windows"].ToString());
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                                if (File.Exists(b1))
                                {
                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                    {
                                        Game.ZipList.Add(b1);
                                    }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            Game.downLoadHelper.JarsList.Add(b1);
                                            Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                            Game.ZipList.Add(b1);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < jA.Count; i++)
                    {
                        JObject jObject1 = JObject.Parse(jA[i].ToString());
                        string a1 = jObject1["name"].ToString();
                        string[] vs = a1.Split(':');
                        bool @new = true;
                        try
                        {
                            JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                            JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                            @new = true;
                        }
                        catch { @new = false; }
                        if (@new == true)
                        {
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            if (File.Exists(b1.Substring(0, b1.Length - 1)))
                            {
                                CP.Append(b1);
                            }
                            else
                            {
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                                if (jObject3["url"].ToString() == "")
                                { }
                                else
                                {
                                    Game.downLoadHelper.JarsList.Add(b1.Substring(0, b1.Length - 1));
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1);
                                }
                            }
                        }
                        else
                        {
                            string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            if (File.Exists(b1.Substring(0, b1.Length - 1)))
                            {
                                CP.Append(b1);
                            }
                            else
                            {
                                try
                                {
                                    Game.downLoadHelper.JarsList.Add(b1.Substring(0, b1.Length - 1));
                                    Game.downLoadHelper.JarURLsList.Add(jObject1["url"].ToString());
                                    CP.Append(b1);
                                }
                                catch { }
                            }
                        }
                    }
                    Main.Append(" -cp " + CP.ToString());
                    Main.Append(GamePath + "\\versions\\" + form + "\\" + form + ".jar ");
                }
                else if (jObject["id"].ToString().ToLower().Contains("optifine"))
                {
                    string form = jObject["inheritsFrom"].ToString();
                    JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                    JArray jArray = JArray.Parse(jO["libraries"].ToString());
                    JArray jA = JArray.Parse(jObject["libraries"].ToString());
                    if (File.Exists(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar"))
                    {
                        Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                    }
                    else
                    {
                        Check.CanLauch = false;
                    }
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        JObject jObject1 = JObject.Parse(jArray[i].ToString());
                        string a1 = jObject1["name"].ToString();
                        string[] vs = a1.Split(':');

                        bool IsNatives = false;
                        try
                        {
                            jObject1["natives"].ToString();
                            IsNatives = true;
                        }
                        catch { }
                        if (IsNatives == false)
                        {
                            if (vs[2] == "3.2.1")
                            { }
                            else
                            {
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                                if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                                { }
                                else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                                if (File.Exists(b1))
                                {
                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                    { CP.Append(b1 + ";"); }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                        CP.Append(b1 + ";");
                                    }
                                }
                                else
                                {
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            bool X;
                            try
                            {
                                JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                                jObject2["windows"].ToString();
                                X = true;
                            }
                            catch { X = false; }
                            if (X == true)
                            {
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                                JObject jObject4 = JObject.Parse(jObject3["natives-windows"].ToString());
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                                if (File.Exists(b1))
                                {
                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                    {
                                        Game.ZipList.Add(b1);
                                    }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                        Game.ZipList.Add(b1);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            Game.downLoadHelper.JarsList.Add(b1);
                                            Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                            Game.ZipList.Add(b1);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < jA.Count; i++)
                    {
                        JObject jObject1 = JObject.Parse(jA[i].ToString());
                        string a1 = jObject1["name"].ToString();
                        string[] vs = a1.Split(':');
                        string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar;";
                        if (File.Exists(b1.Substring(0, b1.Length - 1)))
                        {
                            CP.Append(b1);
                        }
                        else
                        {
                            Check.CanLauch = false;
                        }
                    }
                    Main.Append(" -cp " + CP.ToString());
                    Main.Append(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar ");
                }
                else
                {
                    JArray jArray = JArray.Parse(jObject["libraries"].ToString());
                    if (File.Exists(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar"))
                    {
                        JObject jObject1 = JObject.Parse(jObject["downloads"].ToString());
                        JObject jObject2 = JObject.Parse(jObject1["client"].ToString());
                        Main.Append(" -Dminecraft.client.jar=" + GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                        if (new FileInfo(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar").Length == Convert.ToInt64(jObject2["size"].ToString()))
                        { }
                        else
                        {
                            Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                            Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                        }
                    }
                    else
                    {
                        JObject jObject1 = JObject.Parse(jObject["downloads"].ToString());
                        JObject jObject2 = JObject.Parse(jObject1["client"].ToString());
                        Game.downLoadHelper.JarsList.Add(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar");
                        Game.downLoadHelper.JarURLsList.Add(jObject2["url"].ToString());
                    }
                    for (int i = 0; i < jArray.Count; i++)
                    {
                        JObject jObject1 = JObject.Parse(jArray[i].ToString());
                        string a1 = jObject1["name"].ToString();
                        string[] vs = a1.Split(':');

                        bool IsNatives = false;
                        try
                        {
                            jObject1["natives"].ToString();
                            IsNatives = true;
                        }
                        catch { }
                        if (IsNatives == false)
                        {
                            if (vs[2] == "3.2.1")
                            { }
                            else
                            {
                                string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + ".jar";
                                if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                                { }
                                else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                                JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                JObject jObject3 = JObject.Parse(jObject2["artifact"].ToString());
                                if (File.Exists(b1))
                                {
                                    if (new FileInfo(b1).Length == Convert.ToInt64(jObject3["size"].ToString()))
                                    { CP.Append(b1 + ";"); }
                                    else
                                    {
                                        File.Delete(b1);
                                        Game.downLoadHelper.JarsList.Add(b1);
                                        Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                        CP.Append(b1 + ";");
                                    }
                                }
                                else
                                {
                                    Game.downLoadHelper.JarsList.Add(b1);
                                    Game.downLoadHelper.JarURLsList.Add(jObject3["url"].ToString());
                                    CP.Append(b1 + ";");
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]))
                            { }
                            else { Directory.CreateDirectory(GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2]); }
                            bool X;
                            try
                            {
                                JObject jObject2 = JObject.Parse(jObject1["natives"].ToString());
                                jObject2["windows"].ToString();
                                X = true;
                            }
                            catch { X = false; }
                            if (X == true)
                            {
                                try
                                {
                                    JObject jObject2 = JObject.Parse(jObject1["downloads"].ToString());
                                    JObject jObject3 = JObject.Parse(jObject2["classifiers"].ToString());
                                    JObject jObject4 = JObject.Parse(jObject3["natives-windows"].ToString());
                                    string b1 = GamePath + "\\libraries\\" + vs[0].Replace(".", "\\") + "\\" + vs[1] + "\\" + vs[2] + "\\" + vs[1] + "-" + vs[2] + "-natives-windows.jar";
                                    if (File.Exists(b1))
                                    {
                                        if (new FileInfo(b1).Length == Convert.ToInt64(jObject4["size"].ToString()))
                                        {
                                            Game.ZipList.Add(b1);
                                        }
                                        else
                                        {
                                            File.Delete(b1);
                                            Game.downLoadHelper.JarsList.Add(b1);
                                            Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                            Game.ZipList.Add(b1);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            Game.downLoadHelper.JarsList.Add(b1);
                                            Game.downLoadHelper.JarURLsList.Add(jObject4["url"].ToString());
                                            Game.ZipList.Add(b1);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    Main.Append(" -cp " + CP.ToString());
                    Main.Append(GamePath + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".jar ");
                }
                Main.Append(jObject["mainClass"].ToString());
                Main.Append(" --username " + UserName);
                if (jObject["id"].ToString().ToLower().Contains("forge"))
                {
                    string[] a = jObject["id"].ToString().Split('-');
                    int b = Convert.ToInt32(a[0].Split('.')[1]);
                    if (b < 13)
                    {
                        string form = jObject["inheritsFrom"].ToString();
                        JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                        try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                        try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                        Main.Append(" --tweakClass net.minecraftforge.fml.common.launcher.FMLTweaker");
                    }
                    else
                    {
                        string form = jObject["inheritsFrom"].ToString();
                        JObject jObject5 = JObject.Parse(jObject["arguments"].ToString());
                        JArray jArray1 = JArray.Parse(jObject5["game"].ToString());
                        JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                        try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                        try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                        Main.Append(" --launchTarget " + jArray1[1]);
                        Main.Append(" --fml.forgeVersion " + jArray1[3]);
                        Main.Append(" --fml.mcVersion " + jArray1[5]);
                        Main.Append(" --fml.forgeGroup " + jArray1[7]);
                        Main.Append(" --fml.mcpVersion " + jArray1[9]);
                    }
                }
                else if (jObject["id"].ToString().ToLower().Contains("optifine"))
                {
                    string form = jObject["inheritsFrom"].ToString();
                    JObject jO = JObject.Parse(File.ReadAllText(GamePath + "\\versions\\" + form + "\\" + form + ".json"));
                    try { JObject jObject2 = JObject.Parse(jO["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                    try { Main.Append(" --version " + jObject["id"].ToString()); } catch { }
                    Main.Append(" --tweakClass optifine.OptiFineTweaker");
                }
                else
                {
                    try { JObject jObject2 = JObject.Parse(jObject["assetIndex"].ToString()); Main.Append(" --assetIndex " + jObject2["id"].ToString()); } catch { }
                    try { Main.Append("  --version " + jObject["id"].ToString()); } catch { }
                }
                Main.Append(" --gameDir " + GamePath);
                if (Directory.Exists(GamePath + "\\assets"))
                    Main.Append(" --assetsDir " + GamePath + "\\assets");
                Main.Append(" --uuid " + UUID);
                Main.Append(" --accessToken " + AccessToken);
                Main.Append(" --userProperties {}");
                Main.Append(" --versionType XMCL.Core");
                Main.Append(" --Wdith 854");
                Main.Append(" --Height 480");
                if (IsFullScreen == true)
                    Main.Append(" --fullscreen");
                return Main.ToString();
            }
            catch (Exception ex)
            {
                if (ex == null) { }
                else
                {
                    Check.CanLauch = false;
                    Game.Error.Append("无法构建启动参数:" + ex.Message + "\r\n");
                }
                return null;
            }*/
        }
        public static void Resource()
        {
            try
            {
                string a = GamePath;
                JObject jObject = JObject.Parse(File.ReadAllText(a + "\\versions\\" + Selected_Version + "\\" + Selected_Version + ".json"));
                if (ComplementaryResources)
                {
                    JObject jObject1;
                    if (jObject["id"].ToString().ToLower().Contains("forge"))
                    {
                        string z = jObject["inheritsFrom"].ToString();
                        jObject = JObject.Parse(File.ReadAllText(a + "\\versions\\" + z + "\\" + z + ".json"));
                        jObject1 = JObject.Parse(jObject["assetIndex"].ToString());
                    }
                    else
                    {
                        jObject1 = JObject.Parse(jObject["assetIndex"].ToString());
                    }
                    if (Directory.Exists(a + "\\assets\\indexes"))
                    { }
                    else Directory.CreateDirectory(a + "\\assets\\indexes");
                    if (File.Exists(a + "\\assets\\indexes\\" + jObject1["id"].ToString() + ".json"))
                    { }
                    else
                    {
                        Game.downLoadHelper.AssetsList.Add(a + "\\assets\\indexes\\" + jObject1["id"].ToString() + ".json");
                        Game.downLoadHelper.AssetURLsList.Add(jObject1["url"].ToString());
                    }
                    string b = File.ReadAllText(a + "\\assets\\indexes\\" + jObject1["id"].ToString() + ".json");
                    JObject json = JObject.Parse(b);
                    JObject jObject2 = JObject.Parse(json["objects"].ToString());
                    string[] vs = jObject2.ToString().Replace("\r\n", "").Replace(" ", "").Replace("},", "$").Split('$');
                    for (int i = 0; i < vs.Length; i++)
                    {
                        string a1 = vs[i] + "}";
                        string a2 = "{" + a1.Replace(":{", "$").Split('$')[1];
                        JObject jo = JObject.Parse(a2.Replace("}", "") + "}");
                        string c = jo["hash"].ToString().Substring(0, 2);
                        string d = "http://resources.download.minecraft.net/" + c + "/" + jo["hash"];
                        if (Directory.Exists(a + "\\assets\\objects\\" + c))
                        { }
                        else { Directory.CreateDirectory(a + "\\assets\\objects\\" + c); }
                        string e = a + "\\assets\\objects\\" + c + "\\" + jo["hash"];
                        if (File.Exists(e))
                        {
                            if (new FileInfo(e).Length == Convert.ToInt32(jo["size"].ToString()))
                            { }
                            else { Game.downLoadHelper.AssetsList.Add(e); Game.downLoadHelper.AssetURLsList.Add(d); }
                        }
                        else
                        {
                            Game.downLoadHelper.AssetsList.Add(e); Game.downLoadHelper.AssetURLsList.Add(d);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Game.Error.Append("无法补全资源文件" + ex.Message);
            }
        }

    }

}

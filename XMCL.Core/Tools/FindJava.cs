using Microsoft.Win32;
using System.Collections.Generic;

namespace XMCL.Core
{
    public partial class Tools
    {
        public static List<string> GetJavaList()
        {
            List<string> javas = new List<string>();
            RegistryKey registryJava = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");
            string[] a = registryJava.GetSubKeyNames();
            for (int i = 0; i < a.Length; i++)
            {
                RegistryKey reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment\\" + a[i]);
                string b = reg.GetValue("JavaHome").ToString() + "\\bin\\javaw.exe";

                if (System.IO.File.Exists(b))
                { }
                else continue;
                if (i == 0)
                    javas.Add(b);
                else
                {
                    bool x = false;
                    for (int o = 0; o < javas.Count; o++)
                    {
                        if (b == javas[o])
                        { x = true; }
                    }
                    if (x == false)
                    {
                        javas.Add(b);
                    }
                }
            }
            return javas;
        }
    }
}


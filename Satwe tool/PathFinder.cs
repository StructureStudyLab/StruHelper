using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace Satwe_tool
{
    internal class PathFinder
    {
        private static List<string> pathListPKPM;
		private static List<string> pathListYJK;
		public const string PKPM="pkpm";
		public const string YJK="yjk";

		public static List<string> GetYjkPath()
		{
			if (pathListYJK != null) {
				return pathListYJK;
			} else {
				pathListYJK = new List<string>();
			}

			try {
				string defaultSoftPath = @"C:\Users\All Users\yjkSoft\";
				if (!Directory.Exists(defaultSoftPath)){
					return pathListYJK;
				}
				string[] dirs = Directory.GetDirectories(defaultSoftPath, "YJK*");
				foreach (var item in dirs) {
					string[] files = Directory.GetFiles(item, "Startup.ini");
					foreach (var file in files) {
						string[] allLines = File.ReadAllLines(file, Encoding.Default);
						for (int i = 0; i < allLines.Length; i++) {
							if (allLines[i].Contains("FullName")) {
								for (int j = i + 1; ; j++) {
									string[] data = allLines[j].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
									if (data.Length > 1) {
										string path = data[1].Trim().Trim(new char[] { '"', '"' });
										if (File.Exists(path)) {
                                            if (!pathListYJK.Contains(path))
                                            {
                                                pathListYJK.Add(path);
                                            }											
										}										
									} else {
										break;
									}
								}
								break;
							}
						}
					}
				}
                LoadConfigYjkPath();
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
			return pathListYJK;
		}
        public static List<string> GetSatwePath()
        {            
            if (pathListPKPM!=null)
            {
                return pathListPKPM;
            }
            else
            {
                pathListPKPM = new List<string>();
            }
            
            try
            {               
                RegistryKey lm = Registry.CurrentUser;               
                RegistryKey path = lm.OpenSubKey(@"SOFTWARE\PKPM\MAIN\PATH", false);
                if (path==null)
                {
                    lm = Registry.LocalMachine;
                    path = lm.OpenSubKey(@"SOFTWARE\PKPM\MAIN\PATH", false);
                }
                if (path!=null)
                {
                    object pkpmPath=path.GetValue("CFG");
                    if (pkpmPath!=null)
                    {
                        string[] allLines=File.ReadAllLines(string.Format("{0}\\PKPM.ini",pkpmPath),Encoding.Default);
                        for (int i = 0; i < allLines.Length; i++)
                        {
                            if (allLines[i].Contains("WorkPath")&&allLines[i].Contains("="))
                            {
                                string onePath = allLines[i].Substring(allLines[i].IndexOf('=') + 1);
								if (Directory.Exists(onePath)) {
									string[] allFiles = Directory.GetFiles(onePath, "*.jws");
									foreach (var item in allFiles) {
                                        if (!pathListPKPM.Contains(item))
                                        {
                                            pathListPKPM.Add(item);
                                        }
										
									}
								}								
                            }
                        }
                    }
                }
                LoadConfigPkpmPath();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return pathListPKPM;
        }

        private static void LoadConfigPkpmPath()
        {
            if (pathListPKPM != null)
            {
                try
                {
                    //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    for (int i = 0; i < 100; i++)
                    {
                        string key = string.Format("LeftPath{0}", i);                        
                        string path=ConfigurationManager.AppSettings[key];
                        if (path != null)
                        {
                            if (File.Exists(path)&&!pathListPKPM.Contains(path))
                            {
                                pathListPKPM.Add(path);
                            }                            
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
                
            }            
            
        }
        private static void LoadConfigYjkPath()
        {
            if (pathListYJK != null)
            {
                try
                {                   
                    for (int i = 0; i < 100; i++)
                    {
                        string key = string.Format("RightPath{0}", i);
                        string path = ConfigurationManager.AppSettings[key];
                        if (path != null)
                        {
                            if (File.Exists(path) && !pathListYJK.Contains(path))
                            {
                                pathListYJK.Add(path);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }

            }

        }

        
    }
}

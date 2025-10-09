using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using pq.Model;
using pq.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ToastNotifications.Messages;

namespace pq.Helper
{
    public class City
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Province { get; set; }
        public string Name { get; set; }
    }
    public class NonSH
    {
        public NonSH()
        {
        }
        public StreamReader sr { get; set; }
        public int GetRealF { get; set; }
        public void GetF(object sender, DoWorkEventArgs e)
        {


            for (int i = 0; sr.Peek() >= 0; i++)
            {
                string s = sr.ReadLine();
                Application.Current.Dispatcher.Invoke(new Action(() => { (sender as BackgroundWorker).ReportProgress(i, s); }));
            }

        }
    }


    public static class Helper
    {
        public static bool IsSynched()
        {

            using (Entities ent = new Entities())
            {
                return ent.ExProes.Any(x => x.WinUsername == Environment.UserName && x.ExID != null);

            }

        }
        public static ExPro Synched()
        {

            using (Entities ent = new Entities())
            {

                List<ExPro> lexpro = ent.ExProes.Where(x => x.WinUsername == Environment.UserName && x.ExID != null).ToList();
                if (lexpro.Count > 0)
                {
                    if (lexpro.Count > 1) ModernDialog.ShowMessage("Multiple,grabbing 1st", "first", MessageBoxButton.OK);

                    return lexpro.FirstOrDefault();
                }
                else return null;


            }

        }
        public static string GetExPro()
        {
            using (Entities ent = new Entities())
            {
                ExPro expro = ent.ExProes.FirstOrDefault();
                if (expro != null)
                {
                    return expro.WinUsername + " | " + expro.ExUsername;
                }
                else
                {
                    //  ModernDialog.ShowMessage("first EXPRO load!", "fl", MessageBoxButton.OK);
                    ExPro newExpro = new ExPro();
                    newExpro.WinUsername = Environment.UserName;
                   // newExpro.WinDomain = Environment.UserDomainName;
                    ent.ExProes.Add(newExpro);

                    ent.SaveChanges();

                    return newExpro.WinUsername + " |" + newExpro.Id.ToString() + " NEW| " + newExpro.ExUsername;
                }
            }


        }
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static ExPro GetExPro(bool b)
        {
            using (Entities ent = new Entities())
            {
                ExPro expro = ent.ExProes.Where(x=>x.WinUsername== Environment.UserName).FirstOrDefault();
                if (expro != null)
                {
                    return expro;
                }
                else
                {
                    // ModernDialog.ShowMessage("first EXPRO load!", "fl", MessageBoxButton.OK);
                    ExPro newExpro = new ExPro();
                    newExpro.WinUsername = Environment.UserName;
                    //newExpro.WinDomain = Environment.UserDomainName;

                    ent.ExProes.Add(newExpro);

                    ent.SaveChanges();

                    return newExpro;
                }
            }


        }
        //public static Achievements GetAch()
        //{
        //    using (ExtensionlessBaseEntities ent = new ExtensionlessBaseEntities())
        //    {


        //        Achievements achievements = ent.Achievements.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
        //        if (achievements == null)
        //        {
        //            achievements = new Achievements() { ExProID = GetExPro(true).Id };
        //            ent.Achievements.Add(achievements);
        //            ent.SaveChanges();
        //        }
        //        return achievements;

        //    }



        //}
        public static string GetGuid()
        {
            using (Entities ent = new Entities())
            {
                Inst inst = ent.Insts.FirstOrDefault();
                if (inst != null)
                {
                    return inst.Guid;
                }
                else
                {
                    //  ModernDialog.ShowMessage("first load!", "fl", MessageBoxButton.OK);
                    Inst newInst = new Inst();
                    newInst.Guid = Guid.NewGuid().ToString();
                    ent.Insts.Add(newInst);

                    ent.SaveChanges();

                    return newInst.Guid;
                }
            }


        }
        public static RegistryKey RegBase { get; set; }
        public static string Username { get { return Environment.UserName; } }
        public static SplitViewModel svm { get; set; }
        public static List<FileExtensionItem> All { get; set; }
        public static List<ExTemplate> Tmpl { get; set; }
        public static FrameworkElement CurentScreen { get; set; }
        public static List<Ex> LEX { get; set; }
        public static Home MyHome { get; set; }
        public static Setting ST { get; set; }
        public static List<FileExtensionItem> GetMine()
        {
            return All.Where(x => x.IsMine == true).ToList();
        }

        public static LinkCollection GetTmplLinks()
        {
            LinkCollection lc = new LinkCollection();
            Link l0 = new Link();
            l0.DisplayName = "- Community -";
            l0.Source = new Uri("/Pages/UTemplatePack.xaml", UriKind.RelativeOrAbsolute);
            lc.Add(l0);
            for (int i = 0; i < Tmpl.Count; i++)
            {
                Link l = new Link();
                l.DisplayName = (i + 1).ToString() + ". " + Tmpl[i].Username;
                l.Source = new Uri("/Pages/ExTemplateItem.xaml", UriKind.RelativeOrAbsolute);
                lc.Add(l);
            }

            return lc;
        }
        public static void getDB(object sender, DoWorkEventArgs e)
        {
            using (var ent = new Entities())
            {
                List<Ex> lex = ent.Exes.Where(x => true).ToList();
                LEX = lex;

                Setting st = ent.Settings.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                if (st == null)
                {
                    st = new Setting() { IsExtended = false, DoAsk = true, IsMine = false, ExProID = GetExPro(true).Id };
                    ent.Settings.Add(st);
                    ent.SaveChanges();
                }
                ST = st;

                string[] WorldArray = File.ReadAllLines("World/World.csv");
                List<City> wc = new List<City>();
                for (int i = 0; i < WorldArray.Length; i++)
                {
                    string[] cols = WorldArray[i].Split(',');
                    City c = new City();
                    c.Id = i + 1;
                    c.Name = cols[0].Trim('"');
                    c.CountryCode = cols[5].Trim('"');
                    c.CountryName = cols[4].Trim('"');
                    c.Province = cols[7].Trim('"');
                    wc.Add(c);
                }

                WorldCities = wc;
            }
        }
        public static void getDBST()
        {
            using (var ent = new Entities())
            {

                Setting st = ent.Settings.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                ST = st;
            }
        }
        public static void getDBST2(int exproid)
        {
            using (var ent = new Entities())
            {

                Setting st = ent.Settings.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                if (st == null)
                {
                    st = new Setting() { IsExtended = false, DoAsk = true, IsMine = false, ExProID = exproid };
                    ent.Settings.Add(st);
                    ent.SaveChanges();
                }
            }
        }
        public static List<string> GetAllUsernames()
        {
            List<string> ls = new List<string>();
            SelectQuery sQuery = new SelectQuery("Win32_UserAccount");

            try
            {
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(sQuery);


                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    ls.Add(mObject["Name"].ToString());
                }
                return ls;
            }
            catch (Exception exce)

            {
                return null;
            }
        }
        public static Color GetAccent(int ind)
        {
            Color[] accentColors = new Color[]{

            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

            return accentColors[ind];
        }
        public static bool IsTmplLoaded = false;
        public static bool WriteLog(string logItem, bool isNewLine = true, bool isItorIsitNot = false)
        {
            string result = Path.GetTempPath();
            string fileName = result + "Extensionless-Log.txt";
            try
            {


                // Create a new file     
                using (StreamWriter fajl = new StreamWriter(fileName, true))
                {
                    string author = (isNewLine ? "< " + DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString() + " - " + RegBase + " - " + Environment.UserName + "> " : " | ") + logItem;
                    if (isNewLine)
                    {
                        fajl.WriteLine(author);
                    }
                    else
                    {
                        fajl.Write(author);
                        if (isItorIsitNot) fajl.WriteLine();
                    }
                    return true;
                }

                // Open the stream and read it back.    

            }
            catch (Exception Ex)
            {
                ModernDialog.ShowMessage(Ex.Message, "sss", MessageBoxButton.OK);
                return false;
            }

        }

        public static int getFRM(FrameworkElement fwe)
        {
            return 1;
        }
        public static Folder F;
        /*   public static LinkCollection GetLL(Folder f)
           {
               F = f;
               LinkCollection ll = new LinkCollection();
               for (int i = 0; i < Tmpl[(int)f].Length; i++)
               {
                   FileInfo b = Tmpl[(int)f][i];
                   Link l = new Link();
                   l.DisplayName = b.Name.Split('.')[1] + " | " + b.Name.Split('.')[0];
                   l.Source = new Uri("/Pages/Frame" + ((int)f == 0 ? "" : "2") + ".xaml", UriKind.RelativeOrAbsolute);

                   ll.Add(l);
               }
               /*     foreach (int i in fileInfos)
                    {

                    }
               return ll;
           }*/

        public static int InsertMyFEx(MyFEx myfex)
        {
            using (Entities entities = new Entities())
            {
                Ex ex = new Ex();
                ex.IsUsed = myfex.IsUsed;
                ex.IsBinary = myfex.IsBinary;
                ex.IsOpen = myfex.IsOpen;
                ex.Name = myfex.Name;
                ex.FullName = myfex.FullName;
                ex.FT = ((int)myfex.FT).ToString();
                ex.FEMTE = ((int)myfex.FEMTE).ToString();
                ex.IsExtended = false;
                ex.IsMine = true;
                ex.Last = DateTime.Now;

                entities.Exes.Add(ex);
                try
                {
                    entities.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }

                return ex.Id;
            }
        }
        public static void getall(object sender, DoWorkEventArgs e)
        {

            try
            {
                int i = 0;
                WriteLog("| Started load |> ");
                List<FileExtensionItem> l = new List<FileExtensionItem>();

                foreach (var ex in LEX)
                {
                    i += 1;
                    //String name = Enum.GetName(typeof(FileExtensionTypeEnum), i);
                    String name = ex.Name;
                    bool isEnabled;

                    isEnabled = GetRegKeyBool(name == "EXTENSIONLESS" ? "" : name);

                    Enum.TryParse(name, out FileExtensionTypeEnum fete);
                    string tmplID;
                    if (isEnabled)
                    {
                        tmplID = GetTemplateID(name == "EXTENSIONLESS" ? "" : name);
                    }
                    else
                    {
                        tmplID = "";
                    }
                    FyleTipe ft = GetFTbyFETE(fete);

                    Color color = GetColor(rnd.Next() % 9);
                    SolidColorBrush isDark1 = IsDark(color);
                    string isDark = isDark1.ToString();
                    byte y = 81;
                    Color color2;
                    if (color.GetBrightness() > isDark1.Color.GetBrightness())
                    {
                        color2 = Color.FromRgb((byte)(color.R - y), (byte)(color.G - y), (byte)(color.B - y));
                    }
                    else
                    {
                        color2 = Color.FromRgb((byte)(color.R + y), (byte)(color.G + y), (byte)(color.B + y));
                    }

                    //  color2.Clamp();

                    FileExtensionItem fe = new FileExtensionItem()
                    {
                        ID = ex.Id,
                        Name = name == "EXTENSIONLESS" ? "." : "." + name,
                        FullName = GetFullNameByFETE(fete),
                        IsEnabled = isEnabled,
                        IsOpen = ex.IsOpen,
                        IsDark = isDark,
                        IsBinary = ex.IsBinary,
                        TemplateID = tmplID,
                        FEColor = color,
                        FETE = fete,
                        FEMTE = GetFEMTEbyFETE(fete),
                        FT = ft,
                        PGB = color2,
                        IsExtended = ex.IsExtended,
                        IsMine = ex.IsMine,
                        Last = ex.Last,
                        IsUsed = ex.IsUsed
                    };

                    l.Add(fe);
                    (sender as BackgroundWorker).ReportProgress(i, fe);
                    string g = (bool)fe.IsEnabled ? (fe.TemplateID == "0" ? " ◼" : " ♥ " + fe.TemplateID) : " ◻";

                    WriteLog(fe.Name + g, false, i == 124);

                }

                All = l.OrderBy(x => x.Name).ToList();
                WriteLog("<| Ended load |");

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            try
            {
                svm.IsLoading = false;
                svm.Notify();
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message);

            }


            /*
                foreach (int i in Enum.GetValues(typeof(FileExtensionTypeEnum)))
                {

                    String name = Enum.GetName(typeof(FileExtensionTypeEnum), i);
                    bool isEnabled;

                    isEnabled = GetRegKeyBool(name == "EXTENSIONLESS" ? "" : name);



                    Enum.TryParse(name, out FileExtensionTypeEnum fete);
                    string tmplID;
                    if (isEnabled)
                    {
                        tmplID = GetTemplateID(name == "EXTENSIONLESS" ? "" : name);
                    }
                    else
                    {
                        tmplID = "";
                    }
                    FyleTipe ft = GetFTbyFETE(fete);

                    Color color = GetColor(rnd.Next() % 9);
                    SolidColorBrush isDark1 = IsDark(color);
                    string isDark = isDark1.ToString();
                    byte y = 81;
                    Color color2;
                    if (color.GetBrightness() > isDark1.Color.GetBrightness())
                    {
                        color2 = Color.FromRgb((byte)(color.R - y), (byte)(color.G - y), (byte)(color.B - y));
                    }
                    else
                    {
                        color2 = Color.FromRgb((byte)(color.R + y), (byte)(color.G + y), (byte)(color.B + y));
                    }


                    //  color2.Clamp();

                    FileExtensionItem fe = new FileExtensionItem()
                    {
                        ID = i,
                        Name = name == "EXTENSIONLESS" ? "." : "." + name,
                        FullName = GetFullNameByFETE(fete),
                        IsEnabled = isEnabled,
                        IsOpen = GetRegKey(1),
                        IsDark = isDark,
                        IsBinary = GetRegKey(1),
                        TemplateID = tmplID,
                        FEColor = color,
                        FETE = fete,
                        FEMTE = GetFEMTEbyFETE(fete),
                        FT = ft,
                        PGB = color2
                    };

                    l.Add(fe);
                    (sender as BackgroundWorker).ReportProgress(i, fe);
                    string g = (bool)fe.IsEnabled ? (fe.TemplateID == "0" ? " ◼" : " ♥ " + fe.TemplateID) : " ◻";

                    WriteLog(fe.Name + g, false, i == 124);

                }
                All = l.OrderBy(x => x.Name).ToList();
                WriteLog("<| Ended load |");
                try
                {
                    svm.Notify();
                }
                catch (Exception eee)
                {


                }*/

        }

        public static void DBWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BD.DialogResult = true;
            BD.Close();

            (Application.Current.MainWindow as MainWindow).notifier.ShowInformation("+ + + DB");
        }

        internal static List<FileExtensionItem> GetFetePicks()
        {
            List<FileExtensionItem> l = new List<FileExtensionItem>();
            l = All.Where(x => (bool)x.IsEnabled).ToList();
            return l;
        }
        public static void Nothing()
        { }

        public static string GetLog()
        {
            string result = Path.GetTempPath();
            string fileName = result + "Extensionless-Log.txt";
            string[] lines = System.IO.File.ReadAllLines(fileName);
            string retStr = "";
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                retStr += "\n" + line;
            }
            return retStr;
        }

        internal static List<FileExtensionItem> GetFeteAll()
        {
            return All.ToList();
        }

        public static List<FileExtensionItem> GetFeteByFyle(int fyle)
        {

            List<FileExtensionItem> l = new List<FileExtensionItem>();
            l = All.Where(x => (int)x.FT == fyle).ToList();
            return l;
        }
        public static Random rnd = new Random();
        public static MediaPlayer mediaPlayer;
        public static ModernWindow wnd;

        public static bool GetRegKey(int name)
        {
            if (name == 1)
            {
                return GetRandomBool(1);
            }
            return GetRandomBool(0);
        }
        public static bool GetRegKeyBool(String name)
        {

            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {
                        using (winLogonKey = RegBase.OpenSubKey(s, new RegistryKeyPermissionCheck()))
                        {


                            if (winLogonKey != null)
                            {
                                string[] S = winLogonKey.GetSubKeyNames();

                                winLogonKey.Close();
                                return S.Contains("ShellNew");

                            }
                            else
                            {
                                ModernDialog.ShowMessage("Fail", "fail", MessageBoxButton.OK);

                                winLogonKey.Close();
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return false;
                    }



                }
                else
                {

                    return false;


                }


            }
            catch (Exception ee)
            {
                return false;
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }


        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static string GetRegKeyFile(String name)
        {

            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {
                        using (winLogonKey = RegBase.OpenSubKey(s, new RegistryKeyPermissionCheck()))
                        {


                            if (winLogonKey != null)
                            {
                                string[] S = winLogonKey.GetSubKeyNames();
                                if (S.Contains("ShellNew"))
                                {
                                    using (RegistryKey rk = winLogonKey.OpenSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        object tmpl = rk.GetValue("FileName");
                                        if (tmpl == null)
                                        {
                                            return "0";
                                        }
                                        else
                                        {
                                            return tmpl.ToString();
                                        }

                                    }
                                }
                                else
                                {

                                    winLogonKey.Close();
                                    return "";
                                }



                            }
                            else
                            {
                                ModernDialog.ShowMessage("Fail", "fail", MessageBoxButton.OK);

                                winLogonKey.Close();
                                return "";
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return "";
                    }



                }
                return "";


            }
            catch (Exception)
            {
                return "";
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }

        public static bool SetRegKeyBool(String name)
        {

            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {


                        //  winLogonKey = Registry.ClassesRoot.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        using (winLogonKey = RegBase.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {


                            if (winLogonKey != null)
                            {
                                if (string.IsNullOrEmpty(winLogonKey.GetValue("", "").ToString()))
                                {
                                    if (ModernDialog.ShowMessage("Create key?", "Repair", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                                    {
                                        //RegistryKey newrkey = rkey.CreateSubKey("." + name);
                                        winLogonKey.SetValue("", name);
                                        winLogonKey.SetValue("PerceivedType", "text");
                                        winLogonKey.SetValue("ContentType", "text/plain");
                                        RegistryKey sn = winLogonKey.CreateSubKey("ShellNew", true);
                                        sn.SetValue("NullFile", "");
                                        RegistryKey newestrkey = rkey.CreateSubKey(name, true);
                                        newestrkey.SetValue("", name);
                                        WriteLog("Repair type A - " + name);
                                        return true;
                                    }
                                    else return false;
                                }


                                string[] S = winLogonKey.GetSubKeyNames();
                                if (S.Contains("ShellNew"))
                                {
                                    //   ModernDialog.ShowMessage("alerre", "ex", MessageBoxButton.OK);
                                }
                                else
                                {

                                    // ModernDialog.ShowMessage("y", "na", MessageBoxButton.OK);
                                    using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew",RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        //  ModernDialog.ShowMessage("1", "na", MessageBoxButton.OK);
                                        //  rk.SetValue("", null);
                                        rk.SetValue("NullFile", "");
                                        WriteLog("Added new file -|- " + name);
                                        (Application.Current.MainWindow as MainWindow).notifier.ShowWarning("Added empty template " + name);
                                        return true;
                                        //  ModernDialog.ShowMessage("2", "na", MessageBoxButton.OK);
                                    }


                                    //      ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);
                                }

                            }
                            else
                            {
                                //  ModernDialog.ShowMessage("Fail", "fail", MessageBoxButton.OK);
                                throw new Exception("sjebosirođo");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return false;
                    }



                }
                else
                {
                    if (ModernDialog.ShowMessage("Create key?", "Repair", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        RegistryKey newrkey = rkey.CreateSubKey("." + name);
                        newrkey.SetValue("", name);
                        newrkey.SetValue("PerceivedType", "text");
                        newrkey.SetValue("ContentType", "text/plain");
                        RegistryKey sn = newrkey.CreateSubKey("ShellNew", true);
                        sn.SetValue("NullFile", "");
                        RegistryKey newestrkey = rkey.CreateSubKey(name, true);
                        newestrkey.SetValue("", name);
                        WriteLog("Repair type B - " + name);
                        return true;
                    }
                    else return false;
                }
                // ModernDialog.ShowMessage("???", "fail", MessageBoxButton.OK);
                return false;


            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                return false;
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }
        public static bool SetRegKeyInvBool(String name)
        {

            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {


                        //  winLogonKey = Registry.ClassesRoot.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        using (winLogonKey = RegBase.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {


                            if (winLogonKey != null)
                            {
                                winLogonKey.DeleteSubKey("ShellNew", true);
                                winLogonKey.DeleteSubKey("FileName", false);
                                (Application.Current.MainWindow as MainWindow).notifier.ShowError("Removed template " + name);
                                SystemSounds.Hand.Play();
                                WriteLog("Removed -||- " + name);
                                return true;

                                //     ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);

                            }
                            else
                            {
                                throw new Exception("sjebosirođo");

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return false;
                    }



                }
                else
                {
                    throw new Exception("sjebosirođo111");  // ModernDialog.ShowMessage("GADAN", "fail", MessageBoxButton.OK);
                }
                // ModernDialog.ShowMessage("???", "fail", MessageBoxButton.OK);
          


            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                return false;
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }


        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        public static bool SetRegKeyFile(String name, String fileName)
        {


            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {
                        //  winLogonKey = Registry.ClassesRoot.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        using (winLogonKey = RegBase.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {


                            if (winLogonKey != null)
                            {
                                string[] S = winLogonKey.GetSubKeyNames();
                                if (S.Contains("ShellNew"))
                                {
                                    //    ModernDialog.ShowMessage("alerre", "ex", MessageBoxButton.OK);
                                    //  ModernDialog.ShowMessage("y", "na", MessageBoxButton.OK);
                                    using (RegistryKey rk = winLogonKey.OpenSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew",RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        //          ModernDialog.ShowMessage("1", "na", MessageBoxButton.OK);
                                        //  rk.SetValue("", null);
                                        rk.DeleteValue("NullFile", true);
                                        rk.DeleteValue("FileName", false);
                                        rk.SetValue("FileName", fileName);
                                        (Application.Current.MainWindow as MainWindow).notifier.ShowSuccess("Added template " + name + " | " + fileName);
                                        SystemSounds.Hand.Play();
                                        WriteLog("ADDED TEMPLATE type A ♥ " + name + "|" + fileName);
                                        //      ModernDialog.ShowMessage("2", "na", MessageBoxButton.OK);
                                    }


                                    //   ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);
                                }
                                else
                                {

                                    //        ModernDialog.ShowMessage("y", "na", MessageBoxButton.OK);
                                    using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew",RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        //        ModernDialog.ShowMessage("1", "na", MessageBoxButton.OK);
                                        //  rk.SetValue("", null);

                                        rk.SetValue("FileName", fileName);
                                        WriteLog("ADDED TEMPLATE type B ♥ " + name + "|" + fileName);

                                        //       ModernDialog.ShowMessage("2", "na", MessageBoxButton.OK);
                                    }


                                    //       ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);
                                }

                            }
                            else
                            {
                                //        ModernDialog.ShowMessage("Fail", "fail", MessageBoxButton.OK);
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return false;
                    }



                }
                else
                {
                    //   ModernDialog.ShowMessage("GADAN", "fail", MessageBoxButton.OK);
                }
                //   ModernDialog.ShowMessage("???", "fail", MessageBoxButton.OK);
                return false;


            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                return false;
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }
        public static bool SetRegKeyInvFile(String name)
        {

            name = name.ToLower();
            String s = name == "" ? "*" : "." + name;
            RegistryKey rkey = RegBase;
            //  RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, @"HKEY_CLASSES_ROOT\"+s);
            try
            {
                string[] RS = RegBase.GetSubKeyNames();
                if (RS.Contains(s))
                {
                    RegistryKey winLogonKey;
                    try
                    {
                        //  winLogonKey = Registry.ClassesRoot.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        using (winLogonKey = RegBase.OpenSubKey(s, RegistryKeyPermissionCheck.ReadWriteSubTree))
                        {


                            if (winLogonKey != null)
                            {
                                string[] S = winLogonKey.GetSubKeyNames();
                                if (S.Contains("ShellNew"))
                                {
                                    //    ModernDialog.ShowMessage("alerre", "ex", MessageBoxButton.OK);
                                    //  ModernDialog.ShowMessage("y", "na", MessageBoxButton.OK);
                                    using (RegistryKey rk = winLogonKey.OpenSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew",RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        //          ModernDialog.ShowMessage("1", "na", MessageBoxButton.OK);
                                        //  rk.SetValue("", null);
                                        rk.DeleteValue("NullFile", false);
                                        rk.DeleteValue("FileName", true);
                                        rk.SetValue("NullFile", "");
                                        (Application.Current.MainWindow as MainWindow).notifier.ShowWarning("Removed template " + name + " | Empty template");
                                        SystemSounds.Hand.Play();
                                        //      ModernDialog.ShowMessage("2", "na", MessageBoxButton.OK);
                                        WriteLog("Removed TEMPLATE ♦ ♣ ♠ " + name + "|");

                                    }


                                    //   ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);
                                }
                                else
                                {
                                    throw new Exception("shit happens");
                                    //        ModernDialog.ShowMessage("y", "na", MessageBoxButton.OK);
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew", RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    //using (RegistryKey rk = winLogonKey.CreateSubKey("ShellNew",RegistryKeyPermissionCheck.ReadWriteSubTree))
                                    {
                                        //        ModernDialog.ShowMessage("1", "na", MessageBoxButton.OK);
                                        //  rk.SetValue("", null);

                                        //rk.SetValue("NullFile", "");
                                        //       ModernDialog.ShowMessage("2", "na", MessageBoxButton.OK);
                                    }


                                    //       ModernDialog.ShowMessage("a", "na", MessageBoxButton.OK);
                                }

                            }
                            else
                            {
                                //        ModernDialog.ShowMessage("Fail", "fail", MessageBoxButton.OK);
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                        return false;
                    }



                }
                else
                {
                    //   ModernDialog.ShowMessage("GADAN", "fail", MessageBoxButton.OK);
                }
                //   ModernDialog.ShowMessage("???", "fail", MessageBoxButton.OK);
                return false;


            }
            catch (Exception e)
            {
                ModernDialog.ShowMessage(e.Message, "fail", MessageBoxButton.OK);
                return false;
            }
            //return (winLogonKey.GetValueNames().Contains("Start"));
        }

        public static int SelButt(int numberOf)
        {
            return rnd.Next() % numberOf;
        }

        public static string GetFullNameByFETE(FileExtensionTypeEnum fete)
        {
            switch (fete)
            {
                case FileExtensionTypeEnum.EXTENSIONLESS:
                    return "Extensionless";
                case FileExtensionTypeEnum.TORRENT:
                    return "BitTorrent P2P File";
                case FileExtensionTypeEnum.XYZ:
                    return "ɛks waɪ ɪzərd";
                case FileExtensionTypeEnum.FILE:
                    return "Generic Windows File";
                case FileExtensionTypeEnum.GDC:
                    return "Godot Engine Compiled Script";
                case FileExtensionTypeEnum.MDF:
                    return "SQL Server Database File";
                case FileExtensionTypeEnum.GEN:
                    return "Sega Genesis ROM";
                case FileExtensionTypeEnum.GALAXY:
                    return "Blizzard Galaxy File";
                case FileExtensionTypeEnum.FUK:
                    return "Postal 2 Map File";
                case FileExtensionTypeEnum.GB:
                    return "Game Boy ROM File";
                case FileExtensionTypeEnum.GAME:
                    return "GameSalad Exported Game File";
                case FileExtensionTypeEnum.GAM:
                    return "Saved Game File";
                case FileExtensionTypeEnum.CTY:
                    return "SimCity City File";
                case FileExtensionTypeEnum.GRP:
                    return "StarCraft Graphics Group File";
                case FileExtensionTypeEnum.GXT:
                    return "GTA Dialogue File";
                case FileExtensionTypeEnum.H3M:
                    return "Heroes 3 Map File";
                case FileExtensionTypeEnum.H4R:
                    return "Heroes of Might and Magic IV Data File";
                case FileExtensionTypeEnum.J2L:
                    return "Jazz Jackrabit 2 Level File";
                case FileExtensionTypeEnum.LMU:
                    return "RPG Maker MAp File";
                case FileExtensionTypeEnum.MAP:
                    return "Compiled Map file";
                case FileExtensionTypeEnum.MM7:
                    return "M&M 7 save game";
                case FileExtensionTypeEnum.MP2S:
                    return "Max Payne 2 Save";
                case FileExtensionTypeEnum.NARC:
                    return "Nintendo DS Archive Filen";
                case FileExtensionTypeEnum.NDS:
                    return "Nintendo DS Game ROM";
                case FileExtensionTypeEnum.PSV:
                    return "Playstation 2 save file";
                case FileExtensionTypeEnum.PSK:
                    return "Unreal Engine Skeletal Mesh";
                case FileExtensionTypeEnum.SC2MAPS:
                    return "StarCraft 2 Map File";
                case FileExtensionTypeEnum.SC4:
                    return "Sim City 4 saved City";
                case FileExtensionTypeEnum.UNITY:
                    return "Unity Scene File";
                case FileExtensionTypeEnum.UT2:
                    return "Unreal Tournament Map";
                case FileExtensionTypeEnum.ZZZ:
                    return "Black & White Game Data";
                case FileExtensionTypeEnum.GSC:
                    return "Call of Duty Game Script";
                case FileExtensionTypeEnum.PGN:
                    return "Chess Portable Game Notation";
                case FileExtensionTypeEnum.EML:
                    return "Email File Format";
                case FileExtensionTypeEnum.VB:
                    return "Visual Basic";
                case FileExtensionTypeEnum.DART:
                    return "Dart";
                case FileExtensionTypeEnum.PHP:
                    return "Hypertext Preprocessor";
                case FileExtensionTypeEnum.PY:
                    return "Phyton";
                case FileExtensionTypeEnum.RB:
                    return "Ruby";
                case FileExtensionTypeEnum.JSX:
                    return "JavaScript XML";
                case FileExtensionTypeEnum.JS:
                    return "JavaScript";
                case FileExtensionTypeEnum.ES:
                    return "ECMA Script";
                case FileExtensionTypeEnum.TS:
                    return "TypeScript";
                case FileExtensionTypeEnum.CSS:
                    return "Cascading Stylesheet";
                case FileExtensionTypeEnum.LESS:
                    return "Leaner Style Sheets";
                case FileExtensionTypeEnum.CS:
                    return "C# (sharp)";
                case FileExtensionTypeEnum.CPP:
                    return "C++";
                case FileExtensionTypeEnum.XAML:
                    return "Extensible App. Markup Lang.";
                case FileExtensionTypeEnum.XML:
                    return "EXtensible Markup Language";
                case FileExtensionTypeEnum.HTML:
                    return "Hypertext Markup Language";
                case FileExtensionTypeEnum.PDF:
                    return "Portable Document Format";
                case FileExtensionTypeEnum.DOC:
                    return "Document";
                case FileExtensionTypeEnum.DOCX:
                    return "Office Open XML";
                case FileExtensionTypeEnum.ODT:
                    return "Office Open Document";
                case FileExtensionTypeEnum.RTF:
                    return "Rich Text File";
                case FileExtensionTypeEnum.TXT:
                    return "Plain Text File";
                case FileExtensionTypeEnum.PNG:
                    return "Portable Network Graphics";
                case FileExtensionTypeEnum.JPEG:
                    return "Joint Photo. Experts Group";
                case FileExtensionTypeEnum.PDN:
                    return "Pain .NET";
                case FileExtensionTypeEnum.GIF:
                    return "Graphic Interchange Format";
                case FileExtensionTypeEnum.EPS:
                    return "Encapsulated PostScript";
                case FileExtensionTypeEnum.SVG:
                    return "Scalable Vector Graphics";
                case FileExtensionTypeEnum.ASP:
                    return "Active Server Pages";
                case FileExtensionTypeEnum.ASPX:
                    return "Active Server Page Framework";
                case FileExtensionTypeEnum.CSHTML:
                    return "C# 6 HTML | Razor";
                case FileExtensionTypeEnum.LOG:
                    return "Log Files";
                case FileExtensionTypeEnum.BAT:
                    return "Batch Files";
                case FileExtensionTypeEnum.ISO:
                    return "ISO Image";
                case FileExtensionTypeEnum.ZIP:
                    return "Zip Archive";
                case FileExtensionTypeEnum.RAR:
                    return "Roshal Archive";
                case FileExtensionTypeEnum.APK:
                    return "Android application package";
                case FileExtensionTypeEnum.EXE:
                    return "Executible File";
                case FileExtensionTypeEnum.CFG:
                    return "Configuration file";
                case FileExtensionTypeEnum.XLS:
                    return "Excel Spreadsheet format";
                case FileExtensionTypeEnum.XLSX:
                    return "Office Open XML";
                case FileExtensionTypeEnum.ODS:
                    return "OpenDocument Spreadsheets";
                case FileExtensionTypeEnum.BAK:
                    return "Backup file";
                case FileExtensionTypeEnum.H:
                    return "C/C++ Heading File";
                case FileExtensionTypeEnum.JAVA:
                    return "Java Oracle";
                case FileExtensionTypeEnum.C:
                    return "The C";
                case FileExtensionTypeEnum.CLASS:
                    return "Java Class File";
                case FileExtensionTypeEnum.SH:
                    return "Computer Shell";
                case FileExtensionTypeEnum.ICO:
                    return "Icon File";
                case FileExtensionTypeEnum.DLL:
                    return "Dynamic Link Library";
                case FileExtensionTypeEnum.DMP:
                    return "Dump File";
                case FileExtensionTypeEnum.MP4:
                    return "MPEG4 video file";
                case FileExtensionTypeEnum.MPEG:
                    return "Moving Pictures Expert Group";
                case FileExtensionTypeEnum.AVI:
                    return "Audio Video Interleav";
                case FileExtensionTypeEnum.MKV:
                    return "Matroska Multimedia Container";
                case FileExtensionTypeEnum.MOV:
                    return "Apple QuickTime movie file";
                case FileExtensionTypeEnum.WMV:
                    return "Windows Mesia Video";
                case FileExtensionTypeEnum.SYS:
                    return "System File";
                case FileExtensionTypeEnum.MSI:
                    return "Windows installer package";
                case FileExtensionTypeEnum.LNK:
                    return "Windows Link File";
                case FileExtensionTypeEnum.INI:
                    return "Initialization file";
                case FileExtensionTypeEnum.TMP:
                    return "Temporary files";
                case FileExtensionTypeEnum.CAB:
                    return "Cabinet Archive File";
                case FileExtensionTypeEnum.PPT:
                    return "PowerPoint presentation";
                case FileExtensionTypeEnum.PPTX:
                    return "PowerPoint Open XML presentation";
                case FileExtensionTypeEnum.ODP:
                    return "O.O. Impress presentation";
                case FileExtensionTypeEnum.XHTML:
                    return "Extensible Hypertext Markup Lang.";
                case FileExtensionTypeEnum.CGI:
                    return "Perl";
                case FileExtensionTypeEnum.PL:
                    return "Perl";
                case FileExtensionTypeEnum.RSS:
                    return "Really Simple Syndication XML";
                case FileExtensionTypeEnum.BMP:
                    return "Bitmap image";
                case FileExtensionTypeEnum.OTF:
                    return "Open type font file";
                case FileExtensionTypeEnum.TTF:
                    return "TrueType font file";
                case FileExtensionTypeEnum.FON:
                    return "Generic font file";
                case FileExtensionTypeEnum.FNT:
                    return "Windows font file";
                case FileExtensionTypeEnum.COM:
                    return "fMS - DOS command fil";
                case FileExtensionTypeEnum.BIN:
                    return "Binary file";
                case FileExtensionTypeEnum.JAR:
                    return "Java Archive file";
                case FileExtensionTypeEnum.MDB:
                    return "Microsoft Access D.B. file";
                case FileExtensionTypeEnum.DAT:
                    return "Data file";
                case FileExtensionTypeEnum.DB:
                    return "Database file";
                case FileExtensionTypeEnum.DBF:
                    return "Database file";
                case FileExtensionTypeEnum.CSV:
                    return "Comma separated value file";
                case FileExtensionTypeEnum.SAV:
                    return "Save File";
                case FileExtensionTypeEnum.TAR:
                    return "Linux / Unix tarball archive";
                case FileExtensionTypeEnum.WMA:
                    return "WMA audio file";
                case FileExtensionTypeEnum.WAV:
                    return "WAV audio file format";
                case FileExtensionTypeEnum.MP3:
                    return "MPEG-1 Audio Layer 3";
                case FileExtensionTypeEnum.CDA:
                    return "CD audio track file";
                case FileExtensionTypeEnum.OGG:
                    return "Ogg Vorbis audio file";
                case FileExtensionTypeEnum.SQL:
                    return "STructured Query Language";
                default:
                    return "Party and Bullshit";
            }
        }


        public static string GetTemplateID(string name)
        {
            return GetRegKeyFile(name);

        }

        [ValueConversion(typeof(bool), typeof(Visibility))]
        public sealed class BoolToVisibilityConverter : IValueConverter
        {
            public Visibility TrueValue { get; set; }
            public Visibility FalseValue { get; set; }

            public BoolToVisibilityConverter()
            {
                // set defaults
                TrueValue = Visibility.Visible;
                FalseValue = Visibility.Collapsed;
            }

            public object Convert(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
                if (!(value is bool))
                    return null;
                return (bool)value ? TrueValue : FalseValue;
            }

            public object ConvertBack(object value, Type targetType,
                object parameter, CultureInfo culture)
            {
                if (Equals(value, TrueValue))
                    return true;
                if (Equals(value, FalseValue))
                    return false;
                return null;
            }
        }
        public static float GetBrightness(this Color color)
        {
            float num = ((float)color.R) / 255f;
            float num2 = ((float)color.G) / 255f;
            float num3 = ((float)color.B) / 255f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
                num4 = num2;
            if (num3 > num4)
                num4 = num3;
            if (num2 < num5)
                num5 = num2;
            if (num3 < num5)
                num5 = num3;
            return ((num4 + num5) / 2f);
        }

        public static float GetHue(this Color color)
        {
            if ((color.R == color.G) && (color.G == color.B))
                return 0f;
            float num = ((float)color.R) / 255f;
            float num2 = ((float)color.G) / 255f;
            float num3 = ((float)color.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
                num4 = num2;
            if (num3 > num4)
                num4 = num3;
            if (num2 < num5)
                num5 = num2;
            if (num3 < num5)
                num5 = num3;
            float num6 = num4 - num5;
            if (num == num4)
                num7 = (num2 - num3) / num6;
            else if (num2 == num4)
                num7 = 2f + ((num3 - num) / num6);
            else if (num3 == num4)
                num7 = 4f + ((num - num2) / num6);
            num7 *= 60f;
            if (num7 < 0f)
                num7 += 360f;
            return num7;
        }

        public static float GetSaturation(this Color color)
        {
            float num = ((float)color.R) / 255f;
            float num2 = ((float)color.G) / 255f;
            float num3 = ((float)color.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
                num4 = num2;
            if (num3 > num4)
                num4 = num3;
            if (num2 < num5)
                num5 = num2;
            if (num3 < num5)
                num5 = num3;
            if (num4 == num5)
                return num7;
            float num6 = (num4 + num5) / 2f;
            if (num6 <= 0.5)
                return ((num4 - num5) / (num4 + num5));
            return ((num4 - num5) / ((2f - num4) - num5));
        }
        public static System.Windows.Media.LinearGradientBrush GetGrad(int flaya)
        {

            // Use the path to construct a brush.
            System.Windows.Media.LinearGradientBrush pthGrBrush = new System.Windows.Media.LinearGradientBrush(GetColor(1), GetColor(2), 2.2);

            // Set the color at the center of the path to blue.

            return pthGrBrush;
        }
        public static Color GetColor(int flaya)
        {
            flaya = 1;
            flaya *= flaya;
            flaya = (int)Math.Pow(flaya, flaya);

            Byte[] b = new Byte[3];
            rnd.NextBytes(b);
            Color color = Color.FromRgb(b[0], b[1], b[2]);
            while (color.B > (byte)199)
            {
                color = Color.Subtract(color, Color.FromRgb((byte)(0), (byte)(0), (byte)(50)));
            }
            while (color.G > (byte)199)
            {
                color = Color.Subtract(color, Color.FromRgb((byte)(0), (byte)(50), (byte)(0)));
            }
            float satur = color.GetSaturation();
            while (satur < 0.6 && satur > 0.3)
            {
                color = Color.Add(color, Color.FromRgb((byte)(flaya), (byte)(flaya), (byte)(flaya)));
                satur = color.GetSaturation();
            }

            float bright = color.GetBrightness();
            while (bright > 0.3f && bright < 0.6)
            {

                color = Color.Add(color, Color.FromRgb((byte)(flaya), (byte)(flaya), (byte)(flaya)));
                bright = color.GetBrightness();

            }


            while (bright > 0.9f)
            {
                color.R += (byte)(rnd.NextDouble() * 33);
                color.G += (byte)(rnd.NextDouble() * 33);
                color.B += (byte)(rnd.NextDouble() * 33);
                bright = color.GetBrightness();
            }
            while (bright < 0.1f)
            {

                color.R -= (byte)(rnd.NextDouble() * 33);
                color.G -= (byte)(rnd.NextDouble() * 33);
                color.B -= (byte)(rnd.NextDouble() * 33);
                bright = color.GetBrightness();
            }
            satur = color.GetSaturation();
            while (satur < 0.33)
            {
                color.B += (byte)(rnd.Next() % 44);
                color.G += (byte)(2 * rnd.Next() % 33);
                satur = color.GetSaturation();
            }
            bright = color.GetBrightness();
            while (color.R > (byte)245 || color.R < (byte)10 || color.G > (byte)245 || color.G < (byte)10 || color.B > (byte)245 || color.B < (byte)10 || bright > 0.95f || bright < 0.05f || satur > 0.95f || satur < 0.05f)
            {
                return IsExtra();
            }
            if ((byte)128 > color.A || color.A > (byte)255)
            {
                return IsExtra();
            }
            return color;
        }
        public static bool GetRandomBool(int name)
        {
            if (name == 1)
            {
                return rnd.NextDouble() > 0.5 ? true : false;
            }
            return rnd.NextDouble() > 0.81 ? true : false;
        }
        public static SolidColorBrush IsDark(Color color)
        {
            if (color.R > 127)
            {
                color.R -= (byte)127;
            }
            else color.R += (byte)127;
            if (color.G > 127)
            {
                color.G -= (byte)127;
            }
            else color.G += (byte)127;
            if (color.B > 127)
            {
                color.B -= (byte)127;
            }
            else color.B += (byte)127;
            /*  if (color.GetBrightness()>0.5f)
              {
                  color.R += x;
                  color.G += x;
                  color.B += x;

              }
              else 
              {
                  color.R -= x;
                  color.G -= x;
                  color.B -= x;
              }*/
            //color.Clamp();
            SolidColorBrush mySCB = new SolidColorBrush(color);
            //float bright = color.GetBrightness();

            //string c = bright < 0.4f ? "Snow": "DimGray";
            //System.Drawing.Color.r

            return mySCB;




        }

        public static Color IsExtra()
        {
            int sta = rnd.Next() % 4;

            switch (sta)
            {
                case 0: return Color.FromRgb(99, 70, 120);
                case 1: return Color.FromRgb(111, 77, 99);
                case 2: return Color.FromRgb(99, 99, 88);
                case 3: return Color.FromRgb(111, 111, 111);
                default:
                    return Color.FromRgb(111, 111, 111);
            }

        }
        public static FileExtensionMidTypeEnum GetFEMTEbyFETE(FileExtensionTypeEnum fete)
        {
            switch (fete)
            {
                case FileExtensionTypeEnum.EXTENSIONLESS:
                    return FileExtensionMidTypeEnum.Generic;
                case FileExtensionTypeEnum.XYZ:
                    return FileExtensionMidTypeEnum.Generic;
                case FileExtensionTypeEnum.TORRENT:
                    return FileExtensionMidTypeEnum.Internet;
                case FileExtensionTypeEnum.FILE:
                    return FileExtensionMidTypeEnum.Generic;
                case FileExtensionTypeEnum.SQL:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.GDC:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.GEN:
                    return FileExtensionMidTypeEnum.ROM;
                case FileExtensionTypeEnum.GALAXY:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.FUK:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.GB:
                    return FileExtensionMidTypeEnum.ROM;
                case FileExtensionTypeEnum.GAME:
                    return FileExtensionMidTypeEnum.Engine;
                case FileExtensionTypeEnum.GAM:
                    return FileExtensionMidTypeEnum.Save;
                case FileExtensionTypeEnum.CTY:
                    return FileExtensionMidTypeEnum.Save;
                case FileExtensionTypeEnum.GRP:
                    return FileExtensionMidTypeEnum.AppData;
                case FileExtensionTypeEnum.GXT:
                    return FileExtensionMidTypeEnum.AppData;
                case FileExtensionTypeEnum.H3M:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.H4R:
                    return FileExtensionMidTypeEnum.AppData;
                case FileExtensionTypeEnum.J2L:
                    return FileExtensionMidTypeEnum.AppData;
                case FileExtensionTypeEnum.LMU:
                    return FileExtensionMidTypeEnum.Engine;
                case FileExtensionTypeEnum.MAP:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.MM7:
                    return FileExtensionMidTypeEnum.Save;
                case FileExtensionTypeEnum.MP2S:
                    return FileExtensionMidTypeEnum.Save;
                case FileExtensionTypeEnum.NARC:
                    return FileExtensionMidTypeEnum.Console;
                case FileExtensionTypeEnum.NDS:
                    return FileExtensionMidTypeEnum.ROM;
                case FileExtensionTypeEnum.PSV:
                    return FileExtensionMidTypeEnum.Console;
                case FileExtensionTypeEnum.PSK:
                    return FileExtensionMidTypeEnum.Mesh;
                case FileExtensionTypeEnum.SC2MAPS:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.SC4:
                    return FileExtensionMidTypeEnum.Save;
                case FileExtensionTypeEnum.UNITY:
                    return FileExtensionMidTypeEnum.Engine;
                case FileExtensionTypeEnum.UT2:
                    return FileExtensionMidTypeEnum.Map;
                case FileExtensionTypeEnum.ZZZ:
                    return FileExtensionMidTypeEnum.AppData;
                case FileExtensionTypeEnum.GSC:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.EML:
                    return FileExtensionMidTypeEnum.Internet;
                case FileExtensionTypeEnum.PGN:
                    return FileExtensionMidTypeEnum.Game;
                case FileExtensionTypeEnum.VB:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.DART:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.PHP:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.PY:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.RB:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.JSX:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.JS:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.ES:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.TS:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.CSS:
                    return FileExtensionMidTypeEnum.Style;
                case FileExtensionTypeEnum.LESS:
                    return FileExtensionMidTypeEnum.Style;
                case FileExtensionTypeEnum.CS:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.CPP:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.XAML:
                    return FileExtensionMidTypeEnum.Markup;
                case FileExtensionTypeEnum.XML:
                    return FileExtensionMidTypeEnum.Markup;
                case FileExtensionTypeEnum.HTML:
                    return FileExtensionMidTypeEnum.Markup;
                case FileExtensionTypeEnum.PDF:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.DOC:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.DOCX:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.ODT:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.RTF:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.TXT:
                    return FileExtensionMidTypeEnum.RichText;
                case FileExtensionTypeEnum.PNG:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.JPEG:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.PDN:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.GIF:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.EPS:
                    return FileExtensionMidTypeEnum.Vector;
                case FileExtensionTypeEnum.SVG:
                    return FileExtensionMidTypeEnum.Vector;
                case FileExtensionTypeEnum.ASP:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.ASPX:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.CSHTML:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.LOG:
                    return FileExtensionMidTypeEnum.Log;
                case FileExtensionTypeEnum.BAT:
                    return FileExtensionMidTypeEnum.Executible;
                case FileExtensionTypeEnum.ISO:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.ZIP:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.RAR:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.APK:
                    return FileExtensionMidTypeEnum.Setup;
                case FileExtensionTypeEnum.EXE:
                    return FileExtensionMidTypeEnum.Executible;
                case FileExtensionTypeEnum.CFG:
                    return FileExtensionMidTypeEnum.Configuration;
                case FileExtensionTypeEnum.XLS:
                    return FileExtensionMidTypeEnum.Sheets;
                case FileExtensionTypeEnum.XLSX:
                    return FileExtensionMidTypeEnum.Sheets;
                case FileExtensionTypeEnum.ODS:
                    return FileExtensionMidTypeEnum.Sheets;
                case FileExtensionTypeEnum.BAK:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.H:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.JAVA:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.C:
                    return FileExtensionMidTypeEnum.Script;
                case FileExtensionTypeEnum.CLASS:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.SH:
                    return FileExtensionMidTypeEnum.Shell;
                case FileExtensionTypeEnum.ICO:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.DLL:
                    return FileExtensionMidTypeEnum.Library;
                case FileExtensionTypeEnum.DMP:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.MP4:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.MPEG:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.AVI:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.MKV:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.MOV:
                    return FileExtensionMidTypeEnum.Video;
                case FileExtensionTypeEnum.WMV:
                    return FileExtensionMidTypeEnum.Video;
                case FileExtensionTypeEnum.SYS:
                    return FileExtensionMidTypeEnum.System;
                case FileExtensionTypeEnum.MSI:
                    return FileExtensionMidTypeEnum.Setup;
                case FileExtensionTypeEnum.LNK:
                    return FileExtensionMidTypeEnum.System;
                case FileExtensionTypeEnum.INI:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.TMP:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.CAB:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.PPT:
                    return FileExtensionMidTypeEnum.Presentation;
                case FileExtensionTypeEnum.PPTX:
                    return FileExtensionMidTypeEnum.Presentation;
                case FileExtensionTypeEnum.ODP:
                    return FileExtensionMidTypeEnum.Presentation;
                case FileExtensionTypeEnum.XHTML:
                    return FileExtensionMidTypeEnum.Markup;
                case FileExtensionTypeEnum.CGI:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.PL:
                    return FileExtensionMidTypeEnum.OOPL;
                case FileExtensionTypeEnum.RSS:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.BMP:
                    return FileExtensionMidTypeEnum.Raster;
                case FileExtensionTypeEnum.OTF:
                    return FileExtensionMidTypeEnum.Font;
                case FileExtensionTypeEnum.TTF:
                    return FileExtensionMidTypeEnum.Font;
                case FileExtensionTypeEnum.FON:
                    return FileExtensionMidTypeEnum.Font;
                case FileExtensionTypeEnum.FNT:
                    return FileExtensionMidTypeEnum.Font;
                case FileExtensionTypeEnum.COM:
                    return FileExtensionMidTypeEnum.Shell;
                case FileExtensionTypeEnum.BIN:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.JAR:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.MDB:
                    return FileExtensionMidTypeEnum.Database;
                case FileExtensionTypeEnum.DAT:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.DB:
                    return FileExtensionMidTypeEnum.Database;
                case FileExtensionTypeEnum.DBF:
                    return FileExtensionMidTypeEnum.Database;
                case FileExtensionTypeEnum.CSV:
                    return FileExtensionMidTypeEnum.Sheets;
                case FileExtensionTypeEnum.SAV:
                    return FileExtensionMidTypeEnum.Data;
                case FileExtensionTypeEnum.TAR:
                    return FileExtensionMidTypeEnum.Archive;
                case FileExtensionTypeEnum.WMA:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.WAV:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.MP3:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.CDA:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.OGG:
                    return FileExtensionMidTypeEnum.Audio;
                case FileExtensionTypeEnum.MDF:
                    return FileExtensionMidTypeEnum.Data;
                default:
                    return FileExtensionMidTypeEnum.None;
            }
        }

        public static FyleTipe GetFTbyFETE(FileExtensionTypeEnum fete)
        {
            switch (fete)
            {

                case FileExtensionTypeEnum.EXTENSIONLESS:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.XYZ:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.TORRENT:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.FILE:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.MDF:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.GDC:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GEN:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GALAXY:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.FUK:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GB:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GAME:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GAM:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.CTY:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GRP:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GXT:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.H3M:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.H4R:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.J2L:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.LMU:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.MAP:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.MM7:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.MP2S:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.NARC:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.NDS:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.PSV:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.PSK:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.SC2MAPS:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.SC4:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.UNITY:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.UT2:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.ZZZ:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.GSC:
                    return FyleTipe.Games;
                case FileExtensionTypeEnum.EML:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.PGN:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.VB:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.DART:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.PHP:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.PY:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.RB:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.JSX:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.JS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.ES:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.TS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CSS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.LESS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CPP:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.XAML:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.XML:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.HTML:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.PDF:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.DOC:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.DOCX:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.ODT:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.RTF:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.TXT:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.PNG:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.JPEG:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.PDN:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.GIF:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.EPS:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.SVG:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.ASP:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.ASPX:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CSHTML:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.LOG:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.BAT:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.ISO:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.ZIP:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.RAR:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.APK:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.EXE:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.CFG:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.XLS:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.XLSX:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.ODS:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.BAK:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.H:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.JAVA:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.C:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CLASS:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.SH:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.ICO:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.DLL:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.DMP:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.MP4:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.MPEG:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.AVI:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.MKV:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.MOV:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.WMV:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.SYS:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.MSI:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.LNK:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.INI:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.TMP:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.CAB:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.PPT:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.PPTX:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.ODP:
                    return FyleTipe.Document;
                case FileExtensionTypeEnum.XHTML:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.CGI:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.PL:
                    return FyleTipe.Code;
                case FileExtensionTypeEnum.RSS:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.BMP:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.OTF:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.TTF:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.FON:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.FNT:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.COM:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.BIN:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.JAR:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.MDB:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.DAT:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.DB:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.DBF:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.CSV:
                    return FyleTipe.Data;
                case FileExtensionTypeEnum.SAV:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.TAR:
                    return FyleTipe.Misc;
                case FileExtensionTypeEnum.WMA:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.WAV:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.MP3:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.CDA:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.OGG:
                    return FyleTipe.Media;
                case FileExtensionTypeEnum.SQL:
                    return FyleTipe.Code;

                default:
                    return FyleTipe.No_File_Type;
            }
        }
        public static LinkCollection Top1 { get; set; }
        public static LinkCollection Top2 { get; set; }
        public static BlankDialog BD { get; set; }
        public static List<City> WorldCities { get; set; }

        /*   public static void SetTop(bool v)
           {
               if (v)
               {
                   (Application.Current.MainWindow as ModernWindow).TitleLinks = Top1;
               }
               else
               {
                   (Application.Current.MainWindow as ModernWindow).TitleLinks = Top2;
               }
           }*/

        internal static void InitTemplates(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<ExTemplate> tmpl = new List<ExTemplate>();



                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = DBConnection.Connection;
                    command.CommandText = "SELECT boilpack.ExProID, boilpack.ID, boilpack.DateTime, boilpack.Username, boilpack.CountryID, boilpack.DonateUrl, Count(ratings.ID) FROM boilpack LEFT JOIN ratings ON boilpack.ID= ratings.ForID GROUP BY ID ORDER BY Count(ratings.ID) DESC"; ;

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ExTemplate et = new ExTemplate();
                        et.ID = reader.GetInt32(0);
                        et.Username = reader.GetString(3);
                        et.Rating = reader.GetInt64(6);
                        tmpl.Add(et);
                    }

                    Tmpl = tmpl.OrderByDescending(x => x.Rating).ToList();


                }

                DBConnection.Close();
            }
            catch (Exception bazatemplajt)
            {
                  //ModernDialog.ShowMessage(bazatemplajt.Message, "bazatemplajt", MessageBoxButton.OK);
            }

        }
    }
}

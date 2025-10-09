using CountryFlag;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Windows.Threading;
using ToastNotifications.Messages;

namespace pq.Pages
{
    public class IconComboItem
    {
        public Canvas c { get; set; }
        public string n { get; set; }
    }
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl, IContent, INotifyPropertyChanged
    {

        private bool issynched;



        public Profile()
        {
            InitializeComponent();
            DataContext = this;
            DirSize.Maximum = 5;
            long dirsize = GetDirectorySize();
            DirSize.Value = (double)dirsize / 10000000;
            DirSize.SelectionStart = 0;
            DirSize.SelectionEnd = DirSize.Value;
            DirSizer.Text = DirSize.Value.ToString("0.00") + " MB";
            Tajmer();
            PackColor = Application.Current.TryFindResource("Accent") as SolidColorBrush;
            tral();
          //  guid.Password = Helper.Helper.GetGuid();
            user.Text = Helper.Helper.Username;
            mydom.Text = Environment.UserDomainName;
            IsSynched = Helper.Helper.IsSynched();
            DataContext = this;
            if (IsSynched)
            {
                ExPro ep = Helper.Helper.Synched();
                if (ep != null)
                {

               //     exguid.Password = ep.ExID;
                    exuser.Text = ep.ExUsername;
                    try
                    {
                       // dob.Text = ((DateTime)ep.Dob).ToShortDateString();
                    }
                    catch (Exception)
                    {

                    }

                    if (!string.IsNullOrEmpty(ep.Country))
                    {
                        flag.Code = (CountryCode)Enum.Parse(typeof(CountryCode), ep.Country);
                    }
                  //  email.Text = ep.Email;
                    donate.Text = ep.DonateUrl;
                    password.Password = ep.Password;
                    ModernDialog.ShowMessage(ep.Id.ToString(), "fafala", MessageBoxButton.OK);
                }


                else
                {
                    ModernDialog.ShowMessage("fuck up", "fu", MessageBoxButton.OK);
                }
            }
            else
            {
                //    ModernDialog.ShowMessage("klončo", "čembrio", MessageBoxButton.OK);

            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                Clipboard.SetText(password.Password);
                ModernDialog.ShowMessage(password.Password + " Copied to the clipboard", "Copy!", MessageBoxButton.OK);
            }
            catch (Exception)
            {

                ModernDialog.ShowMessage("Easy does it!", "Copy!", MessageBoxButton.OK);
            }

        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            ExDialog ed = new ExDialog();
            if (ed.ShowDialog() == true)
            {
                if (ed.DialogResult == true)
                {

                    IsSynched = true;

                    exuser.Text=string.IsNullOrEmpty(ed.ep.ExUsername) ?"• • •" : ed.ep.ExUsername;
                    donate.Text=string.IsNullOrEmpty(ed.ep.DonateUrl) ?"• • •" : ed.ep.DonateUrl;
                    password.Password=string.IsNullOrEmpty(ed.ep.Password) ?"•••" : ed.ep.Password;
                    if (string.IsNullOrEmpty(ed.ep.Country)) 
                        flag.Visibility = Visibility.Hidden;
                    else { 
                        flag.Code = (CountryCode)Enum.Parse(typeof(CountryCode), ed.ep.Country);
                        flag.Visibility= Visibility.Visible;
                    }
                  
                //    exguid.Password = ed.ep.ExID;
                   // email.Text = ed.ep.Email;
                    try
                    {
                       // dob.Text = ((DateTime)ed.ep.Dob).ToShortDateString();
                    }
                    catch (Exception)
                    {

                    }

                  //  donate.Text = ed.ep.DonateUrl;
                  //  if (!string.IsNullOrEmpty(ed.ep.Country))
                  //  {
                   //     flag.Code = (CountryCode)Enum.Parse(typeof(CountryCode), ed.ep.Country);
                  //      flag.ToolTip = ((CountryCode)Enum.Parse(typeof(CountryCode), ed.ep.Country)).ToString();
                  //  }
                    IsSynched = string.IsNullOrEmpty(ed.ep.ExID) ? false : true;
                }
                else
                {

                }
            }

        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }


        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FlagPicker fp = new FlagPicker();
            if (fp.ShowDialog() == true)
            {
                flag.Code = fp.CTRY;

            }
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
            //    Clipboard.SetText(exguid.Password);
            //    ModernDialog.ShowMessage(exguid.Password + " Copied to the clipboard", "Copy!", MessageBoxButton.OK);
            }
            catch (Exception)
            {

                ModernDialog.ShowMessage("Easy does it!", "Copy!", MessageBoxButton.OK);
            }
        }

        private void PasswordBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
             //   Clipboard.SetText(exguid.Password);
           //     ModernDialog.ShowMessage(exguid.Password + " Copied to the clipboard", "Copy!", MessageBoxButton.OK);
            }
            catch (Exception)
            {

                ModernDialog.ShowMessage("Easy does it!", "Copy!", MessageBoxButton.OK);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(password.Password);
                ModernDialog.ShowMessage("Password copied to the clipboard", "Copy!", MessageBoxButton.OK);
            }
            catch (Exception)
            {

                ModernDialog.ShowMessage("Easy does it!", "Copy!", MessageBoxButton.OK);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBlock_MouseLeftButtonDown(null, null);
        }



        private CountryFlag.CountryCode cc;
        public CountryFlag.CountryCode CC
        {
            get { return cc; }
            set
            {
                if (value != cc)
                {
                    cc = value;
                    OnPropertyChanged("CC");
                }
            }
        }
        private SolidColorBrush packcolor;
        public SolidColorBrush PackColor
        {
            get { return packcolor; }
            set
            {
                if (value != packcolor)
                {
                    packcolor = value;
                    OnPropertyChanged("PackColor");
                }
            }
        }
        private SolidColorBrush bordercolor;
        public SolidColorBrush BorderColor
        {
            get { return bordercolor; }
            set
            {
                if (value != bordercolor)
                {
                    bordercolor = value;
                    OnPropertyChanged("BorderColor");
                }
            }
        }
        private SolidColorBrush bordercolor2;
        public SolidColorBrush BorderColor2
        {
            get { return bordercolor2; }
            set
            {
                if (value != bordercolor2)
                {
                    bordercolor2 = value;
                    OnPropertyChanged("BorderColor2");
                }
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsSynched
        {
            get { return issynched; }
            set
            {
                issynched = value;
                OnPropertyChanged("IsSynched");
            }
        }
        static long GetDirectorySize()
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles("c:\\def\\", "*.*");

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                if (info.Exists)
                {
                    b += info.Length;
                }

            }
            // 4.
            // Return total size
            return b;
        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }



        private void Tajmer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (x, y) =>
            {
                long dirsize2 = GetDirectorySize();
                DirSize.Value = (double)dirsize2 / 10000000;
                DirSize.SelectionStart = 0;
                DirSize.SelectionEnd = DirSize.Value;
                DirSizer.Text = DirSize.Value.ToString("0.00") + " MB";
                Tajmer();
            };
            timer.Start();
        }

        private void Button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            (Application.Current.MainWindow as MainWindow).notifier.ShowInformation("SHARE started");
            BackgroundWorker worker2 = new BackgroundWorker();

            worker2.DoWork += DoShare;

            worker2.RunWorkerCompleted += Helper.Helper.DBWorker_RunWorkerCompleted;
            worker2.RunWorkerAsync();

            BlankDialog bd = new BlankDialog();
            bd.Owner = Application.Current.MainWindow;
            Helper.Helper.BD = bd;

            if (bd.ShowDialog() == true)
            {
                MessageBox.Show("Bravo2");
            }






        }
        public static void DoShare(object sender, DoWorkEventArgs e)
        {
            string startPath = "C:\\abc";
            string zipPath = "C:\\def\\def.zip";
            FileInfo finfo = new FileInfo(zipPath);
            if (finfo.Exists)
            {
                finfo.MoveTo(finfo.Directory + "\\" + "def_" + Guid.NewGuid() + ".zip");
            }
            ZipFile.CreateFromDirectory(startPath, zipPath);

            //FileStream stream = 
            byte[] fileBytes = File.ReadAllBytes(zipPath);


            using (MySqlCommand command = new MySqlCommand())
            {
                command.Connection = DBConnection.Connection;
                command.CommandText = "UPDATE boilpack SET BoilerplateZip=?rawData, ZipSize=?fileSize WHERE ExProID=?exproid;";
                MySqlParameter exproid = new MySqlParameter("?exproid", MySqlDbType.VarChar, 256);
                MySqlParameter fileSize = new MySqlParameter("?fileSize", MySqlDbType.Int32, 11);
                MySqlParameter rawData = new MySqlParameter("?rawData", MySqlDbType.Blob, fileBytes.Length);

                exproid.Value = Helper.Helper.GetGuid();
                fileSize.Value = fileBytes.Length;
                rawData.Value = fileBytes;

                command.Parameters.Add(exproid);
                command.Parameters.Add(fileSize);
                command.Parameters.Add(rawData);
                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bravo - 4");
                }
                else
                {
                    MessageBox.Show("Fail - 5");
                }
                DBConnection.Close();


            }
            /* string query = "insert into BoilPack(ExProID,Zip) values('" + Helper.Helper.Synched().ExID + "','" + fileBytes + "');";
             var cmd = new MySqlCommand(query, dbCon.Connection);
             int reader = cmd.ExecuteNonQuery();*/

        }



        public void OnNavigatedTo(NavigationEventArgs e)
        {
            //  Helper.Helper.SetTop(false);
            IsSynched = Helper.Helper.IsSynched();
            Synch.IsEnabled = Synch.IsEnabled;
            ExPro ep = Helper.Helper.Synched();
            string exproexusername = ep == null ? "" : ep.ExUsername.ToUpper();
            Username = string.IsNullOrEmpty(exproexusername) ? Helper.Helper.Username.ToUpper() : exproexusername;
            CountryFlag.CountryCode mycc;
            if (ep != null)
            {

                Enum.TryParse(ep.Country, out mycc);
                CC = mycc;
            }

        }
        private void tral()
        {

            List<IconComboItem> IconList = new List<IconComboItem>();
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new System.Uri("/Resources/IconsNonShared.xaml", System.UriKind.RelativeOrAbsolute);
            // Random rnd = new Random();
            var iconenumerator = rd.GetEnumerator();
            for (int i = 0; i < 1262; i++)
            {

                iconenumerator.MoveNext();
                DictionaryEntry de = (DictionaryEntry)iconenumerator.Current;

                IconComboItem ici = new IconComboItem();
                ici.c = de.Value as Canvas;
                ici.n = de.Key.ToString();
                IconList.Add(ici);
            }
           // zzz.ItemsSource = IconList;
        }

        private void OpenF_Click(object sender, RoutedEventArgs e)
        {
            //    var dbCon = DBConnection.Instance();
            // if (DBConnection.IsConnect())
            {


                MySqlDataReader myData;

                byte[] rawData;
                UInt32 FileSize;

                string SQL = "select BoilerplateZip,ZipSize from boilpack where ExProID='" + Helper.Helper.GetGuid() + "'";
                var cmd = new MySqlCommand();

                try
                {
                    cmd.Connection = DBConnection.Connection;
                    cmd.CommandText = SQL;

                    myData = cmd.ExecuteReader();

                    if (!myData.HasRows)
                        throw new Exception("There are no blobs to save");

                    myData.Read();

                    FileSize = myData.GetUInt32(myData.GetOrdinal("ZipSize"));
                    rawData = new byte[FileSize];

                    myData.GetBytes(myData.GetOrdinal("BoilerplateZip"), 0, rawData, 0, (Int32)FileSize);

                    using (Stream file = File.OpenWrite(@"c:\abc\here.zip"))
                    {
                        if (rawData != null)
                        {
                            file.Write(rawData, 0, rawData.Length);
                        }

                    }

                    myData.Close();
                    myData.Dispose();

                    cmd.Dispose();

                    DBConnection.Close();


                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }







            ////////////////////////
            /* byte[] newbytes = null;
             var dbCon = DBConnection.Instance();
             if (dbCon.IsConnect())
             {
                 string query = "select Zip from BoilPack where ExProID='" + Helper.Helper.Synched().ExID + "'";
                 var cmd = new MySqlCommand(query, dbCon.Connection);


                 var reader = cmd.ExecuteReader();

                 while (reader.Read())
                 {
                     reader.GetBytes(0, 0, newbytes, 0, reader.FieldCount);
                 }
                 dbCon.Close();
             }*/

        }


        private void StackPanel_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }


        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void StackPanel_MouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ColorPicker cp = new ColorPicker();
            if (cp.ShowDialog() == true)
            {
                BorderColor = new SolidColorBrush(cp.SelectedAccentColor);
            }
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ColorPicker cp = new ColorPicker();
            if (cp.ShowDialog() == true)
            {
                BorderColor2 = new SolidColorBrush(cp.SelectedAccentColor);
            }
        }

    }
}




/// <summary>
/// Interaction logic for TemplatePack.xaml
/// </summary>

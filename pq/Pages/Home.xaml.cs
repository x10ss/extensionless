using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using pq.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl, IContent
    {
        string pu;
        void IContent.OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            //   throw new NotImplementedException();
        }

        void IContent.OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //   throw new NotImplementedException();
        }

        void IContent.OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

            Helper.Helper.svm = null;
            // AppearanceManager.Current.AccentColor = Helper.Helper.GetAccent(4); 
            //  Helper.Helper.CurentScreen = this as FrameworkElement;
            Helper.Helper.getDBST();
            IsExtended.IsChecked = Helper.Helper.ST.IsExtended;
            IsMine.IsChecked = Helper.Helper.ST.IsMine;
            //  Helper.Helper.SetTop(false);
            //  throw new NotImplementedException();
            //AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }

        public bool MyIsLoaded { get; set; }
        /* public async Task<bool> MyMyIsLoaded()
         {
             return await Dispatcher.InvokeAsync(() => { return MyIsLoaded; });
         }*/
        OpenDialog newdialoge;
        public Home()
        {
            InitializeComponent();
            MyInit();
            Helper.Helper.MyHome = this;

            //  ModernDialog.ShowMessage(Helper.Helper.GetGuid(), "home", MessageBoxButton.OK);
            // ModernDialog.ShowMessage(Helper.Helper.GetExPro(), "home", MessageBoxButton.OK);



        }       /*   private async void LoadContentLoader()
               {
                   var b = await ((Application.Current.MainWindow as ModernWindow).ContentLoader as MyCL).LoadExAsync(this as Home);
               }*/

        public void MyInit()
        {
            Helper.Helper.All = new System.Collections.Generic.List<FileExtensionItem>();

            //  Helper.Helper.getDB();

            //   Hi.Text = Helper.Helper.Username + ", start templating extensions";
            Load();
        }
        public void Load()
        {
            MyIsLoaded = false;
            newdialoge = new OpenDialog();
            // FETdetails UserControl1Control = sender as FETdetails;

            newdialoge.Owner = Application.Current.MainWindow;
            bool bl;
            try
            {
                bl = (bool)newdialoge.ShowDialog();
            }
            catch (Exception exc)
            {
                bl = false;
                ModernDialog.ShowMessage("suckadick", "pc", MessageBoxButton.OK);
            }

            if (bl)
            {
                pu = "Machine";
                Helper.Helper.RegBase = Registry.ClassesRoot;
                //  ModernDialog.ShowMessage("pc", "pc", MessageBoxButton.OK);
            }
            else
            {
                Helper.Helper.RegBase = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Classes", true);
                pu = "User";
                //  ModernDialog.ShowMessage("user", "user", MessageBoxButton.OK);
            }

            if (!MyIsLoaded) LoadFete();
        }


        private void MyHome2_Loaded_1(object sender, RoutedEventArgs e)
        {

            //  LoadContentLoader();
            /*  if (((Application.Current.MainWindow as ModernWindow).ContentLoader as MyCL).myLoad(this))
            {
                ModernDialog.ShowMessage("saka cast", "", MessageBoxButton.OK);
              }
              else
              {
                  ModernDialog.ShowMessage("sdsdsdsd", "", MessageBoxButton.OK);
              }*/

            //   version.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
        }


        private void LoadFete()
        {
            (Application.Current.MainWindow as MainWindow).notifier.ShowInformation("DB...");
            BackgroundWorker worker2 = new BackgroundWorker();

            worker2.DoWork += Helper.Helper.getDB;

            worker2.RunWorkerCompleted += Helper.Helper.DBWorker_RunWorkerCompleted;
            worker2.RunWorkerAsync();

            BlankDialog bd = new BlankDialog();
            bd.Owner = Application.Current.MainWindow;
            Helper.Helper.BD = bd;

            if (bd.ShowDialog() == true)
            {


                (Application.Current.MainWindow as MainWindow).notifier.ShowInformation("Registry load started");
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Helper.Helper.getall;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerCompleted += AllWorker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            else
            {
                (Application.Current.MainWindow as MainWindow).notifier.ShowError("----D---B----");
            }

        }

        private void AllWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                QuickStart.Visibility = Visibility.Visible;
                IsExtended.IsChecked = Helper.Helper.ST.IsExtended;
                IsMine.IsChecked = Helper.Helper.ST.IsMine;

                (Application.Current.MainWindow as MainWindow).notifier.ShowInformation("Registry Loaded");

                MyIsLoaded = true;
                Slajda5.Text = " Extensions loaded • " + pu.ToUpper() + " Registry";
                Slajda5a.Text = "✔";
            }
            catch (Exception eeee)
            {
                MessageBox.Show(eeee.Message);

                MyIsLoaded = true;
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            try
            {
                Slajda.Value = e.ProgressPercentage;
            }
            catch (Exception bzvz)
            {

            }
            FileExtensionItem fei = e.UserState as FileExtensionItem;
            Desc.Text += " " + fei.Name + (Slajda.Value == Slajda.Maximum ? "" : " |");
            Slajda5.Text = " Loading → " + fei.Name + " | " + fei.FullName;
            int slajda = String.IsNullOrEmpty(fei.TemplateID) ? 2 : fei.TemplateID == "0" ? 3 : 4;
            switch (slajda)
            {
                case 2: Slajda2.Value++; break;
                case 3:
                    Slajda3.Value++;
                    (Application.Current.MainWindow as MainWindow).notifier.ShowWarning(fei.Name + " | Empty template");
                    break;
                case 4:
                    Slajda4.Value++;
                    (Application.Current.MainWindow as MainWindow).notifier.ShowSuccess(fei.Name + " | " + fei.TemplateID + " ♥ TEMPLATED");
                    break;
                default:
                    break;
            }
            /*      if (Slajda.Value==1)
              {
                    BBCodeBlock bs = new BBCodeBlock();

                    try
                    {
                        bs.LinkNavigator.Navigate(new Uri("/Pages/ModernDialog1.xaml", UriKind.Relative), this);
                    }
                    catch (Exception error)
                    {
                        ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
                    }
                }*/
            if (Slajda.Value == Slajda.Maximum)
            {
                // Desc.Visibility = Visibility.Hidden;
                //  Ring.IsActive = false;
                //  Ring.Visibility = Visibility.Collapsed;
                //   Konzola.Visibility = Visibility.Visible;

                // string url = "/Pages/Split7.xaml";
                //  (Application.Current.MainWindow as ModernWindow).LinkNavigator.Navigate(new Uri(url, UriKind.RelativeOrAbsolute), Helper.Helper.CurentScreen);

                /*   BBCodeBlock bs = new BBCodeBlock();

                   try
                   {
                       bs.LinkNavigator.Navigate(new Uri("/Pages/Split.xaml", UriKind.Relative), this);
                   }
                   catch (Exception error)
                   {
                       ModernDialog.ShowMessage(error.Message, FirstFloor.ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
                   }*/
            }
        }

        public void ReReg(object sender, RoutedEventArgs e)
        {

            Load();
            Slajda.Value = 0;
            Slajda2.Value = 0;
            Slajda3.Value = 0;
            Slajda4.Value = 0;
            Slajda5a.Text = "▲▼";

            MyIsLoaded = false;
            //    Konzola.Visibility = Visibility.Collapsed;
            //  Ring.Visibility = Visibility.Visible;
            //   Ring.IsActive = true;
            QuickStart.Visibility = Visibility.Hidden;
            Desc.Text = "";
        }

        private void ReLog(object sender, RoutedEventArgs e)
        {

            Process myProcess = new Process();
            string result = Path.GetTempPath();
            string fileName = result + "Extensionless-Log.txt";
            myProcess.StartInfo.FileName = "Notepad.exe"; //not the full application path
            myProcess.StartInfo.Arguments = fileName;
            myProcess.Start();

        }

        private void MyHome2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.F1)
            {
                var url = "/Pages/SettingsPage.xaml";

                (Application.Current.MainWindow as ModernWindow).LinkNavigator.Navigate(new Uri(url, UriKind.Relative), sender as FrameworkElement);

            }
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ms-settings:defaultapps");
        }

        private void ContentPresenter_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ModernDialog.ShowMessage("Don't touch", "dangerous", MessageBoxButton.OK);
        }

        private void ContentPresenter_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ModernDialog.ShowMessage("Don't touch", "dangerous", MessageBoxButton.OK);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ReReg(null, null);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }



        private void IsExtended_Checked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {

                Setting st = ent.Setting.FirstOrDefault();
                st.IsExtended = true;
                ent.SaveChanges();
            }
        }

        private void IsExtended_Unchecked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {
                Setting st = ent.Setting.FirstOrDefault();
                st.IsExtended = false;
                ent.SaveChanges();
            }
        }

        private void IsMine_Checked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {

                Setting st = ent.Setting.FirstOrDefault();
                st.IsMine = true;
                ent.SaveChanges();
            }
        }

        private void IsMine_Unchecked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {

                Setting st = ent.Setting.FirstOrDefault();
                st.IsMine = false;
                ent.SaveChanges();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameworkElement fel = (Parent as FrameworkElement);
            FrameworkElement fel2 = (fel.Parent as FrameworkElement);
            FrameworkElement fel3 = (fel2.Parent as FrameworkElement);
            FrameworkElement fel4 = (fel3.Parent as FrameworkElement);
            FrameworkElement fel5 = (fel4.Parent as FrameworkElement);

            //   (fel.Parent as FrameworkElement).Visibility = Visibility.Visible;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameworkElement fel = (Parent as FrameworkElement);

            //    (fel.Parent as FrameworkElement).Visibility = Visibility.Collapsed;

        }
    }
}

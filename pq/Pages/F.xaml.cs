using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using pq.Helper;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

//
namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Frame.xaml
    /// </summary>
    public partial class F : UserControl, IContent
    {
        public int FFS { get; set; }
        public bool myIsL = false;
        public NonSH nosh { get; set; }
        public F(int i)
        {
            FFS = i - 1;
            InitializeComponent();
            nosh = new NonSH();
        }

        private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModernDialog.ShowMessage("", "", MessageBoxButton.OK);
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine((Application.Current.MainWindow as ModernWindow).ContentSource.ToString());
            //   ((((this.Parent as FrameworkElement).Parent as FrameworkElement).Parent as Grid).Children[0] as ListBox).SelectionChanged += LoadFlaya;
            // (((this.Parent as FrameworkElement).Parent as Grid).Children[0] as ListBox).SelectionChanged += LoadFlaya2;
        }
        /* private void LoadFlaya(object sender, SelectionChangedEventArgs e)
         {

             myIsL = false;
             Loading.IsActive = true;
             Grid g = ((Parent as FrameworkElement).Parent as FrameworkElement).Parent as Grid;
             int ind = ((((g).Children[0] as ListBox).SelectedIndex));
             FileInfo finfo = Helper.Helper.Tmpl[FFS][ind];
             Flaya.Text = finfo.Name.ToUpper();
             Flaya2.Text = finfo.Directory.FullName.ToUpper();
             Flaya3.Text = finfo.LastWriteTime.ToShortDateString() + " | " + finfo.LastWriteTime.ToShortTimeString().ToUpper();
             Flaya4.Text = ((decimal)finfo.Length / 1000000000).ToString() + " GB";
             Flaya5.Text = finfo.Extension.ToUpper();
             Flaya6.Text = "";
             Flaya7.Text = "0";
             Flaya8.Text = "0";
             Flaya9.Text = "0";
             nosh.sr = finfo.OpenText();
             Flaya6.Text = "Image";

             if (finfo.Extension == ".png" || finfo.Extension == ".jpeg"
                 || finfo.Extension == ".tiff"
                 || finfo.Extension == ".bmp" || finfo.Extension == ".tif"
                 || finfo.Extension == ".gif" || finfo.Extension == ".jpg")
             {
                 image1.Source = new BitmapImage(new Uri(finfo.DirectoryName + "/" + finfo.Name));
                 return;
             }
             else if (finfo.Extension == ".pgn")
             {
                 string s = finfo.DirectoryName + "/" + finfo.Name;
                 //finfo.Delete();
                 //READ FILE
                 var reader = new PgnReader();
                 try
                 {
                     var gameDb = reader.ReadFromFile(s);
                     Game game = gameDb.Games[0];

                     Flaya6.Text = game.ToString();
                 }
                 catch (Exception ex)
                 {
                     ModernDialog.ShowMessage(ex.Message, "error", MessageBoxButton.OK);
                 }
                 //    var gameDb = reader.ReadFromFile(s);


             }
             else
             {
                 if (!myIsL)
                 {


                     initWork();


                 }

             }


         }

     */
        private void LoadFlaya2(object sender, SelectionChangedEventArgs e)
        {

            if (!myIsL)
            {


                initWork();


            }




        }
        public void initWork()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += nosh.GetF;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //            Application.Current.Dispatcher.Invoke(new Action(() => { (sender as BackgroundWorker).ReportProgress(i, s); }));

            myIsL = true;
            //  Loading.BorderBrush = new SolidColorBrush(Colors.Red);
        }
        private int rows2load = 25;
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ModernDialog.ShowMessage(e.UserState.ToString().Length.ToString(), e.UserState.ToString().Length.ToString(), MessageBoxButton.YesNo);


            string line = e.UserState.ToString();
            int lineLenght = line.Length;
            // Flaya6.Text += line;
            if (e.ProgressPercentage < rows2load)
            {
                Flaya6.Text += line + Environment.NewLine;
            }
            else if (e.ProgressPercentage == rows2load)
            {
                Flaya6.Text += Environment.NewLine + "◄◄••••••••►►" + Environment.NewLine;
            }

            Flaya7.Text = (Int32.Parse(Flaya7.Text) + lineLenght).ToString();
            Flaya8.Text = (Int32.Parse(Flaya8.Text) + 1).ToString();
            Flaya9.Text = e.ProgressPercentage.ToString();

        }

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
            //  throw new NotImplementedException();
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
    }
}

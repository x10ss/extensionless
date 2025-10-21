using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using pq.Model;
using pq.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace pq
{
    /// <summary>
    /// Interaction logic for FETdetails.xaml
    /// </summary>
    public partial class FETdetails : UserControl, IContent
    {
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

            // AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
        public FETdetails()
        {
            InitializeComponent();
            //   MessageBox.Show(Flaya.ToString());

        }

        public static readonly DependencyProperty
        FlayaProperty = DependencyProperty.Register("Flaya", typeof(SplitViewModel),
        typeof(FETdetails), new PropertyMetadata(new SplitViewModel(0),
         new PropertyChangedCallback(OnSetTextChanged)));
        public SplitViewModel Flaya
        {

            get { return (SplitViewModel)GetValue(FlayaProperty); }
            set { SetValue(FlayaProperty, value); }
        }
        private static void OnSetTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SplitViewModel svm = Application.Current.MainWindow.DataContext as SplitViewModel;
            FETdetails UserControl1Control = d as FETdetails;
            UserControl1Control.OnSetTextChanged(e);
            //  MessageBox.Show(UserControl1Control.Flaya.ToString());

        }
        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            //   tbTest.Text = e.NewValue.ToString();
        }
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;

            svm.NewCommand_Executed(sender, e);

            //  MessageBox.Show("The New command was invoked");
        }
        private void NewCommand_CanExecute2(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed2(object sender, ExecutedRoutedEventArgs e)
        {
            ModernDialog md = new ModernDialog();
            md.Owner = Application.Current.MainWindow;
            Button b1 = new Button();
            Button b2 = new Button();
            b1.Content = "OK";
            b1.Click += (x, y) => { md.DialogResult = true; };
            b2.Content = "Cancel";
            b2.Click += (x, y) => { md.DialogResult = false; };
            Button[] ba = { b1, b2 };
            md.Buttons = ba;
            ContentPresenter cp = new ContentPresenter();
            Canvas c = Application.Current.TryFindResource("appbar_layer_arrange_bringforward") as Canvas;
            cp.Content = c;
            md.Content = cp;
            bool bl = (bool)md.ShowDialog();
            if (bl)
            {

                SplitViewModel svm = DataContext as SplitViewModel;

                svm.NewCommand_Executed2(sender, e);

            }

        }

        public void Gridlock(object sender, SizeChangedEventArgs e)
        {
            int c = (int)e.NewSize.Width / 60;
            if (c == 0)
            {
                c = 1;
            }

            if (Flaya != null)
            {
                Flaya.Cols = c;
            }
            else
            {
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;

            OpenDialog newdialoge = new OpenDialog();

            // FETdetails UserControl1Control = sender as FETdetails;
            svm.ItemTemplateChanged(sender, e);
            newdialoge.Owner = Application.Current.MainWindow;
            newdialoge.ShowDialog();



        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;
            FrameworkElement rb = sender as FrameworkElement;
            if (rb == null)
            {

            }
            else if (rb.Name == "On" && svm.SelectedFE.TemplateID == "")
            {
                string s = (DataContext as SplitViewModel).SelectedFE.Name.Replace(".", "");
                //s = "."+s.ToLower();
                // ModernDialog.ShowMessage(s, "add", MessageBoxButton.OK);
                Helper.Helper.SetRegKeyBool(s);
                // Full.IsChecked = true;

                svm.SelectedFE.TemplateID = "0";
                svm.Notify();
            }
            else if (rb.Name == "Off")
            {
                string s = (DataContext as SplitViewModel).SelectedFE.Name.Replace(".", "");
                //s = "."+s.ToLower();
                //  ModernDialog.ShowMessage(s, "del", MessageBoxButton.OK);
                Helper.Helper.SetRegKeyInvBool(s);
                // Full.IsChecked = true;

                // Full.IsChecked = false;
                svm.SelectedFE.TemplateID = "";
                svm.Notify();
            }
            else
            {
                svm.Notify();
            }
        }

        private void Full_Checked(object sender, RoutedEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;

            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                svm.SelectedFE.TemplateID = "0";
                svm.Notify();
            }
            else
            {
                svm.SelectedFE.TemplateID = "";
                svm.Notify();
            }
        }

        private void On_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
            RadioButton_Checked(sender, e);
        }

        private void On_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var radioButton = sender as RadioButton;
            MessageBoxResult result = MessageBox.Show("Choosing this sample will override any changes you've made. Continue?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                radioButton.IsChecked = true;
            }
        }
        private void RadioButton_Checked(object sender, MouseButtonEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;
            FrameworkElement rb = sender as FrameworkElement;
            RadioButton rbb = sender as RadioButton;

            ModernDialog md = new ModernDialog();
            md.Owner = Application.Current.MainWindow;
            Button b1 = new Button();
            Button b2 = new Button();
            b1.Content = "Ok";
            b1.Click += (x, y) => { md.DialogResult = true; };
            b2.Content = "Cancel";
            b2.Click += (x, y) => { md.DialogResult = false; };
            Button[] ba = { b1, b2 };
            md.Buttons = ba;
            ContentPresenter cp = new ContentPresenter();


            if (rb == null)
            {

            }
            else if (rb.Name == "On" && svm.SelectedFE.TemplateID == "")
            {

                Canvas c = Application.Current.TryFindResource("appbar_layer_arrange_solid_bringforward") as Canvas;
                cp.Content = c;
                md.Content = cp;
                bool bl = (bool)md.ShowDialog();
                if (bl)
                {
                    string s = (DataContext as SplitViewModel).SelectedFE.Name.Replace(".", "");
                    //s = "."+s.ToLower();
                    // ModernDialog.ShowMessage(s, "add", MessageBoxButton.OK);
                    if (Helper.Helper.SetRegKeyBool(s))
                    {
                        FileExtensionItem fei = svm.SelectedFE;
                        svm.FEIs = svm.FEIs;
                        fei.TemplateID = "0";
                        svm.SelectedFE = fei;
                        // rbb.IsChecked = true;
                        fei.IsEnabled = true;
                        svm.Notify();

                    }

                }




                // Full.IsChecked = true;

            }
            else if (rb.Name == "Off")
            {
                Canvas c = Application.Current.TryFindResource("appbar_layer_arrange_solid_sendbackward") as Canvas;
                cp.Content = c;
                md.Content = cp;

                bool bl = (bool)md.ShowDialog();
                if (bl)
                {
                    string s = (DataContext as SplitViewModel).SelectedFE.Name.Replace(".", "");
                    //s = "."+s.ToLower();
                    //  ModernDialog.ShowMessage(s, "del", MessageBoxButton.OK);
                    if (Helper.Helper.SetRegKeyInvBool(s))
                    {

                        svm.SelectedFE.IsEnabled = false;
                        svm.SelectedFE.TemplateID = "";
                        //rbb.IsChecked = true;
                        //   AdornerLayer.GetAdornerLayer(rbb).Visibility = Visibility.Collapsed;
                        svm.Notify();
                    }

                    // Full.IsChecked = true;

                }
            }
            else
            {
                svm.Notify();
            }
        }



        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SplitViewModel svm = DataContext as SplitViewModel;
            TextBox tb = sender as TextBox;
            svm.FEIs = svm.Search(tb.Text);

            if (e.Key == Key.Enter || e.Key == Key.Down)
            {

                Board.SelectedIndex = 0; Board.Focus();

            }

        }

        private void ListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AdornerLayer.GetAdornerLayer(On).Visibility = Visibility.Collapsed;
                AdornerLayer.GetAdornerLayer(Off).Visibility = Visibility.Collapsed;

            }
            catch (Exception eee)
            {
            }

            var FEISs = Board.SelectedItems;
            SplitViewModel svm = DataContext as SplitViewModel;
            if (FEISs.Count > 1)
            {
                List<FileExtensionItem> realfeislist = FEISs.Cast<FileExtensionItem>().ToList();
                bool? nullbool = null;
                if (realfeislist.All(x => (bool)x.IsEnabled))
                {
                    nullbool = true;
                }
                else if (realfeislist.All(x => !(bool)x.IsEnabled))
                {
                    nullbool = false;
                }
                string templ = "";

                if (realfeislist.All(x => x.TemplateID.Length > 1))
                {
                    templ = "multi";
                }
                else if (realfeislist.All(x => x.TemplateID == "0"))
                {
                    templ = "0";
                }
                else if (realfeislist.All(x => x.TemplateID == ""))
                {
                    templ = "";
                }
                else if (realfeislist.All(x => x.TemplateID == "0" || x.TemplateID.Length > 2 || x.TemplateID == ""))
                {
                    templ = "3";
                    if (realfeislist.All(x => x.TemplateID == "0" || x.TemplateID.Length > 2))
                    {
                        templ = "1";

                    }
                    else if (realfeislist.All(x => x.TemplateID == "0" || x.TemplateID == ""))
                    {
                        templ = "2a";
                    }
                    else if (realfeislist.All(x => x.TemplateID.Length > 2 || x.TemplateID == ""))
                    {
                        templ = "2b";
                    }
                }
                else
                {
                    ModernDialog.ShowMessage("šudnt", "šudnt", MessageBoxButton.OK);
                }


                svm.SelectedFEISs = realfeislist;
                svm.SelectedFE = new FileExtensionItem
                {
                    IsEnabled = nullbool,
                    TemplateID = templ,
                    ID = nullbool == null ? 999 : 9999,
                    IsOpen = null,
                    IsDark = Helper.Helper.IsDark(Color.FromRgb(111, 111, 111)).ToString(),
                    IsBinary = null,
                    FEColor = (Color.FromRgb(111, 111, 111)),
                   // FEMTE = FileExtensionMidTypeEnum.None,
                    FT = FyleTipe.Mix,
                    Name = "FILE TYPE BUNDLE of " + realfeislist.Count,
                    FullName = "Multiple Selection",
                    IsNotEnabled = false,
                    Last = null

                };

            }
            else
            {

                List<FileExtensionItem> realfeislist = FEISs.Cast<FileExtensionItem>().ToList();

                svm.SelectedFEISs = realfeislist;
                if (svm.SelectedFEISs.Count == 1)
                {
                    svm.SelectedFE = svm.SelectedFEISs[0];
                }
                if (svm.SelectedFE == null)
                {
                    return;
                }
                if ((bool)svm.SelectedFE.IsEnabled)
                {
                    On.IsChecked = true; Off.IsChecked = false;

                }
                else
                {
                    Off.IsChecked = true; On.IsChecked = false;

                }
            }

        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            Searchit.Text = "";
            TextBox_KeyUp(Searchit, null);
            SplitViewModel svm = DataContext as SplitViewModel;
            svm.Notify();
        }

        private void On_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void On_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void Off_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Off_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Board_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            SplitViewModel svm = (DataContext as SplitViewModel);
            int cols = svm.Cols;

            if (e.Key == Key.Escape)
            {
                Searchit.SelectAll();
                Searchit.Focus();
            }

            if (e.Key == Key.Up)
            {
                if (Board.SelectedIndex < cols)
                {
                    Searchit.SelectAll();
                    Searchit.Focus();
                }
            }

        }

        private void Button_Drop(object sender, DragEventArgs e)
        {
            ModernDialog.ShowMessage(e.Data.ToString(), "", MessageBoxButton.OK);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SplitViewModel svm = (DataContext as SplitViewModel);

            svm.IsLoading = true;
            svm.SelectedFE = null;
            svm.SelectedFEISs = null;
            svm.Notify();
            Helper.Helper.MyHome.ReReg(null, null);
        }

        private void Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {


            Process myProcess = new Process();

            string result = @"C:\\Users\\" + Environment.UserName + "\\Templates\\";

            string fileName = result + (DataContext as SplitViewModel).SelectedFE.TemplateID;
            FileInfo finfo = new FileInfo(fileName);
            System.Diagnostics.Process.Start(fileName);

            /*    if (finfo.Extension == ".png" || finfo.Extension == ".jpeg" || finfo.Extension == ".tga"
                    || finfo.Extension == ".bmp" || finfo.Extension == ".tif" || finfo.Extension == ".tif"
                    || finfo.Extension == ".gif" || finfo.Extension == ".jpg" || finfo.Extension == ".tiff")
                {

                    Image i = new Image();
                    if (finfo.Extension == ".gif")
                    {
                        var image = new BitmapImage();
                        image.BeginInit();

                        image.UriSource = new Uri(finfo.DirectoryName + "/" + finfo.Name);

                        image.EndInit();

                        ImageBehavior.SetAnimatedSource(i, image);
                    }
                    else i.Source = new BitmapImage(new Uri(finfo.DirectoryName + "/" + finfo.Name));
                    var wnd = new ModernWindow
                    {
                        Style = (Style)App.Current.Resources["BlankWindow"],
                        Title = finfo.Name,
                        IsTitleVisible = true,
                        Content = i,
                        Width = 800,
                        Height = 600
                    };
                    wnd.LogoData = PathGeometry.Parse("F1 M 24.9015,43.0378L 25.0963,43.4298C 26.1685,49.5853 31.5377,54.2651 38,54.2651C 44.4623,54.2651 49.8315,49.5854 50.9037,43.4299L 51.0985,43.0379C 51.0985,40.7643 52.6921,39.2955 54.9656,39.2955C 56.9428,39.2955 58.1863,41.1792 58.5833,43.0379C 57.6384,52.7654 47.9756,61.75 38,61.75C 28.0244,61.75 18.3616,52.7654 17.4167,43.0378C 17.8137,41.1792 19.0572,39.2954 21.0344,39.2954C 23.3079,39.2954 24.9015,40.7643 24.9015,43.0378 Z M 26.7727,20.5833C 29.8731,20.5833 32.3864,23.0966 32.3864,26.197C 32.3864,29.2973 29.8731,31.8106 26.7727,31.8106C 23.6724,31.8106 21.1591,29.2973 21.1591,26.197C 21.1591,23.0966 23.6724,20.5833 26.7727,20.5833 Z M 49.2273,20.5833C 52.3276,20.5833 54.8409,23.0966 54.8409,26.197C 54.8409,29.2973 52.3276,31.8106 49.2273,31.8106C 46.127,31.8106 43.6136,29.2973 43.6136,26.197C 43.6136,23.0966 46.127,20.5833 49.2273,20.5833 Z");

                    wnd.ResizeMode = ResizeMode.CanResizeWithGrip;

                    wnd.Show();
                }
                else if (finfo.Extension == ".rtf" || finfo.Extension == ".txt" || finfo.Extension == ".odt" || finfo.Extension == ".docx"
                    || finfo.Extension == ".cpp" || finfo.Extension == ".js" || finfo.Extension == ".jsx"
                    || finfo.Extension == ".cs" || finfo.Extension == ".html" || finfo.Extension == ".xaml" || finfo.Extension == ".xml"
                    || finfo.Extension == ".css")
                {
                    StreamReader sr = finfo.OpenText();
                    ScrollViewer sv = new ScrollViewer();
                    //   if (finfo.Extension == ".txt") { RichTextBox rtb = new RichTextBox(); rtb.Selection.Load(sr.BaseStream, DataFormats.UnicodeText); sv.Content = rtb; }
                    if (finfo.Extension == ".rtf") { RichTextBox rtb = new RichTextBox(); rtb.IsEnabled = false; rtb.Selection.Load(sr.BaseStream, DataFormats.Rtf); sv.Content = rtb; }
                    else if (finfo.Extension == ".docx") { RichTextBox rtb = new RichTextBox(); rtb.IsEnabled = false; rtb.Selection.Load(sr.BaseStream, DataFormats.Rtf); sv.Content = rtb; }
                    //  else if (finfo.Extension == ".odt") { RichTextBox rtb = new RichTextBox(); rtb.IsEnabled = false; rtb.Selection.Load(sr.BaseStream, DataFormats.Rtf); sv.Content = rtb; }
                    ///else if (finfo.Extension == ".xaml") { RichTextBox rtb = new RichTextBox(); rtb.Selection.Load(sr.BaseStream, DataFormats.Xaml); sv.Content = rtb; }
                    ///modernuiapp1???????????????????????????????????
                    else
                    {
                        TextBlock tb = new TextBlock();
                        //SyntaxHighlightBox shb = new SyntaxHighlightBox();
                        tb.Text = sr.ReadToEnd();
                        tb.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                        //  shb.TextWrapping = TextWrapping.Wrap;
                        //    shb.MaxWidth = 450;
                        tb.TextWrapping = TextWrapping.Wrap;
                        sv.Content = tb;
                    }

                    var wnd = new ModernWindow
                    {
                        Style = (Style)App.Current.Resources["BlankWindow"],
                        Title = finfo.Name,
                        IsTitleVisible = true,
                        Content = sv,
                        Width = 480,
                        Height = 480
                    };
                    wnd.LogoData = PathGeometry.Parse("F1 M 24.9015,43.0378L 25.0963,43.4298C 26.1685,49.5853 31.5377,54.2651 38,54.2651C 44.4623,54.2651 49.8315,49.5854 50.9037,43.4299L 51.0985,43.0379C 51.0985,40.7643 52.6921,39.2955 54.9656,39.2955C 56.9428,39.2955 58.1863,41.1792 58.5833,43.0379C 57.6384,52.7654 47.9756,61.75 38,61.75C 28.0244,61.75 18.3616,52.7654 17.4167,43.0378C 17.8137,41.1792 19.0572,39.2954 21.0344,39.2954C 23.3079,39.2954 24.9015,40.7643 24.9015,43.0378 Z M 26.7727,20.5833C 29.8731,20.5833 32.3864,23.0966 32.3864,26.197C 32.3864,29.2973 29.8731,31.8106 26.7727,31.8106C 23.6724,31.8106 21.1591,29.2973 21.1591,26.197C 21.1591,23.0966 23.6724,20.5833 26.7727,20.5833 Z M 49.2273,20.5833C 52.3276,20.5833 54.8409,23.0966 54.8409,26.197C 54.8409,29.2973 52.3276,31.8106 49.2273,31.8106C 46.127,31.8106 43.6136,29.2973 43.6136,26.197C 43.6136,23.0966 46.127,20.5833 49.2273,20.5833 Z");

                    wnd.ResizeMode = ResizeMode.CanResizeWithGrip;

                    wnd.Show();
                }
                else if (false)
                {
                    StreamReader sr = finfo.OpenText();
                    Helper.Helper.mediaPlayer = new MediaPlayer();

                    Helper.Helper.mediaPlayer.Volume = 100;
                    ScrollViewer sv = new ScrollViewer();
                    Helper.Helper.mediaPlayer.Open(new Uri(finfo.DirectoryName + "/" + finfo.Name));
                    Helper.Helper.mediaPlayer.Play();
                    Slider slider = new Slider();
                    slider.Maximum = 100;
                    slider.Value = Helper.Helper.mediaPlayer.Volume * 100;
                    slider.ValueChanged += (x, y) => { Helper.Helper.mediaPlayer.Volume = y.NewValue / 100; };
                    Slider slider2 = new Slider();
                    slider2.Value = 5;
                    slider2.Maximum = 100;



                    ModernButton mb1 = new ModernButton();
                    mb1.Content = "Play";
                    mb1.Click += (x, y) => { Helper.Helper.mediaPlayer.Play(); };
                    ModernButton mb2 = new ModernButton();
                    mb2.Content = "Stop";
                    mb2.Click += (x, y) => { Helper.Helper.mediaPlayer.Stop(); };
                    ModernButton mb3 = new ModernButton();
                    mb3.Content = "Pause";
                    mb3.Click += (x, y) => { Helper.Helper.mediaPlayer.Pause(); };
                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    StackPanel sp2 = new StackPanel();
                    TextBlock tb = new TextBlock();
                    TextBlock tb2 = new TextBlock();
                    tb.Text = "None";
                    tb2.Text = Helper.Helper.mediaPlayer.Position.TotalSeconds.ToString();
                    sp.Children.Add(mb1);
                    sp.Children.Add(mb2);
                    sp.Children.Add(mb3);

                    sp2.Children.Add(sp);
                    sp2.Children.Add(slider);
                    // sp2.Children.Add(slider2);
                    sp2.Children.Add(tb);
                    // sp2.Children.Add(tb2);

                    sv.Content = sp2;
                    var wnd = new ModernWindow
                    {
                        DataContext = tb,
                        Style = (Style)App.Current.Resources["BlankWindow"],
                        Title = fileName,
                        IsTitleVisible = true,
                        Content = sv,
                        Width = 480,
                        Height = 480
                    };
                    wnd.Closed += (x, y) => { Helper.Helper.wnd = null; Helper.Helper.mediaPlayer = null; };
                    Helper.Helper.wnd = wnd;
                    wnd.Show();

                    DispatcherTimer timer2 = new DispatcherTimer();
                    timer2.Interval = TimeSpan.FromSeconds(1);
                    timer2.Tick += timer_Tick2;
                    timer2.Start();

                }
                else if (finfo.Extension == ".avi" || finfo.Extension == ".wav" ||
                        finfo.Extension == ".mov" || finfo.Extension == ".mpeg" || finfo.Extension == ".mpg" ||
                        finfo.Extension == ".mp4" || finfo.Extension == ".mkv" || finfo.Extension == ".tms" ||
                        finfo.Extension == ".vob" || finfo.Extension == ".webm" || finfo.Extension == ".wav" ||
                        finfo.Extension == ".flv" || finfo.Extension == ".m4v" || finfo.Extension == ".wmv" ||
                        finfo.Extension == ".wav" || finfo.Extension == ".mts" || finfo.Extension == ".mp3" ||
                        finfo.Extension == ".wma")
                {
                    StackPanel g = new StackPanel();

                    MediaElement me = new MediaElement();
                    me.LoadedBehavior = MediaState.Manual;
                    me.Source = new Uri(finfo.Directory + "/" + finfo.Name);

                    Slider slider = new Slider();
                    slider.Maximum = 100;
                    slider.Value = me.Volume * 100;
                    slider.ValueChanged += (x, y) => { me.Volume = y.NewValue / 100; };




                    StackPanel sp = new StackPanel();
                    sp.Orientation = Orientation.Horizontal;
                    sp.HorizontalAlignment = HorizontalAlignment.Center;
                    Border b = new Border();
                    b.BorderThickness = new Thickness(2);
                    Border b2 = new Border();
                    b2.BorderThickness = new Thickness(2);
                    Border b3 = new Border();
                    b3.BorderThickness = new Thickness(2);
                    Border b4 = new Border();
                    b4.BorderThickness = new Thickness(2);

                    ModernButton mb1 = new ModernButton();
                    ModernButton mb2 = new ModernButton();
                    ModernButton mb3 = new ModernButton();
                    mb1.Margin = new Thickness(7);
                    mb2.Margin = new Thickness(7);
                    mb3.Margin = new Thickness(7);
                    mb1.Content = "Play";
                    mb2.Content = "Pause";
                    mb3.Content = "Stop";
                    mb1.Click += (x, y) => { me.Play(); };
                    mb2.Click += (x, y) => { me.Pause(); };
                    mb3.Click += (x, y) => { me.Stop(); };
                    mb1.IconData = Geometry.Parse("F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z ");
                    mb2.IconData = Geometry.Parse("F1 M 26.9167,23.75L 33.25,23.75L 33.25,52.25L 26.9167,52.25L 26.9167,23.75 Z M 42.75,23.75L 49.0833,23.75L 49.0833,52.25L 42.75,52.25L 42.75,23.75 Z ");
                    mb3.IconData = Geometry.Parse("F1 M 0,0L 76,0L 76,76L 0,76L 0,0");
                    sp.Children.Add(mb1);
                    sp.Children.Add(mb3);
                    sp.Children.Add(mb2);

                    TextBlock tb = new TextBlock();
                    tb.Text = "Not Playing";

                    b.Child = sp;
                    b2.Child = tb;
                    b3.Child = slider;
                    b4.Child = me;
                    g.Children.Add(b4);
                    g.Children.Add(b);
                    g.Children.Add(b3);
                    g.Children.Add(b2);

                    var wnd = new ModernWindow
                    {
                        DataContext = g,
                        Style = (Style)App.Current.Resources["BlankWindow"],
                        Title = fileName,
                        IsTitleVisible = true,
                        Content = g,
                        Width = 480,
                        Height = 480
                    };
                    wnd.Closed += (x, y) => { Helper.Helper.wnd = null; Helper.Helper.mediaPlayer = null; };
                    Helper.Helper.wnd = wnd;

                    me.Play();
                    wnd.Show();
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer_Tick;
                    timer.Start();

                }
                else
                {
                    ModernDialog.ShowMessage("Unavailable", "u", MessageBoxButton.OK);
                }


                */

        }
        void timer_Tick2(object sender, EventArgs e)
        {
            if (Helper.Helper.wnd != null)
            {
                if (Helper.Helper.mediaPlayer != null && Helper.Helper.mediaPlayer.Source != null)
                    (Helper.Helper.wnd.DataContext as TextBlock).Text = String.Format("{0} / {1}", Helper.Helper.mediaPlayer.Position.ToString(@"mm\:ss"), Helper.Helper.mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                else
                    (Helper.Helper.wnd.DataContext as TextBlock).Text = "No file selected...";
                if (true)
                {

                }
            }

        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (Helper.Helper.wnd != null)
            {

                MediaElement me = ((Helper.Helper.wnd.DataContext as StackPanel).Children[0] as Border).Child as MediaElement;
                TextBlock tb = ((Helper.Helper.wnd.DataContext as StackPanel).Children[3] as Border).Child as TextBlock;
                if (me != null && me.Source != null)
                {
                    if (me.NaturalDuration.HasTimeSpan)
                        tb.Text = String.Format("{0} / {1}", me.Position.ToString(@"mm\:ss"), me.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                }
                else
                    tb.Text = "No file selected...";
            }
        }

    }
}

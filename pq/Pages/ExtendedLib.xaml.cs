using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for ExtendedLib.xaml
    /// </summary>
    public partial class ExtendedLib : UserControl, INotifyPropertyChanged, IContent
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        Ex mySel = null;
        List<Ex> lex = Helper.Helper.LEX.Where(x => x.IsMine == false).OrderBy(x => x.Name).ToList();
        public List<Ex> LEX
        {
            get
            {
                return lex;
            }
            set
            {
                lex = value;
                OnPropertyChanged("LEX");
            }
        }
        public Ex MySel
        {
            get
            {
                return mySel;
            }
            set
            {
                mySel = value;
                OnPropertyChanged("MySel");
            }
        }
        public ExtendedLib()
        {
            InitializeComponent();
            DataContext = this;
            using (var ent = new ExtensionlessBaseEntities())
            {
                Setting st = ent.Setting.FirstOrDefault();
                if (st.IsExtended == true)
                {

                    off.Visibility = Visibility.Visible;
                    on.Visibility = Visibility.Collapsed;
                }
                else
                {

                    on.Visibility = Visibility.Visible;
                    off.Visibility = Visibility.Collapsed;
                }
                ent.SaveChanges();
            }
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void CheckBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CheckBox cb = ((sender as Grid).Children[0] as CheckBox);
            cb.IsChecked = !cb.IsChecked;
            //  LEX.Where(x => x.Name == (sender as CheckBox).Content.ToString()).FirstOrDefault().IsExtended = !LEX.Where(x => x.Name == (sender as CheckBox).Content.ToString()).FirstOrDefault().IsExtended;

        }

        private void ListBox_Selected(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MySel == null) return;
            MySel.IsExtended = !MySel.IsExtended;
            MySel = (sender as ListBox).SelectedItem as Ex;
            LEX = lex.ToList();

        }
        private int cols = 8;
        public int Cols
        {
            get { return this.cols; }
            set
            {
                if (this.cols != value)
                {
                    this.cols = value;
                    OnPropertyChanged("Cols");

                }
            }
        }
        private void ListBox_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Cols = (int)(e.NewSize.Width / 60);
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (MySel == null)
            {
                return;
            }
            if (((sender as Grid).Children[0] as CheckBox).Content.ToString() == MySel.Name)
            {
                MySel = null;
                LexLB.SelectedItem = null;
            }
        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ModernButton_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            (Application.Current.MainWindow as ModernWindow).LinkNavigator.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative), this as FrameworkElement);

            SettingsPage.SP.spfaq.SelectedSource = new Uri("/Pages/Faq/faq16.xaml", UriKind.RelativeOrAbsolute);

        }

        private void IsExtended_Checked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {

                Setting st = ent.Setting.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                st.IsExtended = true;
                on.Visibility = Visibility.Visible;
                off.Visibility = Visibility.Collapsed;
                ent.SaveChanges();
            }
        }

        private void IsExtended_Unchecked(object sender, RoutedEventArgs e)
        {
            using (var ent = new ExtensionlessBaseEntities())
            {
                Setting st = ent.Setting.FirstOrDefault(x => x.ExPro.WinUsername == Environment.UserName);
                st.IsExtended = false;
                off.Visibility = Visibility.Visible;
                on.Visibility = Visibility.Collapsed;
                ent.SaveChanges();
            }
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Helper.Helper.getDBST();
            IsExtended.IsChecked = Helper.Helper.ST.IsExtended;

            //    Helper.Helper.SetTop(false);
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}

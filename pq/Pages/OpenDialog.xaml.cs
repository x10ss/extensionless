using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class OpenDialog : ModernDialog
    {
        private bool IsAdmin;
        ModernButton user = new ModernButton();
        ModernButton pc = new ModernButton();
        ModernButton cancel = new ModernButton();
        public OpenDialog()
        {

            IsAdmin = Helper.Helper.IsAdministrator();


            pc.EllipseDiameter = 50;
            pc.IconWidth = 26;
            pc.IconHeight = 26;
            user.EllipseDiameter = 50;
            user.IconHeight = 30;
            user.IconWidth = 30;
            pc.Margin = new Thickness(5);
            user.Margin = new Thickness(5);
            pc.IconData = Geometry.Parse("F1 M 40,44L 39.9999,51L 44,51C 45.1046,51 46,51.8954 46,53L 46,57C 46,58.1046 45.1045,59 44,59L 32,59C 30.8954,59 30,58.1046 30,57L 30,53C 30,51.8954 30.8954,51 32,51L 36,51L 36,44L 40,44 Z M 47,53L 57,53L 57,57L 47,57L 47,53 Z M 29,53L 29,57L 19,57L 19,53L 29,53 Z M 19,22L 57,22L 57,31L 19,31L 19,22 Z M 55,24L 53,24L 53,29L 55,29L 55,24 Z M 51,24L 49,24L 49,29L 51,29L 51,24 Z M 47,24L 45,24L 45,29L 47,29L 47,24 Z M 21,27L 21,29L 23,29L 23,27L 21,27 Z M 19,33L 57,33L 57,42L 19,42L 19,33 Z M 55,35L 53,35L 53,40L 55,40L 55,35 Z M 51,35L 49,35L 49,40L 51,40L 51,35 Z M 47,35L 45,35L 45,40L 47,40L 47,35 Z M 21,38L 21,40L 23,40L 23,38L 21,38 Z ");
            user.IconData = Geometry.Parse("F1 M 40,44L 39.9999,51L 44,51C 45.1046,51 46,51.8954 46,53L 46,57C 46,58.1046 45.1045,59 44,59L 32,59C 30.8954,59 30,58.1046 30,57L 30,53C 30,51.8954 30.8954,51 32,51L 36,51L 36,44L 40,44 Z M 47,53L 57,53L 57,57L 47,57L 47,53 Z M 29,53L 29,57L 19,57L 19,53L 29,53 Z M 29,42L 29,30.5L 27.8611,31.7778L 25.9444,28.5833L 38.0833,19L 43,23L 43,20.5873L 45,19.5907L 45,24.5L 50.2222,28.5833L 48.3056,31.7778L 47,30.5L 47,42L 29,42 Z M 38.0833,23.419L 31,29.1389L 31,40L 35,40L 35,33L 41,33L 41,40L 45,40L 45,29.1389L 38.0833,23.419 Z ");

            pc.HorizontalAlignment = HorizontalAlignment.Stretch;
            user.HorizontalAlignment = HorizontalAlignment.Stretch;
            //  pc.Foreground = new SolidColorBrush(Color.FromRgb(88, 111, 111));
            //   user.Foreground = new SolidColorBrush(Color.FromRgb(55, 55, 155));
            cancel.Content = "YOU HAVE TO BE AN ADMINISTRATOR";
            cancel.Visibility = IsAdmin ? Visibility.Collapsed : Visibility.Visible;
            user.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            pc.Visibility = IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            cancel.Click += Exit;
            InitializeComponent();
            //   Content = new InkCanvas();
            // define the dialog buttons
            pc.Content = Environment.UserDomainName;
            pc.Click += TreeViewItem_MouseDoubleClick;
            pc.IsEnabled = true;
            user.IsEnabled = true;
            user.Content = Environment.UserName;
            user.Click += TreeViewItem_MouseDoubleClick_1;
            this.Buttons = new Button[] { pc, user, cancel };
            //Username.Text = "Hi " + Helper.Helper.Username.ToUpper();


        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        private void TreeViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void TreeViewItem_MouseDoubleClick_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            user.Focus();
            // user.IsEnabled = false;
        }

        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            //  pc.IsEnabled = false;
            pc.Focus();
        }

        private void TreeViewItem_MouseDoubleClick_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DialogResult = true;

        }

        private void TreeViewItem_MouseDoubleClick_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DialogResult = false;

        }

        private void TreeViewItem_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = false;
        }

        private void TreeViewItem_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            (sender as TreeViewItem).IsSelected = false;
        }
    }
}

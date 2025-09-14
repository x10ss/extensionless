using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages.Faq
{
    /// <summary>
    /// Interaction logic for faq1.xaml
    /// </summary>
    public partial class faq16 : UserControl
    {
        public faq16()
        {
            InitializeComponent();
        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (Application.Current.MainWindow as ModernWindow).LinkNavigator.Navigate(new Uri("/Pages/ExtendedLib.xaml", UriKind.RelativeOrAbsolute), SettingsPage.SP as FrameworkElement);


        }
    }
}

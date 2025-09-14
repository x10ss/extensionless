using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class Forgot : UserControl
    {

        public Forgot()
        {

            InitializeComponent();

        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("sentmail");
            (Application.Current.MainWindow as ModernWindow).LinkNavigator.Navigate(new System.Uri("/Pages/Login.xaml", System.UriKind.RelativeOrAbsolute), this as FrameworkElement);
        }
    }
}

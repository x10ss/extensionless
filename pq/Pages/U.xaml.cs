using FirstFloor.ModernUI.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for U.xaml
    /// </summary>
    public partial class U : UserControl, IContent
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

            Helper.Helper.svm = null;

            //  Helper.Helper.CurentScreen = this as FrameworkElement;
            //  throw new NotImplementedException();
            //AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
        public U()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool result = Helper.Helper.IsValidEmailAddress(tb.Text);
            if (!result)
            {
                tb.BorderThickness = new Thickness(5, 2, 5, 2);
                tb.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            }
            else
            {
                tb.BorderThickness = new Thickness(2, 4, 2, 4);
                tb.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 255, 0));

            }
        }
    }
}

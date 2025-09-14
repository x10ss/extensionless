using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Extenstionless.xaml
    /// </summary>
    public partial class Extenstionless : UserControl, IContent
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

            // Helper.Helper.CurentScreen = this as FrameworkElement;
            //    Helper.Helper.SetTop(true);
            //  throw new NotImplementedException();
            //AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
        public Extenstionless()
        {
            InitializeComponent();
        }
    }
}

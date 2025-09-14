using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : UserControl, IContent
    {
        public static SettingsPage SP { get; set; }
        public SettingsPage()
        {
            InitializeComponent();
            SP = this;

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
            //   Helper.Helper.SetTop(true);
            //AppearanceManager.Current.AccentColor = Color.FromRgb(150, 150, 150);
            //  Helper.Helper.CurentScreen = this as FrameworkElement;//  throw new NotImplementedException();
            //AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
    }
}

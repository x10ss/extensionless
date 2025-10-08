using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;


namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Templates.xaml
    /// </summary>
    public partial class UserTemplates : UserControl, IContent
    {
        public static ModernTab mt;
        public UserTemplates()
        {
            InitializeComponent();
            mt = ListLinksList;
            mt.SelectedSource = new System.Uri("/Pages/UTemplatePack.xaml", System.UriKind.Relative);



        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

            //  Helper.Helper.SetTop(false);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}

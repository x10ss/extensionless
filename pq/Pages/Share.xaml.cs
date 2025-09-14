using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Share : UserControl, IContent
    {
        public Achievements Ach { get; set; }
        public Share()
        {
            // myPro.
            InitializeComponent();
            Ach = Helper.Helper.GetAch();
            Check();
            DataContext = this;

        }

        private void Check()
        {
            //   throw new NotImplementedException();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {

            //    Helper.Helper.SetTop(false);
            Ach = Helper.Helper.GetAch();
            Check();
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
    }
}

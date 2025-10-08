using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class Login : UserControl, IContent
    {
        public static Login login;

        public TextBox EName;
        public TextBox EID;


        private bool IsAdmin;
        public Login()
        {

            InitializeComponent();

            EName = exuser;
          //  EID = exguid;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            login = null;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            login = this;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
    }
}

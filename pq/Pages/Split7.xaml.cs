using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Split.xaml
    /// </summary>
    public partial class Split7 : UserControl, IContent
    {
        public SplitViewModel SVM = new SplitViewModel(-1);
        public Split7()
        {
            InitializeComponent();

            this.DataContext = SVM;
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
            Helper.Helper.svm = SVM;
            SVM.IsLoading = !Helper.Helper.MyHome.MyIsLoaded;
            SVM.Notify();
            //  throw new NotImplementedException();
            //  AppearanceManager.Current.AccentColor = Helper.Helper.GetAccent(13);

            fetulj.Focus();// Helper.Helper.CurentScreen = fetulj as FrameworkElement;
            //    Helper.Helper.SetTop(false);
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }
    }
}

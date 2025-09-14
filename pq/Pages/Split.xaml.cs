using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Split.xaml
    /// </summary>
    public partial class Split : UserControl, IContent
    {
        public SplitViewModel SVM = new SplitViewModel(0);
        public Split()
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
            SVM.SelectedFE = !Helper.Helper.MyHome.MyIsLoaded ? null : SVM.SelectedFE;
            SVM.SelectedFEISs = !Helper.Helper.MyHome.MyIsLoaded ? null : SVM.SelectedFEISs;
            SVM.IsLoading = !Helper.Helper.MyHome.MyIsLoaded;
            SVM.Notify();
            //   Helper.Helper.SetTop(false);
            //  throw new NotImplementedException();
            //   AppearanceManager.Current.AccentColor = Helper.Helper.GetAccent(1);

            fetulj.Focus();
            //     Helper.Helper.CurentScreen = fetulj as FrameworkElement;
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //   throw new NotImplementedException();
        }

    }
}

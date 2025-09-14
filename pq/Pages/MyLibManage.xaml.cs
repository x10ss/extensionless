using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using pq.Model;
using System;
using System.Linq;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for MyLibManage.xaml
    /// </summary>
    public partial class MyLibManage : UserControl, IContent
    {
        public MyLibManage()
        {
            InitializeComponent();
            FEMTE.ItemsSource = Enum.GetValues(typeof(FileExtensionMidTypeEnum)).Cast<FileExtensionMidTypeEnum>();
            FT.ItemsSource = Enum.GetValues(typeof(FyleTipe)).Cast<FyleTipe>();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {

            //  Helper.Helper.SetTop(false);
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
    }
}

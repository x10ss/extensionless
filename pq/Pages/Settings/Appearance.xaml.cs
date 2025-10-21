using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace pq.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : System.Windows.Controls.UserControl, IContent
    {
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new AppearanceViewModel();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            //   Helper.Helper.SetTop(true);
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    folder.Content = selectedPath;
                }
            }
        }
    }
}

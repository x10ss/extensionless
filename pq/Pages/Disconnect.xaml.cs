using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class Disconnect : UserControl, IContent
    {
        public static ExPro disconn;
        public Disconnect()
        {

            InitializeComponent();

        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("sure?");
            using (ExtensionlessBaseEntities ent = new ExtensionlessBaseEntities())
            {
                ExPro ep = ent.ExPro.Where(x => x.WinUsername == Environment.UserName).FirstOrDefault();
                ent.Setting.Remove(ent.Setting.Where(x => x.ExPro.ExID == ep.ExID).FirstOrDefault());
                ent.ExPro.Remove(ep);
                ent.SaveChanges();
            }
            ExPro newep = Helper.Helper.GetExPro(true);
            ExDialog.exdialog.ep = newep;
            Helper.Helper.getDBST2(newep.Id);
            ExDialog.exdialog.DialogResult = true;

        }
    }
}

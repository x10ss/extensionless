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
        public static x10ss disconn;
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
            using (ex10sionlessEntities ent = new ex10sionlessEntities())
            {
                x10ss ep = ent.x10ss.Where(x => x.WindowsUsername == Environment.UserName).FirstOrDefault();
                ent.x10ss.Remove(ep);
                ent.SaveChanges();
            }
            x10ss newep = Helper.Helper.GetExPro(true);
            ExDialog.exdialog.ep = newep;
            //Helper.Helper.getDBST2(newep.Id);
            ExDialog.exdialog.DialogResult = true;

        }
    }
}

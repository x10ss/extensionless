using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class Delete : UserControl, IContent
    {
        public static x10ss del;
        public Delete()
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
                //    ent.Settings.Remove(ent.Settings.Where(x => x.ExPro.ExID == ep.ExID).FirstOrDefault());


                string query = "delete from boilpack where Username='" + ep.Username + "'";
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                int reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    MessageBox.Show("Bravo - " + reader.ToString());
                }
                else
                {
                    MessageBox.Show("Fail - " + reader.ToString());
                }

                DBConnection.Close();



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

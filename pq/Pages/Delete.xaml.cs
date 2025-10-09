using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class Delete : UserControl
    {

        public Delete()
        {


        }

        private void ModernButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("TODO DELETE ON SERVER");
            using (Entities ent = new Entities())
            {
                ent.ExProes.Remove(ent.ExProes.Where(x => x.WinUsername == Environment.UserName).FirstOrDefault());
                ent.SaveChanges();
            }
            ExDialog.exdialog.ep = Helper.Helper.GetExPro(true);
            ExDialog.exdialog.DialogResult = true;

        }
    }
}

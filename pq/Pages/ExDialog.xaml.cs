using FirstFloor.ModernUI.Windows.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pq.Pages
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class ExDialog : ModernDialog
    {
        public ExPro ep { get; set; }
        public static ExDialog exdialog;
        Button OK = new Button();
        Button PC = new Button();
        public ExDialog()
        {

            InitializeComponent();
            exdialog = this;
            //   Content = new InkCanvas();
            // define the dialog buttons
            PC.Content = "Cancel";
            OK.Click += OK_Click;
            PC.Click += Cancel_Click;
            OK.Content = "OK";
            PC.Margin = new Thickness(5);
            OK.Margin = new Thickness(5);
            this.Buttons = new Button[] { PC, OK };

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (CreateAccount.createAcc != null)
            {
                string exguid = Guid.NewGuid().ToString();

                string dt = ((DateTime)(CreateAccount.createAcc.EDob.SelectedDate)).ToString("yyyy-MM-dd HH:mm:ss");

                //suppose col0 and col1 are defined as VARCHAR in the DB
                // string query = "SELECT ID FROM ExPro";
                string query = "insert into ExPro(ExProID,WinProID,CountryID,Username,DonateUrl,Email,Dob) values('" + exguid + "','" + Helper.Helper.GetGuid()
                    + "','" + CreateAccount.createAcc.EFlag.SelectedItem.ToString() +
                    "','" + CreateAccount.createAcc.EName.Text + "','" + CreateAccount.createAcc.EDonate.Text +
                    "','" + CreateAccount.createAcc.EEmail.Text + "','" + dt + "');";
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    MessageBox.Show("Bravo - " + reader.ToString());
                }
                else
                {
                    MessageBox.Show("Fail - " + reader.ToString());
                }

                DBConnection.Close();

                using (ExtensionlessBaseEntities ent = new ExtensionlessBaseEntities())
                {
                    ExPro newep = ent.ExPro.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                    newep.ExUsername = CreateAccount.createAcc.EName.Text;
                    newep.Email = CreateAccount.createAcc.EEmail.Text;
                    newep.DonateUrl = CreateAccount.createAcc.EDonate.Text;
                    newep.Dob = CreateAccount.createAcc.EDob.SelectedDate;
                    newep.ExID = exguid;
                    newep.Country = CreateAccount.createAcc.EFlag.SelectedItem.ToString();
                    ent.SaveChanges();
                    ep = newep;

                }
                this.DialogResult = true;


            }
            else if (ChangeData.createAcc != null)
            {
                string exguid = Guid.NewGuid().ToString();

                string dt = ((DateTime)(ChangeData.createAcc.EDob.SelectedDate)).ToString("yyyy-MM-dd HH:mm:ss");

                //suppose col0 and col1 are defined as VARCHAR in the DB
                // string query = "SELECT ID FROM ExPro";
                string query = "Update ExPro set ExPro.CountryID='" + ChangeData.createAcc.EFlag.SelectedItem.ToString() +
                    "', ExPro.Username='" + ChangeData.createAcc.EName.Text + "',ExPro.DonateUrl='" + ChangeData.createAcc.EDonate.Text +
                    "',ExPro.Email='" + ChangeData.createAcc.EEmail.Text + "',ExPro.Dob='" + dt + "' where ExPro.ExProID='" + Helper.Helper.Synched().ExID + "';";
                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    MessageBox.Show("Bravogg - " + reader.ToString());
                }
                else
                {
                    MessageBox.Show("Fail - " + reader.ToString());
                }

                DBConnection.Close();

                using (ExtensionlessBaseEntities ent = new ExtensionlessBaseEntities())
                {
                    ExPro newep = ent.ExPro.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                    newep.ExUsername = ChangeData.createAcc.EName.Text;
                    newep.Email = ChangeData.createAcc.EEmail.Text;
                    newep.DonateUrl = ChangeData.createAcc.EDonate.Text;
                    newep.Dob = ChangeData.createAcc.EDob.SelectedDate;
                    newep.Country = ChangeData.createAcc.EFlag.SelectedItem.ToString();
                    ent.SaveChanges();
                    ep = newep;

                }
                this.DialogResult = true;
            }
            else if (Login.login != null)
            {

                //suppose col0 and col1 are defined as VARCHAR in the DB
                // string query = "SELECT ID FROM ExPro";
                string query = "SELECT ExPro.ExProID, ExPro.CountryID,ExPro.Username,ExPro.DonateUrl,ExPro.Email,ExPro.Dob FROM ExPro WHERE ExPro.ExProID='" + Login.login.EID.Text +
                    "' and ExPro.Username='" + Login.login.EName.Text +
                    "';";

                var cmd = new MySqlCommand(query, DBConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    using (ExtensionlessBaseEntities ent = new ExtensionlessBaseEntities())
                    {
                        ExPro newep = ent.ExPro.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                        newep.ExUsername = reader.GetString(2);
                        newep.Email = reader.GetString(4);
                        newep.DonateUrl = reader.GetString(3);
                        newep.Dob = reader.GetDateTime(5);
                        newep.Country = reader.GetString(1);
                        newep.ExID = reader.GetString(0);
                        newep.WinUsername = Environment.UserName;
                        ent.SaveChanges();
                        ep = newep;

                    }

                    MessageBox.Show("Bravoggrrri22222ttt - ");


                }


                DBConnection.Close();
                this.DialogResult = true;
            }
        }




    }
}

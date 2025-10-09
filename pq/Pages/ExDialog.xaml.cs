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
            if (CreateAccount.createAccount != null)
            {

               // string dt = ((DateTime)(CreateAccount.createAcc.EDob.SelectedDate)).ToString("yyyy-MM-dd HH:mm:ss");

                //suppose col0 and col1 are defined as VARCHAR in the DB
                // string query = "SELECT ID FROM ExPro";
               
                
                string query = "insert into boilpack(ExProID, CountryID, Username,DonateURL, BoilerplateZip, Hits, ZipSize, Category, FilesList, Password) " +
                    "values('" + Helper.Helper.GetGuid()
                    + "','" + CreateAccount.createAccount.EFlag.SelectedItem.ToString() 
                    + "','" + CreateAccount.createAccount.EName.Text
                    + "','" + CreateAccount.createAccount.EDonate.Text 
                    + "' , '',0, 0, '', '', '"+ CreateAccount.createAccount.EPassword.Password + "');";
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

                using (Entities ent = new Entities())
                {
                    ExPro newep = ent.ExProes.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                    //ExPro newep = new ExPro();
                    newep.ExUsername = CreateAccount.createAccount.EName.Text;
                 //   newep.Password = CreateAccount.createAccount.EPassword.Password;
                   // newep.Email = CreateAccount.createAcc.EEmail.Text;
                    newep.DonateUrl = CreateAccount.createAccount.EDonate.Text;
                    newep.Password = CreateAccount.createAccount.EPassword.Password;
                   // newep.Dob = CreateAccount.createAcc.EDob.SelectedDate;
                    newep.ExID = Helper.Helper.GetGuid();
                    newep.Country = CreateAccount.createAccount.EFlag.SelectedItem.ToString();
                    ent.SaveChanges();
                    ep = newep;
                }
                CreateAccount.createAccount = null; 
                this.DialogResult = true;

            }
            else if (ChangeData.changeData != null)
            {
               // string dt = ((DateTime)(ChangeData.createAcc.EDob.SelectedDate)).ToString("yyyy-MM-dd HH:mm:ss");

                //suppose col0 and col1 are defined as VARCHAR in the DB
                // string query = "SELECT ID FROM ExPro";
                string query = "Update boilpack set CountryID='" + ChangeData.changeData.EFlag.SelectedItem.ToString() +
                    "', Username='" + ChangeData.changeData.EName.Text + "',DonateUrl='" + ChangeData.changeData.EDonate.Text 
                    + "',Password='" + ChangeData.changeData.EPassword.Password + "' " +
                    "WHERE ExProID = '"+ Helper.Helper.GetGuid() +"';";
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

                using (Entities ent = new Entities())
                {
                    ExPro newep = ent.ExProes.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                    newep.ExUsername = ChangeData.changeData.EName.Text;
                 //   newep.Password = ChangeData.changeData.EPassword.Password;
                    newep.DonateUrl = ChangeData.changeData.EDonate.Text;
                    newep.Password = ChangeData.changeData.EPassword.Password;
                    newep.Country = ChangeData.changeData.EFlag.SelectedItem.ToString();
                    ent.SaveChanges();
                    ep = newep;

                }
                ChangeData.changeData = null;   
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

                    using (Entities ent = new Entities())
                    {
                        ExPro newep = ent.ExProes.Where(x => x.WinUsername == System.Environment.UserName).FirstOrDefault();
                        newep.ExUsername = reader.GetString(2);
                       // newep.Email = reader.GetString(4);
                        newep.DonateUrl = reader.GetString(3);
                      //  newep.Dob = reader.GetDateTime(5);
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
